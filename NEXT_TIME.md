# For Next Time

Instructor notes for the next run of this workshop. Written while the current run is still fresh. These are changes to consider, not decisions already made.

## 1. Give the class one concrete thing to build

**The problem:** Right now the four days read as a collection of experiments. Students learn ProBuilder, then materials, then lights, then Timeline, but the through-line is thin. "Build an environment" is too open for people who have never opened Unity, and the assignments end up feeling like unrelated exercises rather than steps toward a finished piece.

**The fix:** Pick a theme on Day 1 that every exercise feeds into.

### Proposed theme: Micro World

Inspired by "The Genesis Tub" from The Simpsons, Treehouse of Horror VII, where Lisa accidentally creates a miniature, rapidly-evolving civilization inside a petri dish.

https://www.youtube.com/watch?v=hISNhmvcEt8

Show the clip in the first 15 minutes of Day 1. It's short, funny, and it sets the constraint instantly: you are building a tiny world, and it has an implied scale, an implied observer, and an implied passage of time.

**Why it works for this class:**
- The container is a hard boundary, which is exactly what beginners need. Nobody has to decide how big the world is or where it ends.
- It stays wildly individual. A micro world can be a swamp, a mining colony, a garden, a ruin, a city on a tooth. The frame is shared, the results are not.
- Scale is inherently interesting to look at and easy to demo. Small things filmed close read as cinematic almost for free.
- It gives every technique an obvious motivation.

### How each day reframes

| Day | Currently | Under the theme |
|---|---|---|
| 1 | Greybox a room, add first person controller | Establish the container and the ground. What is your world sitting inside? |
| 2 | Import assets, apply materials | Populate the civilization. Scans become boulders, terrain features, artifacts. |
| 3 | Lighting, audio, atmosphere | Light your world. Where is its sun? Does it have weather, a sky, a night? |
| 4 | Post, builds, showcase | Present it as an artifact. A specimen being observed. |

Every instruction becomes "do this to your world" instead of "here is a feature." Lighting stops being a list of light types and becomes "your world needs a light source, what is it and where does it come from."

**Things to watch for:**
- Don't let the theme become mandatory literalism. Somebody will want to build something that isn't in a tub, and that's fine as long as they keep the scale idea.
- Have two or three reference images ready beyond the Simpsons clip so it doesn't read as a single joke. Terrariums, dioramas, ant farms, model railroad scenery, Miniatur Wunderland, macro photography.
- Say out loud on Day 1 that the assignments compound. People need to know that Day 2's work builds on Day 1's file, not a fresh scene.

## 2. Consider swapping ProBuilder for Terrain

Given a natural, small-scale, organic theme, **Terrain** may serve the class better than **ProBuilder**.

**Arguments for Terrain:**
- Better fit for the theme. Micro worlds are mostly landscape, not architecture.
- Faster to something that looks good. Sculpting a hill is more immediately satisfying than pushing faces on a cube, especially for people with no 3D background.
- Built-in tools that pay off later in the week: heightmap sculpting, texture painting with multiple layers, tree and detail (grass) painting. That last one covers a lot of Day 2's "populate your world" work in one tool.
- Painting foliage across a terrain is a much stronger Day 2 demo than dragging in individual prefabs.

**Arguments against, or things to check first:**
- Terrain is heavy by default. A 1000x1000 terrain for a tiny world is wasteful and will hurt frame rate on laptops. Need to set a small resolution (something like 50x50 or 100x100) in the create step and make that part of the instructions, not an afterthought.
- URP terrain materials need the right shader assigned or everything comes in magenta. Test this in a clean Unity 6 URP project before class and write the exact steps down.
- Terrain grass and detail rendering has historically been finicky in URP. Verify it works before promising it.
- Losing ProBuilder means losing the "build a room" exercise, which is genuinely useful and which some students will want for architectural projects.

**Likely best answer:** Terrain becomes the primary Day 1 tool, ProBuilder gets demoted to a short segment or an optional aside for anyone who wants built structures in their world. Don't try to teach both properly in 2.5 hours.

## 3. Move camera movement to the last session

Timeline and Cinemachine currently sit on Day 3, alongside lighting, audio, reflection probes, and volumetrics. That's too much for one session, and it puts camera work before the world is finished.

**Move it to Day 4**, where it belongs next to post-processing and Unity Recorder. The natural Day 4 flow becomes:

1. Post-processing (make it look good)
2. Cinemachine and Timeline (decide how it's seen)
3. Unity Recorder (capture it)
4. Showcase

That's one coherent sequence about presentation rather than three disconnected topics.

**Knock-on effects:**
- Day 3 gets room to breathe and can go deeper on lighting and audio, which is where the biggest visual payoff is anyway.
- Day 4 gets tight. Post plus Cinemachine plus Recorder plus a showcase in 2.5 hours is a lot. Standalone builds may need to be cut down to a 10 minute demo, or moved to written instructions people follow at home.
- The Day 3 assignment needs rewriting. "Create a 30-second cinematic flythrough" no longer works there. Replace it with something lighting and mood focused, for example: light your world for two different times of day and save a screenshot of each.

## 4. Drop Cinemachine, animate the camera with the Animation window

Related to #3. When camera work moves to Day 4, it should also get simpler.

**The problem:** Cinemachine has a lot of setup surface for what we actually use it for, which is "fly the camera along a path and look at things." The Day 3 walkthrough currently has to explain, and troubleshoot, all of this:

- Installing the package.
- Creating a separate cutscene camera, because the first person controller's camera can't also be the Cinemachine camera.
- Removing the Cinemachine Brain from the player camera and adding it to the cutscene camera.
- Binding the Cinemachine Track to the Brain.
- The single-track rule, because dropping a shot into empty space silently creates a second track and two tracks fight over the same Brain. This is the number one thing that breaks in class.
- Camera **Depth** ordering, so the right camera actually renders in the Game view.

That is a lot of concepts and a lot of failure modes for a no-code workshop, and none of them are about the thing students care about, which is what the shot looks like.

**The fix:** Use the **Animation** window instead. Keyframe the camera's position and rotation directly, exactly the same way we already keyframe a spinning sculpture or a rising platform.

**Why this is better for this class:**
- **It's already taught.** Day 3 has a Basic Animation section that keyframes position and rotation on a cube. The camera becomes one more object you animate. Zero new concepts, and it retroactively makes the animation lesson feel more important.
- **One camera, no brain, no binding, no track rules.** Select the camera, hit record, move the playhead, move the camera. That's the whole workflow.
- **Direct control over the shot.** With Cinemachine, students place shots and hope the blend between them looks good. With keyframes, they see and adjust the actual path. When a move feels wrong, the fix is dragging a keyframe, not tuning a blend curve on a clip overlap.
- **Debuggable.** When a keyframed camera does something weird, you can see why. When a Cinemachine blend does something weird, you're explaining Brains and track binding.

**What we lose, and whether it matters:**
- Automatic smooth blending between shots. Mostly fine. An animated camera moving continuously is arguably the better look for a micro world anyway, closer to a macro lens drifting over a specimen than to an edited film with cuts.
- Hard cuts between angles. Still doable: keyframe the camera to jump, or set the tangents to constant. Worth showing as a one minute aside.
- Follow/orbit/look-at behavior. This is the real loss. If somebody wants a camera orbiting a centerpiece, hand-keyframing an orbit is tedious. Options: parent the camera to an empty at the world's center and animate the **empty's** Y rotation, which gives a perfect orbit with two keyframes. That trick is worth teaching regardless and covers most of what people will actually ask for.

**Does Timeline stay?** Probably yes, but as an optional layer rather than a requirement. Once the camera is an animated object, Timeline's job is just sequencing (start the camera move, then trigger a light change, then fade audio) via an Animation Track. If time is short on Day 4, cut Timeline entirely and let the Animator play the camera clip on its own. The flythrough works fine without it.

**To do before next run:**
- Rewrite the Day 3 Cinemachine section as a Day 4 "Animating the Camera" section.
- Test the parent-empty orbit trick in Unity 6 and screenshot it.
- Confirm Unity Recorder captures an Animator-driven camera cleanly without Timeline. It should, but verify before promising it.
- Delete or archive the Cinemachine screenshots in `images/` once the section is gone.

## Other notes and open questions

- Confirm whether Polycam scans still make sense under the micro world theme. They probably do, and arguably get better: scanning small real objects (a rock, a plant, a piece of trash) and dropping them in at world scale is exactly the right gesture. Consider explicitly reframing the Day 2 Polycam assignment as "scan a small object and make it a landmark."
- Physics on Day 1 could tie into the theme. Dropping objects into the container is a nice first demo.
- Consider a shared showcase format at the end where all the micro worlds are shown back to back. The common frame makes the variety land much harder than unrelated projects would.
- Check timing on Day 1. It ran full this time even before adding a theme discussion and clip.
