# The Unity Interface

Part of **[Day 1](README.md)**.

When you first open the Unity Hub, click **New Project** and select the **Universal 3D** template. This uses the **Universal Render Pipeline (URP)**, which is the industry standard for performance and high-quality visual effects.

Unity's interface can look complex, but you only need five main windows to build your world: **Hierarchy**, **Project**, **Scene**, **Game**, and **Inspector**.

![Unity Views](../images/unity-views.png)

## Creating Your First Objects

Right-click in the **Hierarchy** and select **3D Object > Plane** to create a floor. Then do the same to add a **Cube** (**3D Object > Cube**).

### Precision Placement
Select the Cube and look at the **Inspector** tab on the right to see its **X, Y, and Z** coordinates. To ensure it's perfectly centered, click the three vertical dots next to the **Transform** component and select **Reset**. This snaps it to (0, 0, 0).

### Setting the View
Press the **Play** button at the top. You might notice the camera is looking at nothing. To fix this:
1.  Stop Play mode.
2.  In the **Scene view**, fly to a position where you like the view of your objects.
3.  Select the **Main Camera** in the Hierarchy.
4.  Press **Shift + Cmd + F** (Mac) or **Shift + Ctrl + F** (Windows) to **Align with View**.

## Navigating the 3D World

### How to Move Around in Unity
Right-click in the **Scene view** and use **WASD** to fly around like a video game. 

### How to Manipulate Objects
To manipulate objects, keep these shortcuts in mind:
*   **W** - Move
*   **E** – Rotate
*   **R** – Scale
*   **Y is Up** – Always remember the vertical axis is Y.

![Basic Controls](../images/unity-basic_controls.png)
![Unity XYZ](../images/unity-xyz.png)

### Navigation Tips
*   **Option + Left-click drag** - Orbit around a focal point (great for inspecting objects)
*   **Middle-click drag** - Pan the view
*   **F** - Frame/snap the view to whatever object is selected
*   **Double-click** an object in the Hierarchy to jump straight to it
*   **Scroll wheel** or **two-finger scroll** (trackpad) - Zoom in/out
*   **Cmd + D** - Duplicate selected object
*   **Cmd + Z / Cmd + Shift + Z** - Undo/redo (works on almost everything, including moving objects)

> **Note:** Flythrough mode (right-click + WASD) is the only way to get free movement in the Scene view. If you're on a trackpad and finding it difficult, plug in a mouse.

> **Deep Dive:** [Explore the Unity Editor](https://learn.unity.com/tutorial/explore-the-unity-editor-1?version=2021.3)
