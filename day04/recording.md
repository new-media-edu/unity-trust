# Rendering & Output

Part of **[Day 4](README.md)**.

To show your work outside of Unity, we need to record it. There are two ways: a script from the class files, or Unity's official Recorder package.

## Option A: SceneRecorder (class files)

`_Workshop_Assets/Scripts/SceneRecorder.cs` is a drag-and-drop recorder. Nothing to install.

1. Drag **SceneRecorder.cs** onto the camera you want to record (your flythrough camera, or the one under your First Person Controller).
2. In the Inspector, set the **Width** and **Height** (1920 x 1080 is a good default) and the **Frame Rate**.
3. Press **Play**.
4. Press **F9** for a still, or **F10** to start and stop recording. A red **● REC** dot appears while it records.
5. Find your files in a **Recordings** folder next to your Assets folder (`Show in Explorer` / `Reveal in Finder` on the project folder).

Notes:
- It captures exactly what that camera sees, post-processing included, at the resolution you set, no matter what size your Game view is.
- **Video Format** has two options. **Video** writes a playable `.mp4` straight out of Unity, which is what you want in class. **Png Sequence** writes numbered frames instead, for maximum quality; the Console prints the `ffmpeg` command to turn them into a movie.
- The `.mp4` option only works inside the Unity Editor. In a standalone build it automatically falls back to PNG frames.
- It records picture only, no audio.
- Set **Max Seconds** to stop automatically, handy for matching a 30-second flythrough.
- **Save Frames** writes the numbered PNGs alongside the video, so you get both from one take.

### Assembling a video from frames

If you end up with a folder of numbered PNGs, [ffmpeg](https://ffmpeg.org/download.html) turns them into a video. Run this from inside the frame folder:

```
ffmpeg -framerate 30 -i "frame_%05d.png" -c:v libx264 -pix_fmt yuv420p output.mp4
```

Replace `30` with your frame rate, `frame_%05d.png` with the actual naming pattern of your files, and `output.mp4` with the name you want. The Console also prints this command with your own numbers filled in when a recording stops.

## Option B: Unity Recorder (official package)

More features (audio, image sequences, GIFs, Timeline integration), but a heavier setup.

1. Install **Unity Recorder** from the Package Manager.
2. Open `Window > General > Recorder > Recorder Window`.
3. Select **Movie** and choose your resolution (e.g., 1080p or 4K).
4. Set the **Recording Mode**. If your camera move is a Timeline, connect the Recorder to it so it records exactly as the cinematic plays. If you keyframed your camera in the Animation window, just record for the number of seconds your clip runs.
