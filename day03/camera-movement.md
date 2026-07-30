# Advanced Camera Movement (Optional)

Part of **[Day 3](README.md)**.

> [!NOTE]
> **Optional.** You can build a perfectly good flythrough by keyframing a camera in the Animation window above. Reach for this route when you want to cut between several fixed angles and have Unity blend the camera between them automatically.

**Cinemachine** lets you place a bunch of lightweight **Cinemachine Cameras** around your scene - each one is just a *shot*, a saved viewpoint (position, angle, zoom). None of them render on their own; a single real camera (the one with the **Cinemachine Brain**) follows whichever Cinemachine Camera is currently "live." **Timeline** is where you decide the order and timing of those shots, and Cinemachine automatically animates the camera between them - so overlapping two shots gives you a smooth move with no keyframing.

(These steps are for **Unity 6** with **Cinemachine 3**, which is what installs from the Package Manager today.)

## Setup
1. Install **Cinemachine** from the Package Manager (`Window > Package Manager > Unity Registry`, search "Cinemachine", **Install**).
2. Select your **Main Camera** in the Hierarchy. This is your flythrough camera, separate from the camera nested under your First Person Controller.
3. Open the Timeline window (`Window > Sequencing > Timeline`), click **Create**, and save the Timeline asset in your project. This adds a **Playable Director** component to the Main Camera and opens an empty timeline.

![Create a Director and Timeline asset](../images/timeline-create-director.png)

## Building the Shots

**One Cinemachine Camera = one camera angle. All of them go on a single Cinemachine Track.**

1. Create one **Cinemachine Camera** per shot: `GameObject > Cinemachine > Cinemachine Camera`. Rename each so you can tell them apart ("front stair view", "rear stair view", ...) and move/rotate each in the Scene view to frame that shot. The blue camera gizmo shows what it sees.
   - Creating the first one automatically adds a **Cinemachine Brain** to your Main Camera. If it doesn't, add it yourself: select the Main Camera, **Add Component > Cinemachine Brain**.

![Cinemachine Cameras placed in the scene](../images/timeline-cinemachine-cameras.png)

2. In the Timeline, click **+ > Unity.Cinemachine > Cinemachine Track**. **Do this exactly once** - a single track holds every shot.

![Add a Cinemachine Track](../images/timeline-cinemachine-track.png)

3. Bind the track. On the left edge of the track is a field reading **`None (Cinemachine Brain)`**. Click the target icon on its right and pick your **Main Camera**. It should now read **`Main Camera (Cinemachine Brain)`**.
4. Add your shots **onto that same track**: drag each Cinemachine Camera from the Hierarchy and drop it **directly on the Cinemachine Track**, one after another. Each drop becomes a shot clip aimed at that camera.
   - ⚠️ Drop them **on the track row itself**, not in the empty grey area below it. Dropping into empty space creates a **second track**, and two Cinemachine tracks fight over the same camera (this is why nothing plays right). You want **all clips on one track**.
5. Line the clips up left-to-right, with the first one starting at frame **0**. To blend from one shot to the next, drag a clip so its edge **overlaps** the neighbor - the crossed/hatched overlap is the blend, and Cinemachine smoothly flies the camera between the two angles there.

![Two shots on one track with a blend where they overlap](../images/timeline-shot-clips-blend.png)

6. **Preview it:** press the **▶ play button inside the Timeline window** (top-left, next to "Preview") and watch the **Game** tab - that's where the flythrough renders, not the Scene tab. To make it play when you press the main editor Play button instead, select the Main Camera and check **Play On Awake** on its **Playable Director** component.

> **Which camera shows up?** With two cameras in the scene, Unity renders the one with the higher **Depth** (on the Camera component). While building your flythrough, set the **Main Camera** to a higher Depth (e.g. `1` vs the player camera's `0`), or just uncheck the player camera, so the Game view shows the cutscene. If the Cinemachine Brain landed on your *player's* camera instead, remove it from there (**⋮ menu > Remove Component**) and add it to the Main Camera.
