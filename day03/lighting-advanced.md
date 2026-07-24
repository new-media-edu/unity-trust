# Advanced Lighting (URP)

The [basic lighting tips](README.md#simple-tips-for-better-lighting) get you a scene that reads well. This guide is for pushing into higher-quality, more cinematic lighting. Each section is independent - pick what your scene needs.

Everything here assumes the workshop's **URP** project.

---

## First: what a "Volume" actually is

You'll hear "add a Volume" a lot. In URP, a **Volume** is *not* a light and it is *not* volumetric fog. It's a container for **post-processing overrides** - screen-wide image effects applied after the scene is rendered:

- **Bloom** - glow around bright/HDR pixels
- **Tonemapping** (Color Adjustments > add **Tonemapping**, mode `ACES` or `Neutral`) - stops bright lights from clipping to ugly flat white. This alone makes HDR lighting look filmic.
- **Color Adjustments / White Balance / Color Curves** - grade the whole scene's mood (warm, cold, faded, high-contrast).
- **Vignette** - subtle dark edges to focus the eye.

Setup: `GameObject > Volume > Global Volume`, create a new **Profile**, then **Add Override** for each effect. Check **Post Processing** on your Camera, and confirm **HDR** is on in the URP Asset.

> A **Global Volume** affects the whole scene. A **local** Volume (Box Collider set to *Is Trigger* + a Volume component with **Mode: Local**) only applies when the camera enters it - great for making one room feel colder or more saturated than the rest.

---

## "Particle-based" lighting (a fire that lights the room)

Particles themselves don't emit real light - they're just billboards. To make a campfire, torch, or magic effect actually illuminate nearby surfaces, you have two options:

**Option A - one child light (cheapest, recommended).**
Parent a single **Point Light** to the fire, warm orange, and animate its **Intensity** so it flickers. A tiny script or an animation curve driving intensity between, say, `0.8` and `1.2` sells it. One light, full effect.

**Option B - the particle system's Light module.**
On the Particle System, enable the **Lights** module. Assign a light **prefab**, and Unity spawns a real light *per particle*. This looks incredible but is **expensive** - a real light for every spark tanks performance fast.
- Set **Ratio** very low (e.g. `0.05`) so only ~5% of particles get a light.
- Cap **Maximum Lights** (e.g. `4`-`8`).
- Increase **Random Distribution** so the lit particles don't all cluster.

For almost every workshop scene, **Option A is the right call.** Reach for the Lights module only when a single light genuinely can't capture the effect.

---

## Baked Lighting (Global Illumination)

Real-time lights don't bounce - light hits a surface and stops. **Baking** pre-computes soft, bounced light (color bleeding, soft ambient occlusion, gentle shadows) into textures called **lightmaps**. It's the single biggest jump in realism for static scenes, and it's basically free at runtime because it's pre-calculated.

1. Select the static geometry (walls, floors, furniture) and check **Static** (top-right of the Inspector).
2. Set the lights you want baked to **Mode: Baked** (fully pre-computed) or **Mixed** (baked bounce + real-time direct light and dynamic shadows).
3. Open **Window > Rendering > Lighting**, go to the **Scene** tab, and click **Generate Lighting**.

Baking takes time and only works for things that don't move. Keep moving objects (the player, doors) on **Realtime** lights or use Light Probes below.

---

## Light Probes (baked light for moving objects)

Baked lightmaps only cover static geometry, so a moving character walking through a baked scene looks disconnected - flatly lit, ignoring the warm glow around it. **Light Probes** fix that: they sample the baked lighting at points in space and blend it onto dynamic objects.

- `GameObject > Light > Light Probe Group`.
- Position probes through the volume the player can walk - denser where lighting changes sharply (doorways, near colored lights).
- Re-bake (**Generate Lighting**). Moving objects now pick up the room's color and brightness.

---

## Reflection Probes (local reflections)

Smooth/metallic materials reflect the skybox by default, which looks wrong indoors. A **Reflection Probe** captures the actual surroundings so reflections match the room.

- `GameObject > Light > Reflection Probe`, place it at the center of a room.
- Set **Type: Baked** and bake, or **Realtime** for reflective surfaces that must update live (pricier).

---

## Light Cookies (shape and texture your light)

A **cookie** is a texture mask on a light - like a gobo/stencil in theater. It's how you get dappled light through leaves, window-blind stripes, or a stained-glass pattern across the floor.

- Import a grayscale texture, set its **Texture Type** appropriately, and drop it in the light's **Cookie** slot.
- Works on Spotlights (a mask over the cone) and Directional Lights (a tiling pattern over the whole scene - great for fake tree shadows).

---

## Fog and "volumetric" light

Plain **fog** (Lighting window > **Environment** > enable **Fog**, choose color/density) adds depth cheaply and is covered in the basic tips.

True **volumetric lighting** - visible god-ray shafts and light scattering through fog - is **not built into URP** (it's a native HDRP feature). To get it in URP you need a third-party asset (e.g. a volumetric lighting/fog package from the Asset Store) or a custom **Renderer Feature**. Don't chase this unless a scene truly calls for it; a good fog color plus Bloom fakes most of the impression.

---

## A sensible order of operations

1. Block in lights with color (warm/cool), dial intensities down from the default sun.
2. Add a **Volume** with **Tonemapping** first, then **Bloom**, then light **Color Adjustments**. Tonemapping before bloom stops highlights from clipping.
3. Mark static geometry **Static**, set lights to **Baked/Mixed**, and **Generate Lighting**.
4. Add **Light Probes** so moving objects match, and **Reflection Probes** for shiny surfaces.
5. Layer in cookies, flicker, and fog for character.

You rarely need all of it. Color + tonemapping + one bake already carries a scene most of the way.
