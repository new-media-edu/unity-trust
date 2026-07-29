# Workshop Assets: No-Code Assembly Guide

These scripts are designed for easy, drag-and-drop behavior. Here is how to assemble them into functional "prefabs" during the workshop.

### 1. The Kinetic Sculpture (AutoRotate)
*   **Assembly:** Create a Cube or Sphere (or your scanned object). Drag the `AutoRotate.cs` script onto it in the Inspector.
*   **Settings:** Change the X, Y, or Z speed to make it rotate like a physical motor.

### 2. The Interactive Door/Video (SimpleTrigger)
*   **Assembly:** Create a **Cube**, remove its **Mesh Renderer** (so it's invisible), and check **Is Trigger** on its Collider. Drag the `SimpleTrigger.cs` script onto it.
*   **Usage:** Click the `+` on the `OnPlayerEnter` list. Drag a **Video Player** into the slot and select `VideoPlayer.Play`. Now, walking into that invisible box starts the video.

### 3. Smart Wall Labels (LookAt + DistanceFade)
*   **Assembly:** Create a **3D Object > TextMeshPro**. Drag both `LookAtPlayer.cs` and `DistanceFade.cs` onto it.
*   **Effect:** The text will now follow the player like a billboard and only appear when they are close enough to read it.

### 4. Audio Zone Blending (AudioCrossfader)
*   **Assembly:** Create an Empty Object. Add two **Audio Sources** as children. Drag the `AudioCrossfader.cs` script onto the parent object.
*   **Settings:** Drag both child Audio Sources into the script's `Source A` and `Source B` slots. 
*   **Effect:** Source A plays when you are standing right on the object; Source B takes over as you walk away. Perfect for ambient transitions.

### 5. Camera Recorder (SceneRecorder)
*   **Assembly:** Drag `SceneRecorder.cs` onto any **Camera**. That's it.
*   **Usage:** Press **Play**, then **F9** for a still image and **F10** to start/stop recording video. A red **● REC** dot shows while it's running.
*   **Output:** Files appear in a `Recordings` folder next to your `Assets` folder. Set **Width**/**Height** in the Inspector to record at a proper resolution regardless of your Game view size.
*   **Formats:** **Video** gives you a finished `.mp4` (Editor only). **Png Sequence** gives you numbered frames plus an `ffmpeg` command in the Console for stitching them together. No audio either way.

### 6. Standard Gallery Plinths
*   **Assembly:** Use **ProBuilder** to create a cube. Set its dimensions in the Inspector to standard gallery sizes (0.4m x 0.4m x 1.0m). 
*   **Tip:** Once you have one, drag it from the Hierarchy into your Project folder to create a reusable **Prefab**.
