# Lighting, Sky & Atmosphere

Part of **[Day 3](README.md)**.

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

The Volume can do a lot more (color grading, tone mapping, depth of field). The **[Advanced Lighting guide](lighting-advanced.md)** covers how the Volume system fits together.

## Going Further: Baking & Probes

Optional, and worth it if your scene is mostly static. Skip to [Basic Animation](animation.md) if you're short on time.

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
