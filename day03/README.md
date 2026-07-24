# Day 03: Atmosphere & Cinematic Storytelling

**Session Time:** 2.5 Hours

Today we transform your environment into a living, breathing world through advanced lighting and cinematic presentation.

---

## Bringing the Real World In: Importing Scans

If you created photogrammetry scans with Polycam, now is the time to bring them in.
- Export from Polycam as `.glb` or `.obj`.
- Drag the file into your **Project** window.
- **Scaling:** Scans often come in at the wrong size. Use the **Scale Factor** in the Import Settings or the Scale tool (R) to fix it.

---

## Lighting Design

Lighting is the most powerful tool for setting a mood. In Day 02 you dropped in one of each light type to see what they do - now we'll use them intentionally.

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
- **Skybox as a light source.** Your skybox actually lights the scene. Swapping to a sunset or overcast sky in the Lighting window changes the whole mood for free.
- **Soft shadows.** On a light, set **Shadow Type** to **Soft Shadows**. Hard shadows look like a video game; soft edges look grounded.
- **Fewer, stronger lights.** Resist scattering dozens of dim lights. One or two strong key lights plus a soft fill almost always beats a room full of weak ones - and it runs faster.
- **Fog for atmosphere.** In the Lighting window's **Environment** tab, enable **Fog**. A subtle color-matched fog adds depth and hides where your geometry ends.

> **Bloom makes lights glow.** To get that soft glow around bright lights and emissive surfaces, add a **Global Volume** (`GameObject > Volume > Global Volume`), add a **Bloom** override, and check **Post Processing** on your Camera. See the [advanced fire guide](../day02/fire-particles-advanced.md#6-bloom-makes-it-glow) for the full steps.

### Going Further
Want richer, higher-quality lighting - baked global illumination, light cookies, real lights attached to particles (like a flickering campfire that lights the room), reflections, and how the **Volume** system actually fits in? See the **[Advanced Lighting guide](lighting-advanced.md)**.

---

## Advanced Atmosphere

### Volumetric Effects (Unity 6)
Unity 6 introduces enhanced **Volumetric Fog and Clouds**.
1. Select your **Global Volume**.
2. Add the **Fog** override. 
3. Enable **Volumetric Fog** to see light beams (god rays) cutting through the air.

### Atmospheric Effects & Post-Processing
Your scene automatically includes a **Global Volume**. Think of this as a cinematic filter for your camera. Select it in the Hierarchy, find the **Vignette** effect in the Inspector, and try increasing the **Intensity**. This darkens the edges of the screen for a focused, gallery-like feel.

---

## Mood & Reflection

### Skyboxes
The Skybox provides the background and the "ambient" light. 
- Go to `Window > Rendering > Lighting`.
- In the **Environment** tab, you can swap the **Skybox Material**.
- Pro Tip: Search for "HDRIs" to get realistic 360-degree backgrounds.

### Light Baking & Emissive Materials
Baking (pre-calculating) your lighting allows you to create realistic, soft bounced light and shadows—including glowing light from emissive materials—without impact on runtime performance.

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
   - Click the **Generate Lighting** button at the bottom-right corner. Unity will calculate and bake the lightmaps in the background.

### Reflection & Light Probes
- **Reflection Probes:** Capture the surroundings to make metallic objects look realistic.
- **Light Probes:** Allow moving objects (like your player) to receive light from "baked" sources.

---

## Cinematics with Timeline & Cinemachine

We’ll use **Timeline** to create a cinematic "flythrough" of your space.

### Setup
1. Install **Cinemachine** from the Package Manager.
2. Open the **Timeline Window** (`Window > Sequencing > Timeline`).
3. Create an empty object called "Cutscene" and drag it into the Timeline window to create a new Director component.

### Directing the Camera
- Add a **Cinemachine Track**.
- Create a **Virtual Camera** (Vcam) for each shot.
- Use the **Timeline** to blend between cameras. Unity will automatically smooth the movement between them!

---

## Scripted Interactivity (No Coding Required)

The class files include a set of scripts in `_Workshop_Assets/Scripts/` to add life to your scene.

- **AutoRotate.cs:** Make objects (like art pieces) spin.
- **SimpleTrigger.cs:** Trigger an event (like a light turning on) when the player walks into a zone.
- **AudioCrossfader.cs:** Smoothly switch between music tracks as you enter different rooms.
- **LookAtPlayer.cs:** Make an object (like an eye or a spotlight) always face the visitor.

**How to use:** Drag the script onto an object in the Hierarchy and look at the Inspector to adjust the settings.

---

## Homework: Atmosphere & Pacing

Prepare your project for the final showcase.
1. **Refine Lighting & Sound:** Dial in your mood. Use Volumetric Fog for visual density and ambient wind/audio to ground the space.
2. **Interactivity:** Add at least two scripted elements (e.g., a rotating sculpture or a triggered event) to make the space feel alive.
3. **Cinematic Cutscene:** Refine your 30-second Timeline flythrough. Focus on smooth camera transitions and interesting angles.

Next session: **Post-Processing, Builds, and Showcase.**
