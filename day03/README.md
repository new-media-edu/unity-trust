# Day 03: Atmosphere & Cinematic Storytelling

**Session Time:** 2.5 Hours

Today we transform your environment into a living, breathing world through advanced lighting and cinematic presentation.

---

## 1. Advanced Lighting Design

Lighting is the most powerful tool for setting a mood.

### Light Types in URP
- **Directional Light:** Your "Sun." Controls time of day and global shadows.
- **Point Light:** A bulb that radiates in all directions.
- **Spotlight:** A focused beam (perfect for gallery highlights).
- **Area Light:** Soft, window-like lighting (requires "Baking").

### Volumetric Effects (Unity 6)
Unity 6 introduces enhanced **Volumetric Fog and Clouds**.
1. Select your **Global Volume**.
2. Add the **Fog** override. 
3. Enable **Volumetric Fog** to see light beams (god rays) cutting through the air.

---

## 2. Mood & Reflection

### Skyboxes
The Skybox provides the background and the "ambient" light. 
- Go to `Window > Rendering > Lighting`.
- In the **Environment** tab, you can swap the **Skybox Material**.
- Pro Tip: Search for "HDRIs" to get realistic 360-degree backgrounds.

### Reflection & Light Probes
- **Reflection Probes:** Capture the surroundings to make metallic objects look realistic.
- **Light Probes:** Allow moving objects (like your player) to receive light from "baked" sources.

---

## 3. Cinematics with Timeline & Cinemachine

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

## 4. Scripted Interactivity (No Coding Required)

We've provided a set of scripts in `_Workshop_Assets/Scripts/` to add life to your scene.

- **AutoRotate.cs:** Make objects (like art pieces) spin.
- **SimpleTrigger.cs:** Trigger an event (like a light turning on) when the player walks into a zone.
- **AudioCrossfader.cs:** Smoothly switch between music tracks as you enter different rooms.
- **LookAtPlayer.cs:** Make an object (like an eye or a spotlight) always face the visitor.

**How to use:** Drag the script onto an object in the Hierarchy and look at the Inspector to adjust the settings.

---

## Homework: Atmosphere & Pacing

Prepare your project for the final showcase.
1. **Refine Lighting:** Dial in your mood. Use Volumetric Fog to create "God Rays" or specific atmospheric density.
2. **Interactivity:** Add at least two scripted elements (e.g., a rotating sculpture or a triggered event) to make the space feel alive.
3. **Cinematic Cutscene:** Refine your 30-second Timeline flythrough. Focus on smooth camera transitions and interesting angles.

Next session: **Final Polish, Sound, and Export.**
