# Day 01: Foundations & Spatial Sketching

**Session Time:** 2.5 Hours

Welcome to your first steps in Unity. Today, we're moving from a blank slate to a walkable 3D gallery. We'll focus on the interface, basic physics, and building custom architecture.

---

## 📥 Download Class Files

Before we begin, download the class files we'll be using throughout the workshop:

**[⬇ Download Class Files (.zip)](https://github.com/new-media-edu/unity-trust/archive/refs/heads/main.zip)**

> [!NOTE]
> These files may be updated between sessions. If you're told to re-download, simply grab the latest `.zip` from the link above and replace your old copy - you can safely delete the previous download.

---

## 1. Foundation: Project Setup

When you first open the Unity Hub, click **New Project** and select the **Universal 3D** template. This uses the **Universal Render Pipeline (URP)**, which is the industry standard for performance and high-quality visual effects.

---

Unity's interface can look complex, but you only need five main windows to build your world: **Hierarchy**, **Project**, **Scene**, **Game**, and **Inspector**.

![Unity Views](../images/unity-views.png)

---

## 2. Creating Your First Objects

Right-click in the **Hierarchy** and select **3D Object > Plane** to create a floor. Then do the same to add a **Cube** (**3D Object > Cube**).

### Precision Placement
Select the Cube and look at the **Inspector** tab on the right to see its **X, Y, and Z** coordinates. To ensure it's perfectly centered, click the three vertical dots next to the **Transform** component and select **Reset**. This snaps it to (0, 0, 0).

### Setting the View
Press the **Play** button at the top. You might notice the camera is looking at nothing. To fix this:
1.  Stop Play mode.
2.  In the **Scene view**, fly to a position where you like the view of your objects.
3.  Select the **Main Camera** in the Hierarchy.
4.  Press **Shift + Cmd + F** (Mac) or **Shift + Ctrl + F** (Windows) to **Align with View**.

---

## 3. Navigating the 3D World

### Mastering Movement
Right-click in the **Scene view** and use **WASD** to fly around like a video game. 

### Mastering Object Manipulation
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

---

## 4. Physics & Atmosphere

### Gravity in Action
Add a **Sphere** and position it directly above your Cube. With the sphere selected, click **Add Component** in the Inspector and search for **Rigidbody**. To make it more interesting, select your Cube and use the **Rotate tool (E)** to tilt it slightly. Hit **Play** and watch the sphere react to gravity!

### Atmospheric Effects
Your scene automatically includes a **Global Volume**. Think of this as a cinematic filter for your camera. Select it in the Hierarchy, find the **Vignette** effect in the Inspector, and try increasing the **Intensity**. This darkens the edges of the screen for a focused, gallery-like feel.

---

## 5. Architectural Sketching with ProBuilder

We’ll install **ProBuilder** together via `Window > Package Manager` (Search the Unity Registry). This tool allows you to build walls, stairs, and pedestals directly inside Unity.

![ProBuilder Installation](../images/pro-builder-install.png)

### Setup for Precision
Before building, look at the top of the Scene view:
1.  **Enable Grid Snapping** (the magnet icon) so your walls line up perfectly.
2.  **Set Tool Handle Rotation to Global** (using the drop-down menu) to move objects easily along the main axes.

![Unity Snapping](../images/unity-snapping.png)
![Global Handle](../images/unity-global-handle.png)

### Building Your Room
1.  Open the **ProBuilder Window** (`Tools > ProBuilder > ProBuilder Window`).
2.  Click **New Shape** and try a **Cube** (for floors), **Stairs**, or a **Pipe**. Pay attention to the **Shape Settings** window to adjust steps or thickness.
3.  To sketch walls, create a flat floor Cube, then click the **ProBuilder Edit button** (above the Hand tool).
4.  Switch to **Face Selection** (orange icon), select the edges of your floor, and hold **Shift** while dragging up to **extrude** your walls.

### Creative Experimentation
Try selecting a face and using the **Scale tool (R)** while holding **Shift**. This "insets" the face, creating a new surface inside the original one-perfect for windows or architectural details.

<img src="../images/unity-probuilder-inset1.png" width="48%" /> <img src="../images/unity-probuilder-inset2.png" width="48%" />

---

## 6. Walking Through the Space

To experience your gallery as a visitor, we'll add a first-person controller.

<img src="../images/unity-asset_store1.png" width="48%" align="right" style="margin-left: 20px;" />

1.  **Install:** Search for the [Mini First Person Controller](https://assetstore.unity.com/packages/tools/input-management/mini-first-person-controller-174710) in the Asset Store and click **Open in Unity**.
2.  **Import:** In the Unity Package Manager, click **Download**, then **Import**. If a window pops up asking to backup scripts, select **No**.
3.  **Setup:** In your Project window, navigate to `Mini First Person Controller` and drag the **First Person Controller.prefab** into your scene. 
4.  **Finalize:** **Delete the default Main Camera** in your Hierarchy, as the controller has its own camera.

![Mini First Person Prefab](../images/unity-mini-first.png)

Hit **Play**! You can now walk through your gallery using **WASD** and look around with your **Mouse**. If you need to stop or use your cursor, just hit **Escape** to get control of your mouse back.

Your scene might look something like this:

![Complete Scene](../images/unity-complete_scene.png)

### Adding Sound
To bring your space to life, you'll need an **Audio Source**. 

<img src="../images/unity-audio_source.png" width="45%" align="right" style="margin-left: 20px;" />

1.  Right-click in the **Hierarchy** and select **Audio > Audio Source**.
2.  In the Inspector, drag a sound file (like `running_water.wav`) into the **AudioClip** slot.
3.  Set the **Spatial Blend** to **3D**. Now, the sound will grow louder as your character walks toward it.

<br clear="all" />

---

## 7. Adding Images & Textures

Personalizing your gallery is as simple as dragging and dropping.

### Images on Walls
You can drag any image file from your **Project** window directly onto a geometric surface in the **Scene** view. Unity will automatically create a material for you.

### Tiling & Textures
Textures are tiled by default. If your image looks too small or repetitive:
1.  Select the object.
2.  Find the **Material** in the Inspector.
3.  Adjust the **Tiling** values (X and Y) to scale the pattern.

**Bonus: Realism with Normal Maps**
To make your textures react to light and look 3D, look into **Normal Maps**. You can generate a normal map from any image using [NormalMap-Online](https://cpetry.github.io/NormalMap-Online/) and plug it into the **Normal Map** slot of your material in Unity.

For high-quality, seamless patterns (like wood, brick, or concrete), check out [Architextures](https://architextures.org/textures).

---

## 8. Homework: Environment Foundations

Your goal before the next session is to finish the core layout of your environment.
1. **Architecture:** Use ProBuilder to finalize your floor, walls, and main focal points.
2. **Audio:** Incorporate at least one **3D Audio Source** (e.g., ambient wind, water, or mechanical hum).
3. **Imagery:** Import and place at least one **outside image** (JPG/PNG) on a wall or surface.
4. **Navigation:** Ensure your First Person Controller can walk through the entire space smoothly.

[Click here for some short video tutorials on ProBuilder](https://unity.com/features/probuilder)