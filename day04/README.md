# Day 04: Interactivity, Builds & Final Showcase

**Session Time:** 2.5 Hours

The final session is about that last 10% that makes a project feel finished. We'll add life and interaction, export standalone playable files, capture video of our worlds, and showcase them.

## Scripted Interactivity (No Coding Required)

The class files include a set of scripts in `_Workshop_Assets/Scripts/` to add life to your scene.

- **AutoRotate.cs:** Make objects (like art pieces) spin.
- **SimpleTrigger.cs:** Trigger an event (like a light turning on) when the player walks into a zone.
- **AudioCrossfader.cs:** Smoothly switch between music tracks as you enter different rooms.
- **LookAtPlayer.cs:** Make an object (like an eye or a spotlight) always face the visitor.

**How to use:** Drag the script onto an object in the Hierarchy and look at the Inspector to adjust the settings.

## Building for the Real World (Standalone Builds)

To share your environment as a "playable" experience, we need to export it from the Unity Editor.

### The Build Process
1. Go to `File > Build Settings`.
2. Ensure your current scene is in the **Scenes In Build** list.
3. Select your platform (Mac, Windows, or Linux).
4. Click **Build** and choose a folder. Unity will package everything into a single `.app` or `.exe` that anyone can run without needing Unity installed.

## Rendering & Output

To show your work outside of Unity, we need to record it. There are two ways: a script from the class files, or Unity's official Recorder package.

### Option A: SceneRecorder (class files)

`_Workshop_Assets/Scripts/SceneRecorder.cs` is a drag-and-drop recorder. Nothing to install.

1. Drag **SceneRecorder.cs** onto the camera you want to record (your flythrough camera, or the one under your First Person Controller).
2. In the Inspector, set the **Width** and **Height** (1920 x 1080 is a good default) and the **Frame Rate**.
3. Press **Play**.
4. Press **F9** for a still, or **F10** to start and stop recording. A red **● REC** dot appears while it records.
5. Find your files in a **Recordings** folder next to your Assets folder (`Show in Explorer` / `Reveal in Finder` on the project folder).

Notes:
- It captures exactly what that camera sees, at the resolution you set, no matter what size your Game view is.
- **Video Format** has two options. **Video** writes a playable `.mp4` straight out of Unity, which is what you want in class. **Png Sequence** writes numbered frames instead, for maximum quality; the Console prints the `ffmpeg` command to turn them into a movie.
- The `.mp4` option only works inside the Unity Editor. In a standalone build it automatically falls back to PNG frames.
- It records picture only, no audio.
- Set **Max Seconds** to stop automatically, handy for matching a 30-second flythrough.
- **Save Frames** writes the numbered PNGs alongside the video, so you get both from one take.

#### Assembling a video from frames

If you end up with a folder of numbered PNGs, [ffmpeg](https://ffmpeg.org/download.html) turns them into a video. Run this from inside the frame folder:

```
ffmpeg -framerate 30 -i "frame_%05d.png" -c:v libx264 -pix_fmt yuv420p output.mp4
```

Replace `30` with your frame rate, `frame_%05d.png` with the actual naming pattern of your files, and `output.mp4` with the name you want. The Console also prints this command with your own numbers filled in when a recording stops.

### Option B: Unity Recorder (official package)

More features (audio, image sequences, GIFs, Timeline integration), but a heavier setup.

1. Install **Unity Recorder** from the Package Manager.
2. Open `Window > General > Recorder > Recorder Window`.
3. Select **Movie** and choose your resolution (e.g., 1080p or 4K).
4. Set the **Recording Mode**. If your camera move is a Timeline, connect the Recorder to it so it records exactly as the cinematic plays. If you keyframed your camera in the Animation window, just record for the number of seconds your clip runs.

## Final Group Showcase

The last hour will be spent presenting our worlds to each other.

### Presentation Format
- **The Pitch:** Briefly explain the concept/narrative of your space.
- **The Walkthrough:** Play your cinematic flythrough or walk through the space live.
- **Feedback:** Share one thing you love about the environment and one technical thing you learned.

## Final Homework: Deployment & Sharing

Now that your environment is built, take it further:
1. **Interactivity:** Add at least two scripted elements (a rotating sculpture, a triggered light) to make the space feel alive.
2. **Recording:** Render a high-quality (1080p or 4K) version of your flythrough, plus a few still images for your portfolio.
3. **Standalone Build:** Attempt to "Build" your project for Mac or Windows to share it as a playable file.
4. **Documentation:** Write a short artist statement or description for your portfolio, explaining the concept behind your virtual environment.

## Beyond the Workshop

Your Unity journey doesn't end here.
- **Post-Processing:** Dig into the **Global Volume** for color grading, depth of field, and motion blur.
- **VR/AR:** Explore how to move these environments into headsets like the Meta Quest.
- **Scripting:** Start exploring C# to add even more complex logic.

**Stay in touch!** We'd love to see what you build next.
