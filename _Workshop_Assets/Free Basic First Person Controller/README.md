# Free Basic First Person Controller for Unity

A lightweight, high-performance, modular First Person Controller built for Unity's Universal Render Pipeline (URP) and fully powered by the new Unity Input System (UnityEngine.InputSystem).

Designed to be plug-and-play, easy to customize, and asset-store ready for any 3D URP project.

---

## Key Features

- Universal Render Pipeline (URP) Ready: Custom URP/Lit material included out of the box (no pink material errors).
- New Unity Input System Ready: Fully integrated with com.unity.inputsystem. Works out of the box with Keyboard, Mouse, and Gamepads.
- Smooth Stair & Step Climbing: Dynamic raycast step-offset climbing allows the character to seamlessly walk up stairs and over low ledges without getting stuck.
- Zero-Friction Physics Material: Custom PlayerFrictionless physics material prevents wall-clinging and friction locking on vertical surfaces.
- Normal Human Proportions: Scaled to standard 2.0m height with 1.7m eye-level camera position.
- Fixed & Optimized Audio: Dynamic footstep, running, crouching, jumping, and landing audio system.
- Modular Components:
  - FirstPersonMovement: Smooth physics-based movement with stair climbing, sprint, and custom speed override support.
  - FirstPersonLook: Silky-smooth mouse & stick camera rotation with pitch clamping.
  - Crouch: Smooth crouching with head lowering and capsule collider resizing.
  - Jump: Ground-checked jumping with impulse force.
  - Zoom: Camera FOV scroll zoom.
  - GroundCheck: Precision ground detection using physics raycasting.
  - FirstPersonAudio: Integrated 3D spatialized audio player.

---

## Package Folder Structure

```text
Assets/Free Basic First Person Controller/
├── Audio/                                  # Spatialized sound effects (Steps, Crouch, Jump, Landing)
├── Demo/                                   # Demo scene (Demo Free Basic FPC.unity)
├── Input/                                  # Input Action Asset (FirstPersonControls.inputactions)
├── Materials/                              # URP PlayerMaterial & PlayerFrictionless physics material
├── Scripts/                                # Core controller C# scripts
│   ├── FirstPersonLook.cs
│   ├── FirstPersonMovement.cs
│   └── Components/
│       ├── Crouch.cs
│       ├── FirstPersonAudio.cs
│       ├── GroundCheck.cs
│       ├── Jump.cs
│       └── Zoom.cs
└── Free Basic First Person Controller.prefab  # Controller prefab
```

---

## Quick Start Guide

### 1. Requirements
- Unity 2021.3 LTS or higher.
- Universal Render Pipeline (URP) package installed.
- Package com.unity.inputsystem installed and active in Project Settings > Player > Active Input Handling.

### 2. Using the Prefab
1. Drag Free Basic First Person Controller.prefab into your scene.
2. Ensure your scene has ground colliders for physics contact.
3. Press Play!

### 3. Rendering Notes (Avoiding Shimmer on Tiled/Grid Textures)
The First Person Camera ships with Temporal Anti-Aliasing (TAA) pre-configured via its
Universal Additional Camera Data component — this is what keeps high-frequency, tiled
textures (grids, checkerboards, blockout materials) from crawling/shimmering as the
camera moves, which can otherwise look like motion "jitter" even though the controller
itself is physics-stable.

URP does not support running MSAA and TAA at the same time (URP disables TAA on any
camera where MSAA is active). Leave MSAA off on your URP Pipeline Asset (Quality >
Rendering > Anti Aliasing (MSAA) = Disabled) so the camera's TAA actually takes effect —
if you enable MSAA instead, you will lose TAA and grid/checker textures will shimmer again.

For best results in your own project:
- Keep MSAA Disabled on your URP Pipeline Asset so this camera's TAA is used.
- Enable V Sync (Project Settings > Quality) to avoid tearing, which is especially visible
  on straight grid lines.
These are project-level settings and can't be shipped inside the package, so they must be
set in the host project.

### 3. Controls Default Bindings

| Action | Keyboard / Mouse | Gamepad |
| :--- | :--- | :--- |
| Move | WASD / Arrow Keys | Left Stick |
| Look | Mouse Delta | Right Stick |
| Jump | Space | Button South (A / Cross) |
| Sprint | Left Shift | Left Stick Click (L3) |
| Crouch | Left Ctrl / C | Button East (B / Circle) |
| Zoom | Mouse Scroll Wheel | — |

---

## Component Overview

### FirstPersonMovement
Handles player ground velocity, rigid body physics, and stair climbing.
- speed: Walking speed.
- runSpeed: Sprinting speed.
- enableStepClimbing: Toggles automatic stair and step climbing.
- maxStepHeight: Maximum height of steps character can automatically climb (default 0.35m).
- stepCheckDistance: Distance ahead to check for steps.
- stepSmoothness: Smoothness multiplier for stepping over obstacles.
- moveAction: InputActionReference to Move action (Vector2).
- sprintAction: InputActionReference to Sprint action (Button).

### FirstPersonLook
Rotates the camera vertically (pitch) and character body horizontally (yaw).
- sensitivity: Look sensitivity scaling factor.
- smoothing: Interpolation factor for smooth camera motion.
- lookAction: InputActionReference to Look action (Vector2).

### Crouch
Lowers camera head position and scales CapsuleCollider height while adjusting movement speed.
- movementSpeed: Speed when crouched.
- crouchYHeadPosition: Head local Y position when crouched (default 1.0m).
- crouchAction: InputActionReference to Crouch action (Button).

### Jump
Applies upward impulse force when grounded.
- jumpStrength: Force multiplier.
- jumpAction: InputActionReference to Jump action (Button).

### FirstPersonAudio
Manages movement footstep loops and event-driven SFX (jump, land, crouch).
- Automatically handles looping step and running audio based on horizontal Rigidbody velocity.

---

## License & Asset Store Information

Ready for commercial use and Unity Asset Store distribution.
