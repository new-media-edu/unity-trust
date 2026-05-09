# Day 02: Building the World (Assets & Materials)

**Session Time:** 2.5 Hours

Today we transition from a "greybox" layout to a textured, populated world. We'll focus on importing high-quality assets, mastering materials, and using modular design to build efficiently.

---

## 1. Asset Integration: Beyond Basic Cubes

Unity supports various 3D formats, but **FBX** and **GLB/GLTF** are the most common.

### Importing Scans (Polycam)
If you've created photogrammetry scans, now is the time to bring them in.
- Export from Polycam as `.glb` or `.obj`.
- Drag the file into your **Project** window.
- **Scaling:** Scans often come in at the wrong size. Use the **Scale Factor** in the Import Settings or the Scale tool (R) to fix it.

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

## 3. Modular Design & Prefabs

Building a large environment piece-by-piece is slow. Instead, we use **Prefabs**.

### Creating a Prefab
1. Build an object in your scene (e.g., a lamp with a light source and a stand).
2. Drag the object from the **Hierarchy** into the **Project** folder.
3. It turns blue—this is now a Prefab!
4. You can now drag as many copies as you want into the scene. If you edit the Prefab file, **every copy** in the scene updates automatically.

### Modular Workflows
Use snapping (the magnet icon) to line up modular walls or floors perfectly. This is how professional game levels are built.

---

## 4. Environmental Detail

### Foliage & Terrain
For natural environments, you can use Unity's **Terrain** system or simple mesh-based trees.
- Search for "Starter Assets" or "SpeedTree" samples in the Asset Store.

### Particle Effects (Simple)
Add a "mood" to your scene with basic particles.
1. Right-click > **Effects > Particle System**.
2. Try making a simple "dust" or "fog" effect by slowing down the speed and increasing the size of the particles.

---

## Day 2 Assignment: Populate & Texture

1. **Materials:** Apply custom materials to all your ProBuilder surfaces. Use Normal Maps for realism.
2. **Assets:** Import at least 3 external assets (scans or Asset Store) into your scene.
3. **Modularity:** Create at least one Prefab (e.g., a "Gallery Pedestal" or "Street Lamp") and reuse it.

Next session: We bring the mood with **Lighting** and **Cinematics**.
