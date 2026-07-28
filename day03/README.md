# Day 03: Atmosphere & Cinematic Storytelling

**Session Time:** 2.5 Hours

Today we transform your environment into a living, breathing world. Everything in this session is about **mood**: how light, sky, fog, and movement make a space feel like somewhere rather than something.

**Today's arc:**
1. [Importing Scans](#bringing-the-real-world-in-importing-scans) - bring your Polycam captures into the scene.
2. [Lighting Design](#lighting-design) - the single biggest lever on how your world feels.
3. [Sky, Fog & Volume](#setting-the-mood-sky-fog--volume) - the atmosphere your lights sit inside.
4. [Baking & Probes](#going-further-baking--probes) - higher-quality lighting, if you have time.
5. [Basic Animation](#basic-animation) - keyframing objects (and cameras) to move.
6. [Advanced Camera Movement](#advanced-camera-movement-optional) - *optional* Timeline and Cinemachine flythroughs.

## Bringing the Real World In: Importing Scans

If you created photogrammetry scans with Polycam, now is the time to bring them in.

1. Export from Polycam as `.glb` or `.obj`.
2. Drag the file into your **Project** window.
3. Drag it from the Project window into the **Scene**.
4. **Scaling:** Scans often come in at the wrong size. Use the **Scale Factor** in the Import Settings, or the Scale tool (`R`), to fix it.

![A Polycam scan of a Berlin trash can imported into Unity](../images/polycam-scan-imported.png)

Scans arrive as a single lumpy mesh with the ground they were standing on still attached. That ragged base is normal. Sink it slightly into your floor, or hide the seam with other geometry.

## Lighting Design

Lighting is the most powerful tool for setting a mood. Last session we experimented with the different light types, now we'll use them more intentionally.

### Light Types in URP
- **Directional Light:** Your "Sun." Controls time of day and global shadows.
- **Point Light:** A bulb that radiates in all directions.
- **Spotlight:** A focused beam (perfect for gallery highlights).
- **Area Light:** Soft, window-like lighting (requires "Baking").

### Simple Tips for Better Lighting
A few small moves make a scene look intentional instead of flat:

- **Warm vs. cool.** Give lights a color instead of pure white. Warm oranges (interiors, fire, sunset) against cool blues (shadows, sky, moonlight) instantly add depth. Set the **Color** on each light in the Inspector.
- **Turn down the sun.** A Directional Light at full **Intensity** blows everything out. Dropping it to `0.5`-`1.0` and letting smaller point/spot lights do the accent work reads far more cinematic.
- **Adjust the Environment (ambient) light.** Open **Window > Rendering > Lighting**. The **Environment** tab controls the flat "fill" light in shadows. A dim, colored ambient keeps shadows from turning pure black.
- **Soft shadows.** On a light, set **Shadow Type** to **Soft Shadows**. Hard shadows look like a video game; soft edges look grounded.
- **Fewer, stronger lights.** Resist scattering dozens of dim lights. One or two strong key lights plus a soft fill almost always beats a room full of weak ones - and it runs faster.

> **Bloom makes lights glow.** To get that soft glow around bright lights and emissive surfaces, add a **Global Volume** (`GameObject > Volume > Global Volume`), add a **Bloom** override, and check **Post Processing** on your Camera. See the [advanced fire guide](../day02/fire-particles-advanced.md#6-bloom-makes-it-glow) for the full steps.

## Setting the Mood: Sky, Fog & Volume

Your lights are only half of it. The sky and the air between things do the rest.

### Skyboxes
The Skybox provides the background *and* the ambient light that fills your shadows, so changing it changes the whole scene for free.

1. Go to `Window > Rendering > Lighting` and open the **Environment** tab.
2. Click the small circle next to **Skybox Material** to swap it.

**Download some skyboxes and try them.** A great free starting set is [AllSky Free - 10 Sky / Skybox Set](https://assetstore.unity.com/packages/2d/textures-materials/sky/allsky-free-10-sky-skybox-set-146014) on the Asset Store.
1. Open the link, click **Add to My Assets**, then **Open in Unity**.
2. In Unity's **Package Manager** (`Window > Package Manager`), find **AllSky Free** under **My Assets** and click **Download**, then **Import**.
3. Back in the Lighting window, pick one of the imported skies.
4. Cycle through a few - a sunset, an overcast day, a night sky - and watch how the mood (and the ambient light) changes.

> **Pro Tip:** Search for "HDRIs" to find realistic 360-degree backgrounds beyond this pack.

### Fog
In **URP** (the pipeline this workshop uses), fog is not a Volume override - it lives in the Lighting window next to the skybox settings.

1. Open `Window > Rendering > Lighting` and go to the **Environment** tab.
2. Scroll down and enable **Fog**.
3. Set the **Mode** (Linear or Exponential), pick a **Color** that matches your skybox, and tune the density/distance until the far edges of your scene fade softly into the haze.

Color-matched fog is one of the fastest ways to add depth and hide where your geometry ends.

> **Note:** True *volumetric* fog with visible light beams (god rays) is an **HDRP** feature, not URP. If you want those, that's a reason to explore HDRP later - but the Environment-tab fog above covers most gallery moods.

### The Global Volume
Your scene automatically includes a **Global Volume**. Think of this as a cinematic filter sitting on your camera. Select it in the Hierarchy, find the **Vignette** effect in the Inspector, and try raising the **Intensity**. This darkens the edges of the screen for a focused, gallery-like feel.

We'll go much deeper on the Volume system (color grading, tone mapping, depth of field) next session.

## Going Further: Baking & Probes

Optional, and worth it if your scene is mostly static. Skip to [Basic Animation](#basic-animation) if you're short on time.

### Light Baking & Emissive Materials
Baking (pre-calculating) your lighting gives you realistic, soft bounced light and shadows - including glowing light from emissive materials - with no runtime performance cost.

1. **Create an Emissive Material:**
   - Select your material in the Project window.
   - In the Inspector, check the **Emission** box.
   - Choose a color/intensity (tint) or drag in your emissive texture.
   - Ensure the **Global Illumination** dropdown on the material is set to **Baked**.
2. **Mark Objects as Static:**
   - Select any non-moving geometry (walls, floors, props, or glowing neon lights) in the Hierarchy.
   - In the top-right of the Inspector, check the **Static** box (this flags the object to contribute to Global Illumination).
3. **Generate the Lighting:**
   - Go to **Window > Rendering > Lighting**.
   - Click **Generate Lighting** at the bottom-right. Unity bakes the lightmaps in the background.

### Reflection & Light Probes
- **Reflection Probes:** Capture the surroundings to make metallic objects look realistic.
- **Light Probes:** Allow moving objects (like your player) to receive light from baked sources.

### The Full Guide
Want richer, higher-quality lighting - baked global illumination, light cookies, real lights attached to particles (like a flickering campfire that lights the room), reflections, and how the **Volume** system actually fits in? See the **[Advanced Lighting guide](lighting-advanced.md)**.

## Basic Animation

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

### How time works here
The ruler across the top is your timeline, and the **Samples** rate (top-left, default **60**) is how many frames make up one second.
- So a keyframe at frame **60** happens **1 second** in; frame **120** is 2 seconds, and so on. Click the time readout to toggle the ruler between **frames** and **seconds:frames**.
- **Speed = spacing.** The farther apart two keyframes sit, the slower the motion between them. Want it faster? Drag the keyframes closer together. Slower? Spread them out. (Lowering **Samples** stretches everything out; raising it packs it in.)
- **Looping:** clips loop by default. To make it play once, select the `.anim` file in the Project window and uncheck **Loop Time** in the Inspector.

> **This works on cameras too.** Select a camera instead of a cube, add **Transform > Position** and **Rotation**, and keyframe it moving through your space. That's a flythrough, with nothing else to install. The section below is the more powerful (and more complicated) alternative.

## Advanced Camera Movement (Optional)

> [!NOTE]
> **Optional.** You can build a perfectly good flythrough by keyframing a camera in the Animation window above. Reach for this route when you want to cut between several fixed angles and have Unity blend the camera between them automatically.

**Cinemachine** lets you place a bunch of lightweight **Cinemachine Cameras** around your scene - each one is just a *shot*, a saved viewpoint (position, angle, zoom). None of them render on their own; a single real camera (the one with the **Cinemachine Brain**) follows whichever Cinemachine Camera is currently "live." **Timeline** is where you decide the order and timing of those shots, and Cinemachine automatically animates the camera between them - so overlapping two shots gives you a smooth move with no keyframing.

(These steps are for **Unity 6** with **Cinemachine 3**, which is what installs from the Package Manager today.)

### Setup
1. Install **Cinemachine** from the Package Manager (`Window > Package Manager > Unity Registry`, search "Cinemachine", **Install**).
2. Select your **Main Camera** in the Hierarchy. This is your flythrough camera, separate from the camera nested under your First Person Controller.
3. Open the Timeline window (`Window > Sequencing > Timeline`), click **Create**, and save the Timeline asset in your project. This adds a **Playable Director** component to the Main Camera and opens an empty timeline.

![Create a Director and Timeline asset](../images/timeline-create-director.png)

### Building the Shots

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

## Homework: Atmosphere & Pacing

Prepare your project for the final showcase.
1. **Refine Lighting:** Dial in your mood. Use colored key lights, a matching skybox, and fog for visual density.
2. **Add Sound:** Ground the space with ambient audio (wind, room tone, water) using the Audio Sources from Day 2.
3. **Camera Move:** Build a 30-second flythrough of your space, either by keyframing a camera in the Animation window or with Timeline and Cinemachine. Focus on smooth movement and interesting angles.

Next session: **Post-Processing, Builds, and Showcase.**
