# Bringing the Real World In: Importing Scans

Part of **[Day 3](README.md)**.

If you created photogrammetry scans with Polycam, now is the time to bring them in.

1. Export from Polycam as `.glb` or `.obj`.
2. Drag the file into your **Project** window.
3. Drag it from the Project window into the **Scene**.
4. **Scaling:** Scans often come in at the wrong size. Use the **Scale Factor** in the Import Settings, or the Scale tool (`R`), to fix it.

![A Polycam scan of a Berlin trash can imported into Unity](../images/polycam-scan-imported.png)

Scans arrive as a single lumpy mesh with the ground they were standing on still attached. That ragged base is normal. Sink it slightly into your floor, or hide the seam with other geometry.
