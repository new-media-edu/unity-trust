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

namespace WorkshopAssets
{
    /// <summary>
    /// Drop this on a Camera to grab stills and record video of exactly what that
    /// camera sees, post-processing included. Press Play, then use the hotkeys.
    /// Files land in a "Recordings" folder next to your Assets folder.
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class SceneRecorder : MonoBehaviour
    {
        public enum VideoFormat
        {
            // Real video file, written by Unity's built-in encoder. Editor only.
            Video,
            // Numbered PNGs. Works everywhere, best quality, needs ffmpeg to become a movie.
            PngSequence
        }

        [Header("Hotkeys (during Play mode)")]
        [Tooltip("Saves a single PNG.")]
        public KeyCode stillKey = KeyCode.F9;
        [Tooltip("Starts and stops video recording.")]
        public KeyCode recordKey = KeyCode.F10;

        [Header("Resolution")]
        [Tooltip("Width in pixels. Ignore the Game view size, this is what you get.")]
        public int width = 1920;
        [Tooltip("Height in pixels.")]
        public int height = 1080;

        [Header("Video")]
        [Tooltip("Frames per second of the recording.")]
        public int frameRate = 30;
        [Tooltip("Video = a playable file straight out of Unity (Editor only). PngSequence = numbered frames you turn into a movie with ffmpeg.")]
        public VideoFormat videoFormat = VideoFormat.Video;
        [Tooltip("Stop automatically after this many seconds. 0 means record until you press the key again.")]
        public float maxSeconds = 0f;

        [Header("Files")]
        [Tooltip("Folder name, created next to your Assets folder.")]
        public string outputFolder = "Recordings";
        [Tooltip("Start of every filename.")]
        public string fileNamePrefix = "world";

        [Header("Feedback")]
        [Tooltip("Show a red REC dot in the Game view while recording.")]
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

        // Lazy so the Inspector context-menu items also work outside Play mode.
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
            {
                TakeStill();
            }

            if (WasPressed(recordKey))
            {
                ToggleRecording();
            }
        }

        // ------------------------------------------------------------------
        // Stills
        // ------------------------------------------------------------------

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

        // ------------------------------------------------------------------
        // Video
        // ------------------------------------------------------------------

        [ContextMenu("Toggle Recording")]
        public void ToggleRecording()
        {
            if (IsRecording)
            {
                StopRecording();
            }
            else
            {
                StartRecording();
            }
        }

        public void StartRecording()
        {
            if (IsRecording)
            {
                return;
            }

            if (!Application.isPlaying)
            {
                Debug.LogWarning("[SceneRecorder] Recording only works in Play mode. Press Play first.");
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
                // Unity's encoder ships with the Editor only, so a standalone build
                // falls back to frames on disk.
                Debug.LogWarning("[SceneRecorder] Video encoding is Editor-only. Recording a PNG sequence instead.");
                format = VideoFormat.PngSequence;
            }
#endif

            if (format == VideoFormat.PngSequence)
            {
                sequenceFolder = Path.Combine(OutputDirectory(), $"{fileNamePrefix}_{Timestamp()}");
                Directory.CreateDirectory(sequenceFolder);
                Debug.Log($"[SceneRecorder] Recording frames to: {sequenceFolder}");
            }
#if UNITY_EDITOR
            else
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

            // Locks the game clock to the capture rate, so a slow capture still
            // produces smooth, correctly timed video.
            Time.captureFramerate = Mathf.Max(1, frameRate);

            IsRecording = true;
            recordLoop = StartCoroutine(RecordFrames());
        }

        public void StopRecording()
        {
            if (!IsRecording)
            {
                return;
            }

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
                else if (encoder != null)
                {
                    encoder.AddFrame(frame);
                }
#endif

                frameIndex++;

                if (frameIndex >= frameLimit)
                {
                    StopRecording();
                }
            }
        }

        // ------------------------------------------------------------------
        // Plumbing
        // ------------------------------------------------------------------

        void RenderCameraTo(RenderTexture target)
        {
            var request = new RenderPipeline.StandardRequest { destination = target };

            if (Cam.IsRenderRequestSupported(request))
            {
                // URP / HDRP: renders this camera on demand, post-processing and all,
                // without disturbing what the player sees.
                Cam.SubmitRenderRequest(request);
                return;
            }

            // Built-in pipeline fallback.
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
            {
                StopRecording();
            }
        }

        string OutputDirectory()
        {
            // One level above Assets, so Unity never tries to import thousands of PNGs.
            var root = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
            var directory = Path.Combine(root, string.IsNullOrWhiteSpace(outputFolder) ? "Recordings" : outputFolder);
            Directory.CreateDirectory(directory);
            return directory;
        }

        Vector2Int EvenSize()
        {
            // Video encoders reject odd dimensions.
            var w = Mathf.Max(2, width);
            var h = Mathf.Max(2, height);
            return new Vector2Int(w - (w % 2), h - (h % 2));
        }

        static void SafeDestroy(UnityEngine.Object target)
        {
            if (Application.isPlaying)
            {
                Destroy(target);
            }
            else
            {
                DestroyImmediate(target);
            }
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
            {
                return false;
            }

            var mapped = key == stillKey ? stillKeyNew : recordKeyNew;
            return mapped != Key.None && keyboard[mapped].wasPressedThisFrame;
#else
            return Input.GetKeyDown(key);
#endif
        }

#if ENABLE_INPUT_SYSTEM
        static Key TranslateKey(KeyCode key, Key fallback)
        {
            // KeyCode and Key mostly share names (F9, Space, R, ...). Anything
            // exotic falls back rather than silently doing nothing.
            if (Enum.TryParse(key.ToString(), out Key parsed))
            {
                return parsed;
            }

            Debug.LogWarning($"[SceneRecorder] Key '{key}' has no Input System equivalent. Using {fallback} instead.");
            return fallback;
        }
#endif

        void OnGUI()
        {
            if (!IsRecording || !showRecordingIndicator)
            {
                return;
            }

            indicatorStyle ??= new GUIStyle(GUI.skin.label)
            {
                fontSize = 18,
                fontStyle = FontStyle.Bold,
                normal = { textColor = Color.red }
            };

            var seconds = frameIndex / Mathf.Max(1f, frameRate);
            GUI.Label(new Rect(20, 20, 300, 30), $"● REC  {seconds:0.0}s", indicatorStyle);
        }
    }
}
