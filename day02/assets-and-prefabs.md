# Assets, Prefabs & Environmental Detail

Part of **[Day 2](README.md)**.

Unity supports various 3D formats, but **FBX** and **GLB/GLTF** are the most common.

### The Unity Asset Store
The [Asset Store](https://assetstore.unity.com/) is a massive library of free and paid assets.
1. Search for "Low Poly" or "Free" assets.
2. Click **Add to My Assets**.
3. In Unity, go to `Window > Package Manager`, select **My Assets**, and click **Download** then **Import**.

## Modular Design & Prefabs

Building a large environment piece-by-piece is slow. Instead, we use **Prefabs**.

### Creating a Prefab
1. Build an object in your scene (e.g., a lamp with a light source and a stand).
2. Drag the object from the **Hierarchy** into the **Project** folder.
3. It turns blue-this is now a Prefab!
4. You can now drag as many copies as you want into the scene. If you edit the Prefab file, **every copy** in the scene updates automatically.

### Modular Workflows
Use snapping (the magnet icon) to line up modular walls or floors perfectly. This is how professional game levels are built.

## Environmental Detail

### Foliage & Terrain
For natural environments, you can use Unity's **Terrain** system or simple mesh-based trees.
- Search for "Starter Assets" or "SpeedTree" samples in the Asset Store.

### Particle Effects (Simple)
Add a "mood" to your scene with basic particles.
1. Right-click > **Effects > Particle System**.
2. Try making a simple "dust" or "fog" effect by slowing down the speed and increasing the size of the particles.
