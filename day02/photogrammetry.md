# Photogrammetry

Part of **[Day 2](README.md)**.

Photogrammetry is the process of using photos to create 3D models. We use **Polycam** to capture real-world objects. If you haven't yet, install the app on your phone (see the instructions and tutorial link at the top of this guide) and start thinking about objects or spaces you'd like to scan. We'll import these scans into Unity in Session 3.

![Photogrammetry Cameras](../images/photogrammetry-cameras.png)
![Photogrammetry Diagram](../images/photogrammetry-diagram.png)

## Installation
Install the Unity glTFast package using the Unity Package Manager.

To install the Unity glTFast package, follow these steps:
1. In your Unity project, go to **Window > Package Manager**.
2. In the dropdown in the top-left of the Package Manager window, select **Packages: Unity Registry**.
3. Search for **glTFast** (or **Unity glTFast**), select it, and click **Install**.
4. *Alternative:* Click the **Add (+)** button, select **Add package by name...**, enter `com.unity.cloud.gltfast`, and click **Add**.

> [!WARNING]
> Do **not** use **Add package from git URL...** with the glTFast GitHub URL. Doing so installs the absolute latest development branch, which contains APIs (like `ReadOnlySpan` overloads) that are incompatible with older Unity Editors. This will result in compiler errors like `cannot convert from 'System.ReadOnlySpan<byte>' to 'byte[]'`. If you did this, remove the Git version and reinstall from the Unity Registry.

## Optional Packages
There are some related packages that improve Unity glTFast by extending its feature set:
- **Draco™ 3D Data Compression Unity Package** (provides support for `KHR_draco_mesh_compression`)
- **KTX™ for Unity** (provides support for `KHR_texture_basisu`)
- **meshoptimizer decompression for Unity** (provides support for `EXT_meshopt_compression`)

> [!TIP]
> **Troubleshooting: `TypeLoadException` Errors**
> If you see `TypeLoadException: Could not load type 'GLTFast.AnimationMethod' from assembly 'glTFast'` in your console, it indicates conflicting versions of the package or stale cached assemblies. To resolve it:
> 1. **Check for duplicate packages:** In the Package Manager, make sure you don't have both the old `com.atteneder.gltfast` (or `org.gltfast`) and the new `com.unity.cloud.gltfast` installed. If you imported an older version manually, search your project's **Assets** folder and delete any stray `glTFast` folders or DLLs.
> 2. **Clear the cache:** Close Unity, delete the **`Library`** folder in your project's root directory (where `Assets` and `Packages` folders live), and reopen Unity. This forces Unity to cleanly rebuild all assembly cache references from scratch.
