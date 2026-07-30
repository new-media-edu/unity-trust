# Basic Animation

Part of **[Day 3](README.md)**.

Unity's **Animation** window lets you keyframe almost anything - position, rotation, scale, colors, light intensity - to make a door swing, a sculpture spin, a platform rise, or a **camera fly through your space**.

1. Open the Animation window: **Window > Animation > Animation** (or press `Ctrl/Cmd + 6`). Dock it along the bottom next to your Project tab.
2. In the **Hierarchy**, click the object you want to animate (say, a **Cube**).
3. With the object selected, the Animation window shows a **Create** button. Click it and save the new **Animation Clip** (`.anim`) into your project. This also adds an **Animator** component to the object - that's what plays the clip.
4. Click **Add Property** and choose what you want to animate. For a moving object, add **Transform > Position** and **Transform > Rotation** (each has a **+** to add it). They now appear as rows on the left.

![Animation window with Position and Rotation properties on a Cube](../images/animation-window-keyframes.png)

5. Now keyframe the motion:
   - Make sure **record mode** is on - the round **red button** at the top-left of the Animation window is lit.
   - Leave the playhead (the white line) at **0**, this is your starting pose.
   - Scrub the playhead **forward** along the Animation ruler to a later time (drag the white line, or type a number in the frame field).
   - **Move or rotate the object** in the Scene view to where you want it at that moment, then press **K** to drop a **keyframe** (a diamond appears on the row). In record mode, moving the object often keyframes it automatically too, but `K` is the reliable way to force one.
   - Repeat: scrub forward, reposition, press **K**. Unity fills in the in-between motion for you (this is called *tweening*).
   - Press the **▶** in the Animation window to preview. Turn **off** the red record button when you're done so you don't keyframe by accident.

## How time works here
The ruler across the top is your timeline, and the **Samples** rate (top-left, default **60**) is how many frames make up one second.
- So a keyframe at frame **60** happens **1 second** in; frame **120** is 2 seconds, and so on. Click the time readout to toggle the ruler between **frames** and **seconds:frames**.
- **Speed = spacing.** The farther apart two keyframes sit, the slower the motion between them. Want it faster? Drag the keyframes closer together. Slower? Spread them out. (Lowering **Samples** stretches everything out; raising it packs it in.)
- **Looping:** clips loop by default. To make it play once, select the `.anim` file in the Project window and uncheck **Loop Time** in the Inspector.

> **This works on cameras too.** Select a camera instead of a cube, add **Transform > Position** and **Rotation**, and keyframe it moving through your space. That's a flythrough, with nothing else to install. The section below is the more powerful (and more complicated) alternative.
