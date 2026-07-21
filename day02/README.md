# Day 02: Building the World (Assets & Materials)

**Session Time:** 2.5 Hours

Today we transition from a "greybox" layout to a textured, populated world. We'll focus on importing high-quality assets, mastering materials, and using modular design to build efficiently.

---

## 1. Asset Integration: Beyond Basic Cubes

Unity supports various 3D formats, but **FBX** and **GLB/GLTF** are the most common.

### Introduction to Polycam & Photogrammetry
Photogrammetry is the process of using photos to create 3D models. We'll be using **Polycam** to capture real-world objects. For now, download the app and start thinking about objects or spaces you'd like to scan. We'll import these scans into Unity in Session 3.

### The Unity Asset Store
The [Asset Store](https://assetstore.unity.com/) is a massive library of free and paid assets.
1. Search for "Low Poly" or "Free" assets.
2. Click **Add to My Assets**.
3. In Unity, go to `Window > Package Manager`, select **My Assets**, and click **Download** then **Import**.

---

## 2. Mastering Materials & Shaders

Materials define how light reacts to a surface. In URP, we primarily use the **Lit** shader.

### The Anatomy of a Material
- **Base Map (Albedo):** The color or main texture of the object.
- **Normal Map:** Adds fake 3D detail (bumps, scratches) without adding geometry.
- **Metallic & Smoothness:** Controls how "shiny" or "rough" a surface looks.
- **Emission:** Makes parts of the object glow (useful for screens or neon).

### Transparency & Alpha
To create glass or fences, change the **Surface Type** from **Opaque** to **Transparent**. You can then adjust the **Alpha** channel of the Base Map color.

---

## 3. Lighting Design

Lighting is the most powerful tool for setting a mood.

### Light Types in URP
- **Directional Light:** Your "Sun." Controls time of day and global shadows.
- **Point Light:** A bulb that radiates in all directions.
- **Spotlight:** A focused beam (perfect for gallery highlights).
- **Area Light:** Soft, window-like lighting (requires "Baking").

---

## 4. Modular Design & Prefabs

Building a large environment piece-by-piece is slow. Instead, we use **Prefabs**.

### Creating a Prefab
1. Build an object in your scene (e.g., a lamp with a light source and a stand).
2. Drag the object from the **Hierarchy** into the **Project** folder.
3. It turns blue-this is now a Prefab!
4. You can now drag as many copies as you want into the scene. If you edit the Prefab file, **every copy** in the scene updates automatically.

### Modular Workflows
Use snapping (the magnet icon) to line up modular walls or floors perfectly. This is how professional game levels are built.

---

## 5. Environmental Detail

### Foliage & Terrain
For natural environments, you can use Unity's **Terrain** system or simple mesh-based trees.
- Search for "Starter Assets" or "SpeedTree" samples in the Asset Store.

### Particle Effects (Simple)
Add a "mood" to your scene with basic particles.
1. Right-click > **Effects > Particle System**.
2. Try making a simple "dust" or "fog" effect by slowing down the speed and increasing the size of the particles.

---

## Homework: Populating the World

Before Session 3, continue developing the visual density of your space.
1. **Texturing:** Finish applying custom materials to all surfaces. Ensure you are using Normal Maps for added detail.
2. **Polycam Scans:** Use the Polycam app to scan at least 2 real-world objects. We'll import these next time.
3. **Modularity:** Create and place at least one custom Prefab to build out your scene efficiently.

Next session: We bring the mood with **Lighting** and **Cinematics**.
