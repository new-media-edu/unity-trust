# Architectural Sketching with ProBuilder

Part of **[Day 1](README.md)**.

We’ll install **ProBuilder** together via `Window > Package Manager` (Search the Unity Registry). This tool allows you to build walls, stairs, and pedestals directly inside Unity.

![ProBuilder Installation](../images/pro-builder-install.png)

## Setup for Precision
Before building, look at the top of the Scene view:
1.  **Enable Grid Snapping** (the magnet icon) so your walls line up perfectly.
2.  **Set Tool Handle Rotation to Global** (using the drop-down menu) to move objects easily along the main axes.
3.  **Set Active Context:** You must select the **ProBuilder Active Context** (the cube with nodes icon, usually found on the top left or top middle of the Scene View) in order to access ProBuilder's Vertex, Edge, and Face selection modes. *(Note: no image provided for this yet).*

![Unity Snapping](../images/unity-snapping.png)
![Global Handle](../images/unity-global-handle.png)

## Building Your Room
1.  Open the **ProBuilder Window** (`Tools > ProBuilder > ProBuilder Window`).
2.  Click **New Shape** and try a **Cube** (for floors), **Stairs**, or a **Pipe**. Pay attention to the **Shape Settings** window to adjust steps or thickness.
3.  Switch to **Face Selection** (orange icon in the ProBuilder toolbar or Scene view overlay).
4.  Select a face of your shape (e.g., the top of a floor cube).

## Creative Experimentation & ProBuilder Shortcuts
The real power of ProBuilder is that you can manipulate individual faces, edges, and vertices:
*   **Extrude (Move):** Select a face, hold **Shift**, and drag with the **Move tool (W)**. Instead of just stretching the face, this extrudes a *new* block out of the surface (perfect for drawing walls).
*   **Extrude (Rotate):** Select a face, hold **Shift**, and drag with the **Rotate tool (E)**. This twists and extrudes the geometry outward.
*   **Inset (Scale):** Select a face, hold **Shift**, and drag with the **Scale tool (R)**. This creates a new, smaller face inside the original one-perfect for making window frames.
*   **Delete Faces:** Select a face and press **Backspace / Delete** to cut a hole into your shape.
*   **Exit Edit Mode:** Press **ESC** to stop manipulating the current ProBuilder shape and return to object selection.

<img src="../images/unity-probuilder-inset1.png" width="48%" /> <img src="../images/unity-probuilder-inset2.png" width="48%" />
