# Advanced Fire (Particle Systems)

The [basic fire](README.md#making-fire-particle-systems) gets you a working flame in a few minutes, but it reads flat and grey. Real fire looks *hot* - it glows, flickers, and trails smoke. This guide layers those on top.

Everything here assumes the workshop's **URP** project. Where a step is pipeline-specific, that's called out. Skip around - each section is an independent upgrade, roughly ordered by impact.

---

## 1. An Additive Material (biggest single fix)

Alpha-blended fire always looks flat and grey. Fire is *light*, so it should **add** to whatever is behind it.

1. In the **Project** window, right-click > **Create > Material**. Name it `M_Fire`.
2. In the Inspector, set the **Shader** dropdown to **Universal Render Pipeline > Particles > Unlit**.
3. Under **Surface Options**:
   - **Surface Type:** `Transparent`
   - **Blending Mode:** `Additive`
4. **Base Map:** assign a flame texture - a flipbook sheet or a soft, wispy alpha, *not* the default round blob.
5. **Base Color:** click the swatch, switch the picker to **HDR**, and push **Intensity** to `2`-`4`. This is what makes Bloom (below) treat the flame as a light source.
6. Drag `M_Fire` into the **Renderer** module's **Material** slot on the particle system.

> **Can't find an additive shader?** In URP the blend mode lives *inside* the material (Surface Options), not as a dropdown on the particle system. If the whole Surface Options block is missing, the material isn't using a *Particles* shader - re-check step 2. (For reference: Built-in uses `Particles/Additive`; HDRP uses `HDRP/Unlit` with Surface Type Transparent + Blending Additive.)

---

## 2. Texture Sheet Animation (kills the "static billboard" look)

A single frozen billboard is the #1 source of blandness. A **flipbook** sheet animates the flame.

1. Get a flame flipbook texture (a grid like `4x4` or `8x8`) and assign it as the **Base Map** in step 1.
2. On the particle system, enable **Texture Sheet Animation**.
3. Set **Tiles** to match your sheet (e.g. `8 x 8`).
4. **Time Mode:** `Lifetime`, and enable frame **blending** so frames cross-fade instead of popping.
5. Turn on **Random Row** so identical starting frames don't visibly repeat across particles.

---

## 3. Noise (the flicker)

Noise is what makes flames dance instead of drift straight up.

- Enable the **Noise** module.
- **Strength:** `0.3`-`0.6`
- **Frequency:** `0.8`-`1.5`
- **Scroll Speed:** `0.5`-`1`
- Enable **Damping**, set **Quality** to `High`.
- Turn on **Separate Axes** and give **Y** a lower value than X/Z - horizontal flicker looks like fire; vertical bobbing looks like a jellyfish.

---

## 4. Rotation over Lifetime

- Enable **Rotation over Lifetime**.
- Set **Angular Velocity** to a random range like `-45` to `45` degrees.

Combined with Random Row (step 2), this hides the fact that you're reusing a handful of flipbook frames.

---

## 5. Layer Three Systems

Real fire isn't one emitter - it's three stacked on top of each other. Build each as its own Particle System (parent them under one empty `Fire` object).

- **Core** - small, bright, near-white/yellow, short lifetime, high emission, additive. The hot heart of the flame.
- **Flame body** - the orange system from the basic guide: larger, slower, additive.
- **Smoke** - a *separate* system, **alpha-blended (not additive)**, dark grey, low emission rate, long lifetime, large **Start Size**, slow rotation over lifetime. Spawn it slightly *above* the flame base so smoke rises out of the fire, not from under it.

With smoke on its own system, drop the charcoal/grey from the flame's **Color over Lifetime** entirely. The flame gradient should be pure heat: **white-yellow -> orange -> deep red -> transparent**.

---

## 6. Bloom (makes it glow)

HDR color (step 1) does nothing until Bloom picks it up.

1. **GameObject > Volume > Global Volume**. Create a new profile on it.
2. **Add Override > Post-processing > Bloom.** Set **Threshold** `~1.0`, **Intensity** `1`-`2`.
3. On your **Camera**, check **Post Processing**.
4. Confirm **HDR** is enabled on the URP Asset (**Project Settings > Quality**, or the assigned URP Asset > **HDR**).

HDR start color + Bloom is the difference between fire that looks hot and fire that looks painted on.

---

## 7. A Light (so it lights the room)

Fire that doesn't illuminate its surroundings never convinces.

1. Add a child **Point Light** to the fire (or enable the particle system's **Light** module).
2. Warm orange color.
3. Animate the **intensity** - a noise-driven curve or a tiny flicker script - so the light pulses with the flames.

---

## 8. Soft Particles (optional polish)

Softens the hard line where flames intersect floor or wall geometry.

1. On `M_Fire`, under **Surface Options**, check **Soft Particles**. Set **Near Fade** `0`, **Far Fade** `1`.
2. This requires **Depth Texture** enabled on the active URP Asset: **Project Settings > Graphics** > select the assigned render pipeline asset > **General** > check **Depth Texture**.

> **"Soft Particles isn't showing up."** It only appears when the material's **Surface Type** is `Transparent` (Opaque collapses those fields), *and* Depth Texture is enabled on the URP Asset. On older URP versions (7.x-10.x) it may only exist on **Particles > Lit**, not Unlit - check that shader too. And make sure the material is a *Particles* shader, not plain `Universal Render Pipeline/Unlit`, which has no soft-particle support at all. (Check your URP version under **Window > Package Manager > Universal RP**.)

---

## Renderer settings worth setting

On the particle system's **Renderer** module:
- **Render Alignment:** `View`
- **Sort Mode:** `By Distance`

---

That's the full stack. You rarely need all of it - the additive material, a flipbook sheet, and Bloom alone already take the fire most of the way there.
