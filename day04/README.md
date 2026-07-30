# Day 04: Interactivity, Builds & Final Showcase

**Session Time:** 2.5 Hours

The final session is about that last 10% that makes a project feel finished. We'll sculpt some real ground, add life and interaction, export standalone playable files, capture video of our worlds, and showcase them.

## Terrain: Sculpting the Ground

So far our floors have been flat ProBuilder surfaces. Unity's **Terrain** system lets us sculpt organic ground: hills, valleys, riverbeds, dunes.

### Creating the Terrain

1. Go to `GameObject > 3D Object > Terrain`.
2. A new Terrain appears in the Hierarchy. It is **enormous** by default, and it is going to dwarf everything you've built. That's expected, we fix it next.

### Resizing It

There is no popup asking how big you want it, so we resize it after the fact.

1. Select the **Terrain** in the Hierarchy.
2. In the Inspector, the Terrain component has a row of five icons. Click the **far right icon** (Terrain Settings).
3. Scroll down to **Mesh Resolution** and set:
   - **Terrain Width:** `100`
   - **Terrain Length:** `100`
   - **Terrain Height:** `50`

![Terrain Settings with Mesh Resolution at the bottom](../images/terrain-settings-resolution.png)

> [!IMPORTANT]
> Do this **before** you sculpt anything. Changing the width or length rescales any hills you've already made, and it stretches them out of shape.

**Why change the height?** Terrain Height defaults to `600`, which is the vertical range the sculpting brush works across. At 600, a single click launches a mountain into the sky and sculpting feels broken. At `50` the brush becomes gentle and controllable.

### Centering It

A Terrain's position is its **corner**, not its middle. So a fresh terrain sits entirely off to one side of your scene.

In the **Transform**, set the Position to:
- **X:** `-50`
- **Y:** `0`
- **Z:** `-50`

That's half the width and half the length, which pulls the terrain back so it's centered on the world origin.

### Sculpting

1. Back in the Terrain component, click the **second icon** (Paint Terrain).
2. Make sure the dropdown below it says **Raise or Lower Terrain**.
3. Pick a **Brush** shape from the grid. The soft, fuzzy ones give you smoother, more natural hills than the hard-edged ones.
4. Set **Brush Size** and **Opacity**.
   - **Brush Size** is how wide your brush is. Around `9` is good for detail, go much bigger for broad landscape shapes.
   - **Opacity** is how fast it builds. Start **low**, around `0.01` to `0.1`. High opacity makes spikes, not landscapes.
5. **Left click and drag** in the Scene view to raise the ground. **Hold Shift** and drag to lower it.

![Sculpting a terrain in the Scene view](../images/terrain-sculpting.png)

> [!TIP]
> A brand new terrain sits at height zero, so you can only sculpt **upward**. To carve down into it (a valley, a pond, a crater) first raise the whole thing: choose **Set Height** from the dropdown, set Height to something like `5`, and click **Flatten**. Now you have room to go both directions.

### The Other Sculpting Tools

That dropdown has more than just Raise or Lower. Click it and you'll see:

![The Paint Terrain mode dropdown](../images/terrain-paint-modes.png)

| Mode | What it does |
|---|---|
| **Raise or Lower Terrain** | Your main brush. Click to raise, Shift+click to lower. |
| **Smooth Height** | Softens what you've already sculpted. Brush over lumpy, spiky areas to relax them. |
| **Set Height** | Type an exact height and **Flatten** the whole terrain to it, or brush that height in. |
| **Stamp Terrain** | Stamps the brush shape in as a hill of a set height. Good for quick repeated mounds. |
| **Paint Texture** | Paints ground surfaces. We'll use this next. |
| **Paint Holes** | Cuts holes clean through the terrain, for cave mouths and openings. |

> [!TIP]
> **Smooth Height** is the fix for "my terrain looks like crumpled tinfoil." Sculpt roughly and fast, then smooth it back. That's much easier than trying to be delicate on the first pass.

### Giving It a Surface

Your terrain is currently a grey checkerboard. That is not a bug and it is not a broken material, it just means the terrain has no **Terrain Layer** yet. A Terrain Layer is the texture that gets painted onto the ground.

**First, get a ground texture.** The class files include four, ready to go. Drag the whole `_Workshop_Assets/Terrain_Textures/` folder into your Unity **Project** window.

| Texture | Good for |
|---|---|
| `grass_004` | A base layer. Lush green lawn grass. |
| `sparse_grass` | A base layer. Dry grass over soil, browner and more muted. |
| `roots` | Forest floor, undergrowth, anything overgrown. |
| `rocky_trail_02` | Paths and worn routes. Paint it where people would walk. |
| `cracked_red_ground` | Dry, arid, desert. Also good for a dried-out riverbed. |

Each one has a `_diff` file (the colour) and a `_nor_gl` file (the surface relief). We'll use both.

**Want more?** [Poly Haven](https://polyhaven.com/textures) and [ambientCG](https://ambientcg.com/) both have thousands of ground textures, free under the CC0 public domain licence, no account needed. Download at **1K or 2K**; the 4K and 8K options will slow your laptop down for no visible benefit at terrain scale.

**Then build the layer:**

1. In the Terrain component, click the **second icon** (Paint Terrain) again.
2. Change the dropdown to **Paint Texture**.
3. Under **Terrain Layers**, click **Edit Terrain Layers... > Create Layer**.
4. A texture picker opens. Choose a colour map, the file ending in **`_diff`**. `sparse_grass_diff_1k` is a good base.
5. The checkerboard disappears. The **first** layer you add automatically covers the entire terrain.

### Tiling and Normal Maps

Two adjustments turn a flat-looking texture into convincing ground. Select your layer in the Terrain Layers list to find these.

**Tiling.** Set **Tiling Settings > Size** to around `5` and `5`. Too small and the ground reads as a fine busy pattern rather than a surface; too large and it turns to blurry mush. Nudge it until it looks like ground and not like wallpaper.

**The Normal Map.** This is the payoff from our materials session. Drag the matching **`_nor_gl`** file into the layer's **Normal Map** slot. Suddenly the ground catches light and has real surface relief instead of looking painted on. It is the single biggest quality jump available here for one drag.

> [!NOTE]
> `nor_gl` means "normal map, OpenGL format," which is the flavour Unity expects. If you download textures elsewhere and see a choice between **GL** and **DX**, always take **GL**. The DX version inverts the lighting so bumps read as dents, which looks subtly and unfixably wrong.

**Adding a second surface.** Repeat steps 3 and 4 to create another layer. Unlike the first one, this one does **not** flood the terrain. Select it, then paint it on by hand where you want it.

Try `rocky_trail_02` for this. Painting a worn path into the low ground and along the routes people would actually walk, and leaving grass everywhere else, is the fastest way to make a landscape look deliberate rather than generated. Lower the brush **Opacity** and the two surfaces blend into each other instead of meeting at a hard edge.

## Making the Terrain Look Good

A sculpted, textured terrain still tends to look like a video game level from 2004. These are the fixes, in order of how much they buy you per minute spent.

> [!TIP]
> If you only do two things, do the first two. They take about a minute combined and they change everything.

### 1. Lower the sun

Select your **Directional Light** and set its Rotation **X** to somewhere around `20` to `35`.

Light coming from directly overhead flattens a landscape completely; every slope receives the same amount of light, so all your sculpting work becomes invisible. Low, raking light throws long shadows down the sides of your hills and suddenly the shape of the land reads. This is the single biggest difference between terrain that looks flat and terrain that looks like somewhere.

It also makes the light warmer and more directional, which is most of what people mean when they say a shot looks cinematic.

### 2. Turn on fog

1. Open `Window > Rendering > Lighting`.
2. Go to the **Environment** tab.
3. Scroll to **Other Settings** and check **Fog**.
4. Set **Mode** to `Exponential Squared` and **Density** to something small, around `0.01`.
5. Set the fog **Color** by eyedropping a colour from your sky near the horizon.

Fog does two jobs at once. It adds aerial depth, so distant hills read as distant instead of as flat cutouts. And it **hides the edge of your terrain**, which is otherwise the thing that most obviously breaks the illusion, since your world visibly stops and drops into nothing.

> [!NOTE]
> This is regular distance fog, not volumetric fog. URP doesn't have volumetric fog built in, so don't expect visible shafts of light.

### 3. Nothing in nature is flat

Large flat areas are the strongest tell that a terrain was made in a hurry. Real ground undulates everywhere, even where it looks level.

Go back to **Raise or Lower Terrain**, set a **very large Brush Size** and a **very low Opacity** (`0.01`), and make a few slow passes over your flat areas. You want variation you can barely see. Then run **Smooth Height** over the result.

### 4. Check your tiling

If your ground reads as a flat sheet of single-colour paint rather than as a surface, the texture is tiling too few times. Select the layer and lower **Tiling Settings > Size** until actual detail appears.

The reverse is also true: if it looks like busy visual noise, raise the Size. You are looking for the point where you can tell what the material is but you can't see the repeat.

### 5. Soften the boundaries between layers

Where two painted surfaces meet at a visible edge, it reads as paint. Select your second layer, drop the brush **Opacity** right down, and work back and forth over the boundary so the two surfaces interleave over a wide, uneven band instead of meeting at a line.

While you're there: paint your rock layer onto the **steep** faces and let grass keep the flatter ground. That is how real landscapes work, because loose soil and plants can't hold onto a steep slope.

### 6. Sit your building into the ground

A structure resting on dead-flat terrain looks dropped in. Give it a foundation instead:

1. Choose **Set Height** from the dropdown.
2. Set a **Height** matching the ground level you want.
3. Use a small brush to **flatten a deliberate terrace** for the building to sit on.
4. Paint `rocky_trail_02` as a path leading up to the entrance.

A flat pad plus a worn path is a small amount of work that makes the architecture and the landscape look like they belong to each other.

### 7. Smooth your ridgelines

If your hills look like long rounded worms (the shape a Raise brush naturally makes when you drag it), run **Smooth Height** along the tops with a medium brush. Real ridges are uneven: high in places, collapsed in others. Smoothing selectively, rather than everywhere, is what breaks up the uniformity.

### 8. Sharpen the silhouette

In **Terrain Settings** (the far right icon), lower **Pixel Error** from `5` to `1`.

This makes Unity draw the terrain closer to the shape you actually sculpted, at some performance cost. Worth doing before you record video, and worth turning back up if your frame rate suffers while you work.

### Terrain Troubleshooting

**"I painted trees and nothing happened."**

Two things are probably going on:

1. **You have no tree assets.** Unity 6 ships with zero tree models. Under **Paint Trees**, `Edit Trees... > Add Tree` gives you an empty **Tree Prefab** slot, and if you leave it empty then painting does nothing at all, silently. You need to bring in actual tree prefabs first (see the foliage section).
2. **Do not use `GameObject > 3D Object > Tree`.** Unity's built-in Tree Editor makes trees that use legacy shaders which **do not exist in URP**. They render solid magenta. This is a known, long-standing limitation, not something you've done wrong. Use imported prefabs instead.

**"My terrain is cutting through my building."**

Sculpt around it, or lower the terrain's **Y** position so the ground sits below your existing floor and only pokes up where you want it to.

## Scripted Interactivity (No Coding Required)

The class files include a set of scripts in `_Workshop_Assets/Scripts/` to add life to your scene.

- **AutoRotate.cs:** Make objects (like art pieces) spin.
- **SimpleTrigger.cs:** Trigger an event (like a light turning on) when the player walks into a zone.
- **AudioCrossfader.cs:** Smoothly switch between music tracks as you enter different rooms.
- **LookAtPlayer.cs:** Make an object (like an eye or a spotlight) always face the visitor.

**How to use:** Drag the script onto an object in the Hierarchy and look at the Inspector to adjust the settings.

## Building for the Real World (Standalone Builds)

To share your environment as a "playable" experience, we need to export it from the Unity Editor.

### The Build Process
1. Go to `File > Build Settings`.
2. Ensure your current scene is in the **Scenes In Build** list.
3. Select your platform (Mac, Windows, or Linux).
4. Click **Build** and choose a folder. Unity will package everything into a single `.app` or `.exe` that anyone can run without needing Unity installed.

## Rendering & Output

To show your work outside of Unity, we need to record it. There are two ways: a script from the class files, or Unity's official Recorder package.

### Option A: SceneRecorder (class files)

`_Workshop_Assets/Scripts/SceneRecorder.cs` is a drag-and-drop recorder. Nothing to install.

1. Drag **SceneRecorder.cs** onto the camera you want to record (your flythrough camera, or the one under your First Person Controller).
2. In the Inspector, set the **Width** and **Height** (1920 x 1080 is a good default) and the **Frame Rate**.
3. Press **Play**.
4. Press **F9** for a still, or **F10** to start and stop recording. A red **● REC** dot appears while it records.
5. Find your files in a **Recordings** folder next to your Assets folder (`Show in Explorer` / `Reveal in Finder` on the project folder).

Notes:
- It captures exactly what that camera sees, post-processing included, at the resolution you set, no matter what size your Game view is.
- **Video Format** has two options. **Video** writes a playable `.mp4` straight out of Unity, which is what you want in class. **Png Sequence** writes numbered frames instead, for maximum quality; the Console prints the `ffmpeg` command to turn them into a movie.
- The `.mp4` option only works inside the Unity Editor. In a standalone build it automatically falls back to PNG frames.
- It records picture only, no audio.
- Set **Max Seconds** to stop automatically, handy for matching a 30-second flythrough.
- **Save Frames** writes the numbered PNGs alongside the video, so you get both from one take.

#### Assembling a video from frames

If you end up with a folder of numbered PNGs, [ffmpeg](https://ffmpeg.org/download.html) turns them into a video. Run this from inside the frame folder:

```
ffmpeg -framerate 30 -i "frame_%05d.png" -c:v libx264 -pix_fmt yuv420p output.mp4
```

Replace `30` with your frame rate, `frame_%05d.png` with the actual naming pattern of your files, and `output.mp4` with the name you want. The Console also prints this command with your own numbers filled in when a recording stops.

### Option B: Unity Recorder (official package)

More features (audio, image sequences, GIFs, Timeline integration), but a heavier setup.

1. Install **Unity Recorder** from the Package Manager.
2. Open `Window > General > Recorder > Recorder Window`.
3. Select **Movie** and choose your resolution (e.g., 1080p or 4K).
4. Set the **Recording Mode**. If your camera move is a Timeline, connect the Recorder to it so it records exactly as the cinematic plays. If you keyframed your camera in the Animation window, just record for the number of seconds your clip runs.

## Final Group Showcase

The last hour will be spent presenting our worlds to each other.

### Presentation Format
- **The Pitch:** Briefly explain the concept/narrative of your space.
- **The Walkthrough:** Play your cinematic flythrough or walk through the space live.
- **Feedback:** Share one thing you love about the environment and one technical thing you learned.

## Final Homework: Deployment & Sharing

Now that your environment is built, take it further:
1. **Interactivity:** Add at least two scripted elements (a rotating sculpture, a triggered light) to make the space feel alive.
2. **Recording:** Render a high-quality (1080p or 4K) version of your flythrough, plus a few still images for your portfolio.
3. **Standalone Build:** Attempt to "Build" your project for Mac or Windows to share it as a playable file.
4. **Documentation:** Write a short artist statement or description for your portfolio, explaining the concept behind your virtual environment.

## Beyond the Workshop

Your Unity journey doesn't end here.
- **Post-Processing:** Dig into the **Global Volume** for color grading, depth of field, and motion blur.
- **VR/AR:** Explore how to move these environments into headsets like the Meta Quest.
- **Scripting:** Start exploring C# to add even more complex logic.

**Stay in touch!** We'd love to see what you build next.
