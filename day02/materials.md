# Materials, Images & Textures

Part of **[Day 2](README.md)**.

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

## Mastering Materials & Shaders

Materials define how light reacts to a surface. In URP, we primarily use the **Lit** shader.

### The Anatomy of a Material
- **Base Map (Albedo):** The color or main texture of the object.
- **Normal Map:** Adds fake 3D detail (bumps, scratches) without adding geometry.
- **Metallic & Smoothness:** Controls how "shiny" or "rough" a surface looks.
- **Emission:** Makes parts of the object glow (useful for screens or neon).

### Transparency & Alpha
To create glass or fences, change the **Surface Type** from **Opaque** to **Transparent**. You can then adjust the **Alpha** channel of the Base Map color.
