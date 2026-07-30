# Making Fire (Particle Systems)

Part of **[Day 2](README.md)**.

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
