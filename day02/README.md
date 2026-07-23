# Day 02: Building the World (Assets & Materials)

**Session Time:** 2.5 Hours

Today we transition from a "greybox" layout to a textured, populated world. We'll focus on importing high-quality assets, mastering materials, and using modular design to build efficiently.

---

## Adding Sound

To bring your space to life, you'll need an **Audio Source**. 

<img src="../images/unity-audio_source.png" width="45%" align="right" style="margin-left: 20px;" />

1.  Right-click in the **Hierarchy** and select **Audio > Audio Source**.
2.  In the Inspector, drag a sound file (like `running_water.wav`) into the **AudioClip** slot.
3.  Set the **Spatial Blend** to **3D**. Now, the sound will grow louder as your character walks toward it.

<br clear="all" />

---

## Making Fire (Particle Systems)

Unity's built-in **Shuriken** particle system can fake a convincing campfire in a few minutes. No shaders, scripts, or downloaded assets required.

### 1. Create the Particle System
1. Right-click in the **Hierarchy** and select **Effects > Particle System**.
2. Rename it `FireParticles` and move it where you want the fire to sit.

### 2. Main Module
Select `FireParticles` and set these top-level values in the Inspector. For any field with a small dropdown arrow, choose **Random Between Two Constants** to get the ranges below.

- **Duration:** `1.00`
- **Start Lifetime:** `0.5` to `1.2`
- **Start Speed:** `1` to `3`
- **Start Size:** `0.5` to `1.5`
- **Start Color:** bright orange or red
- **Simulation Space:** `World` (so flames trail naturally if the fire moves)

<img src="../images/fire-particles-inspector.png" width="42%" align="right" style="margin-left: 20px;" />

### 3. Turn Up the Emission
Open the **Emission** module and set **Rate over Time** to `20` or more. The default trickle of particles won't read as fire - you need a steady stream to fill out the flame.

### 4. Shape the Emitter
Open the **Shape** module:
- **Shape:** `Cone`
- **Angle:** `10` (keeps the flames in a narrow rising column)
- **Radius:** `0.2` (shrinks the base to a small campfire origin)

### 5. Fade and Shrink Over Lifetime
- **Color over Lifetime** - check the box, click the gradient, and pull the rightmost **alpha** stop down to `0` so particles fade out completely.
- **Size over Lifetime** - check the box, click the curve, and pick a preset that starts at `1.0` and drops to `0.0`.

### 6. Add Rising Heat
- **Force over Lifetime** - check the box and set **Y** to `2` (higher = more aggressive flames).

<br clear="all" />

![Fire particles in the Scene view](../images/fire-particles-scene.png)

### If your particles look like pink squares
That magenta means the material isn't rendering. Open the **Renderer** module at the bottom of the component, click the circle next to **Material**, and pick a particle material. In this URP project the legacy `Default-Particle` shows up magenta, so make your own additive material instead - the [advanced fire guide](fire-particles-advanced.md) walks through it. Also confirm **Render Mode** is set to `Billboard`.

> **Want it to actually look hot?** A flat orange cone is a fine start. To push it into glowing, flickering, smoke-trailing fire, see **[Advanced Fire](fire-particles-advanced.md)**.

---

## Adding Images & Textures

Personalizing your gallery is as simple as dragging and dropping.

### Images on Walls
You can drag any image file from your **Project** window directly onto a geometric surface in the **Scene** view. Unity will automatically create a material for you.

Once you've applied an image, select the object and find its **Material** in the Inspector. Try these quick tweaks:

- **Metallic** slider - Drag right to make the surface look like metal.
- **Smoothness** slider - Higher = shiny/reflective, lower = matte/rough.
- **Base Map color** - Click the color swatch next to your texture to tint it.
- **Emission** - Check the box, pick a color, and your surface will glow.
- **Render Face** - Change from *Front* to *Both* if your surface is see-through from one side.

### Tiling & Textures
Textures are tiled by default. If your image looks too small or repetitive:
1.  Select the object.
2.  Find the **Material** in the Inspector.
3.  Adjust the **Tiling** values (X and Y) to scale the pattern.

**Bonus: Realism with Normal Maps**
To make your textures react to light and look 3D, look into **Normal Maps**. You can generate a normal map from any image using [NormalMap-Online](https://cpetry.github.io/NormalMap-Online/) and plug it into the **Normal Map** slot of your material in Unity.

For high-quality, seamless patterns (like wood, brick, or concrete), check out [Architextures](https://architextures.org/textures). You can also find free public-domain materials on [ambientCG](https://ambientcg.com/) or explore free assets on [Poliigon](https://www.poliigon.com/textures/free) (requires a free account). Alternatively, search for free materials directly in the [Unity Asset Store (make sure they are URP-compatible)](https://assetstore.unity.com/search#q=free%20materials%20urp).

![Browsing Online Textures](../images/texture-browsing.png)

---

## Mastering Materials & Shaders

Materials define how light reacts to a surface. In URP, we primarily use the **Lit** shader.

### The Anatomy of a Material
- **Base Map (Albedo):** The color or main texture of the object.
- **Normal Map:** Adds fake 3D detail (bumps, scratches) without adding geometry.
- **Metallic & Smoothness:** Controls how "shiny" or "rough" a surface looks.
- **Emission:** Makes parts of the object glow (useful for screens or neon).

### Transparency & Alpha
To create glass or fences, change the **Surface Type** from **Opaque** to **Transparent**. You can then adjust the **Alpha** channel of the Base Map color.

---

## Lighting Design

Lighting is the most powerful tool for setting a mood.

### Light Types in URP
- **Directional Light:** Your "Sun." Controls time of day and global shadows.
- **Point Light:** A bulb that radiates in all directions.
- **Spotlight:** A focused beam (perfect for gallery highlights).
- **Area Light:** Soft, window-like lighting (requires "Baking").

---

## Asset Integration: Beyond Basic Cubes

Unity supports various 3D formats, but **FBX** and **GLB/GLTF** are the most common.

### The Unity Asset Store
The [Asset Store](https://assetstore.unity.com/) is a massive library of free and paid assets.
1. Search for "Low Poly" or "Free" assets.
2. Click **Add to My Assets**.
3. In Unity, go to `Window > Package Manager`, select **My Assets**, and click **Download** then **Import**.

---

## Modular Design & Prefabs

Building a large environment piece-by-piece is slow. Instead, we use **Prefabs**.

### Creating a Prefab
1. Build an object in your scene (e.g., a lamp with a light source and a stand).
2. Drag the object from the **Hierarchy** into the **Project** folder.
3. It turns blue-this is now a Prefab!
4. You can now drag as many copies as you want into the scene. If you edit the Prefab file, **every copy** in the scene updates automatically.

### Modular Workflows
Use snapping (the magnet icon) to line up modular walls or floors perfectly. This is how professional game levels are built.

---

## Environmental Detail

### Foliage & Terrain
For natural environments, you can use Unity's **Terrain** system or simple mesh-based trees.
- Search for "Starter Assets" or "SpeedTree" samples in the Asset Store.

### Particle Effects (Simple)
Add a "mood" to your scene with basic particles.
1. Right-click > **Effects > Particle System**.
2. Try making a simple "dust" or "fog" effect by slowing down the speed and increasing the size of the particles.

---

## Photogrammetry

Photogrammetry is the process of using photos to create 3D models. We use **Polycam** to capture real-world objects. For now, download the app and start thinking about objects or spaces you'd like to scan. We'll import these scans into Unity in Session 3.

![Photogrammetry Cameras](../images/photogrammetry-cameras.png)
![Photogrammetry Diagram](../images/photogrammetry-diagram.png)

[Watch this Polycam video tutorial to see the process in action](https://www.youtube.com/watch?v=BImnzs-rR_k)

---

## Homework

1. **Polycam Scan:** Use Polycam to scan an object. It can be small or large, but make sure to follow the instructions and know that you might have to scan 2 or 3 things until one works out.
2. **Environment:** Create an environment that has both an indoor and an outdoor element. It's up to you to get as creative or practical with that as you choose. Integrate a photogrammetry scanned object into your scene.

Next session: Atmosphere and **Cinematics**.
