using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif
#if UNITY_EDITOR
using UnityEditor.Media;
#endif

[RequireComponent(typeof(Camera))]
public class SceneRecorder : MonoBehaviour
{
    public enum VideoFormat
    {
        Video,
        PngSequence
    }

    [Header("Hotkeys")]
    public KeyCode stillKey = KeyCode.F9;
    public KeyCode recordKey = KeyCode.F10;

    [Header("Resolution")]
    public int width = 1920;
    public int height = 1080;

    [Header("Video")]
    public int frameRate = 30;
    public VideoFormat videoFormat = VideoFormat.Video;
    public float maxSeconds = 0f;
    public bool saveFrames;

    [Header("Files")]
    public string outputFolder = "Recordings";
    public string fileNamePrefix = "world";

    [Header("Feedback")]
    public bool showRecordingIndicator = true;

    public bool IsRecording { get; private set; }

    Camera cam;
    RenderTexture renderTarget;
    Texture2D frame;
    Coroutine recordLoop;
    int frameIndex;
    string sequenceFolder;
    GUIStyle indicatorStyle;
#if UNITY_EDITOR
    MediaEncoder encoder;
#endif
#if ENABLE_INPUT_SYSTEM
    Key stillKeyNew;
    Key recordKeyNew;
#endif

    Camera Cam => cam != null ? cam : cam = GetComponent<Camera>();

    void Awake()
    {
        cam = GetComponent<Camera>();
#if ENABLE_INPUT_SYSTEM
        stillKeyNew = TranslateKey(stillKey, Key.F9);
        recordKeyNew = TranslateKey(recordKey, Key.F10);
#endif
    }

    void Update()
    {
        if (WasPressed(stillKey))
            TakeStill();

        if (WasPressed(recordKey))
            ToggleRecording();
    }

    [ContextMenu("Take Still")]
    public void TakeStill()
    {
        var size = EvenSize();
        var target = RenderTexture.GetTemporary(size.x, size.y, 24, RenderTextureFormat.ARGB32);
        var texture = new Texture2D(size.x, size.y, TextureFormat.RGBA32, false);

        RenderCameraTo(target);
        ReadInto(target, texture);

        var path = Path.Combine(OutputDirectory(), $"{fileNamePrefix}_{Timestamp()}.png");
        File.WriteAllBytes(path, texture.EncodeToPNG());

        RenderTexture.ReleaseTemporary(target);
        SafeDestroy(texture);

        Debug.Log($"[SceneRecorder] Still saved: {path}");
    }

    [ContextMenu("Toggle Recording")]
    public void ToggleRecording()
    {
        if (IsRecording)
            StopRecording();
        else
            StartRecording();
    }

    public void StartRecording()
    {
        if (IsRecording)
            return;

        if (!Application.isPlaying)
        {
            Debug.LogWarning("[SceneRecorder] Recording only works in Play mode.");
            return;
        }

        var size = EvenSize();
        renderTarget = new RenderTexture(size.x, size.y, 24, RenderTextureFormat.ARGB32);
        frame = new Texture2D(size.x, size.y, TextureFormat.RGBA32, false);
        frameIndex = 0;

        var format = videoFormat;
#if !UNITY_EDITOR
        if (format == VideoFormat.Video)
        {
            Debug.LogWarning("[SceneRecorder] Video encoding is Editor-only. Recording a PNG sequence instead.");
            format = VideoFormat.PngSequence;
        }
#endif

        if (format == VideoFormat.PngSequence || saveFrames)
        {
            sequenceFolder = Path.Combine(OutputDirectory(), $"{fileNamePrefix}_{Timestamp()}");
            Directory.CreateDirectory(sequenceFolder);
            if (format == VideoFormat.PngSequence)
                Debug.Log($"[SceneRecorder] Recording frames to: {sequenceFolder}");
            else
                Debug.Log($"[SceneRecorder] Also saving frame PNGs to: {sequenceFolder}");
        }
#if UNITY_EDITOR
        if (format == VideoFormat.Video)
        {
            var path = Path.Combine(OutputDirectory(), $"{fileNamePrefix}_{Timestamp()}{VideoExtension()}");
            var attributes = new VideoTrackAttributes
            {
                frameRate = new MediaRational(Mathf.Max(1, frameRate)),
                width = (uint)size.x,
                height = (uint)size.y,
                includeAlpha = false
            };

            try
            {
                encoder = new MediaEncoder(path, attributes);
            }
            catch (Exception e)
            {
                Debug.LogError($"[SceneRecorder] Could not start the encoder ({e.Message}). Switch Video Format to Png Sequence.");
                CleanUp();
                return;
            }

            Debug.Log($"[SceneRecorder] Recording to: {path}");
        }
#endif

        Time.captureFramerate = Mathf.Max(1, frameRate);

        IsRecording = true;
        recordLoop = StartCoroutine(RecordFrames());
    }

    public void StopRecording()
    {
        if (!IsRecording)
            return;

        IsRecording = false;

        if (recordLoop != null)
        {
            StopCoroutine(recordLoop);
            recordLoop = null;
        }

        var seconds = frameIndex / Mathf.Max(1f, frameRate);
        var finishedSequence = sequenceFolder;

        CleanUp();

        Debug.Log($"[SceneRecorder] Stopped after {frameIndex} frames ({seconds:0.0}s).");

        if (!string.IsNullOrEmpty(finishedSequence))
        {
            Debug.Log("[SceneRecorder] Turn the frames into a movie with:\n" +
                      $"ffmpeg -framerate {frameRate} -i \"{Path.Combine(finishedSequence, "frame_%05d.png")}\" " +
                      $"-c:v libx264 -pix_fmt yuv420p \"{finishedSequence}.mp4\"");
        }
    }

    IEnumerator RecordFrames()
    {
        var endOfFrame = new WaitForEndOfFrame();
        var frameLimit = maxSeconds > 0f ? Mathf.RoundToInt(maxSeconds * frameRate) : int.MaxValue;

        while (IsRecording)
        {
            yield return endOfFrame;

            RenderCameraTo(renderTarget);
            ReadInto(renderTarget, frame);

            if (!string.IsNullOrEmpty(sequenceFolder))
            {
                var path = Path.Combine(sequenceFolder, $"frame_{frameIndex:00000}.png");
                File.WriteAllBytes(path, frame.EncodeToPNG());
            }
#if UNITY_EDITOR
            if (encoder != null)
            {
                encoder.AddFrame(frame);
            }
#endif

            frameIndex++;

            if (frameIndex >= frameLimit)
                StopRecording();
        }
    }

    void RenderCameraTo(RenderTexture target)
    {
        var request = new RenderPipeline.StandardRequest { destination = target };

        if (RenderPipeline.SupportsRenderRequest(Cam, request))
        {
            // URP and HDRP: renders this camera on demand with post-processing,
            // without disturbing what the player sees.
            Cam.SubmitRenderRequest(request);
            return;
        }

        // Built-in pipeline.
        var previous = Cam.targetTexture;
        Cam.targetTexture = target;
        Cam.Render();
        Cam.targetTexture = previous;
    }

    void ReadInto(RenderTexture source, Texture2D destination)
    {
        var previous = RenderTexture.active;
        RenderTexture.active = source;
        destination.ReadPixels(new Rect(0, 0, source.width, source.height), 0, 0, false);
        destination.Apply(false);
        RenderTexture.active = previous;
    }

    void CleanUp()
    {
#if UNITY_EDITOR
        if (encoder != null)
        {
            encoder.Dispose();
            encoder = null;
        }
#endif
        if (renderTarget != null)
        {
            renderTarget.Release();
            SafeDestroy(renderTarget);
            renderTarget = null;
        }

        if (frame != null)
        {
            SafeDestroy(frame);
            frame = null;
        }

        sequenceFolder = null;
        Time.captureFramerate = 0;
    }

    void OnDisable()
    {
        if (IsRecording)
            StopRecording();
    }

    string OutputDirectory()
    {
        var root = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
        var directory = Path.Combine(root, string.IsNullOrWhiteSpace(outputFolder) ? "Recordings" : outputFolder);
        Directory.CreateDirectory(directory);
        return directory;
    }

    Vector2Int EvenSize()
    {
        var w = Mathf.Max(2, width);
        var h = Mathf.Max(2, height);
        return new Vector2Int(w - (w % 2), h - (h % 2));
    }

    static void SafeDestroy(UnityEngine.Object target)
    {
        if (Application.isPlaying)
            Destroy(target);
        else
            DestroyImmediate(target);
    }

    static string Timestamp()
    {
        return DateTime.Now.ToString("yyyyMMdd_HHmmss");
    }

    static string VideoExtension()
    {
#if UNITY_EDITOR_LINUX
        return ".webm";
#else
        return ".mp4";
#endif
    }

    bool WasPressed(KeyCode key)
    {
#if ENABLE_INPUT_SYSTEM
        var keyboard = Keyboard.current;
        if (keyboard == null)
            return false;

        var mapped = key == stillKey ? stillKeyNew : recordKeyNew;
        return mapped != Key.None && keyboard[mapped].wasPressedThisFrame;
#else
        return Input.GetKeyDown(key);
#endif
    }

#if ENABLE_INPUT_SYSTEM
    static Key TranslateKey(KeyCode key, Key fallback)
    {
        switch (key)
        {
            case KeyCode.Return: return Key.Enter;
            case KeyCode.LeftControl: return Key.LeftCtrl;
            case KeyCode.RightControl: return Key.RightCtrl;
        }

        if (Enum.TryParse(key.ToString(), out Key parsed))
            return parsed;

        Debug.LogWarning($"[SceneRecorder] Key '{key}' has no Input System equivalent. Using {fallback} instead.");
        return fallback;
    }
#endif

    void OnGUI()
    {
        if (!IsRecording || !showRecordingIndicator)
            return;

        indicatorStyle ??= new GUIStyle(GUI.skin.label)
        {
            fontSize = 18,
            fontStyle = FontStyle.Bold,
            normal = { textColor = Color.red }
        };

        var seconds = frameIndex / Mathf.Max(1f, frameRate);
        GUI.Label(new Rect(20, 20, 300, 30), $"\u25CF REC  {seconds:0.0}s", indicatorStyle);
    }
}
