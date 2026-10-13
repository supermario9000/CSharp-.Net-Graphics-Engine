# C#/.NET Software Rasterizer Learning Plan

This plan is for building a small CPU-based image rasterizer while learning C#,
.NET, and computer graphics at the same time. It is deliberately incremental:
each milestone should produce a visible result and a set of tests before the
next milestone begins.

The main learning resource should be **TinyRenderer**, but it should be used as
a sequence of graphics exercises rather than copied as a complete architecture.
The project should first render images in memory and save them to files. A
window, real-time input, GPU rendering, and portability come later.

## What this project is initially

The first version is:

- a **software rasterizer**;
- written in C# and running on the CPU;
- able to write pixels into an in-memory image;
- able to save the image to a file;
- eventually able to render textured and lit 3D triangles.

It is not initially:

- an OpenGL, DirectX, Vulkan, or Metal engine;
- a GPU driver;
- a complete game engine;
- a cross-platform UI toolkit;
- a performance benchmark;
- a replacement for an established image library.

Keeping this scope small is important. A correct 256x256 image renderer is a
better milestone than an unfinished system that tries to support every device.

## How to work

Use the following loop for every feature:

1. Read the relevant source or chapter before coding.
2. Write down the inputs, outputs, and mathematical rule in your own words.
3. Implement the smallest version that can be tested.
4. Create a tiny example that makes the result visible.
5. Add automated tests for normal cases and boundary cases.
6. Debug with breakpoints, printed values, and a debugger.
7. Commit the working milestone.
8. Only then improve the design or performance.

When stuck, do not immediately add more code. Reduce the problem to a small
case, such as one pixel, one line, or one triangle. Draw the expected result
on paper or in a notebook and compare it with the program's output.

Keep a short handwritten or local development log. Record what the algorithm
should do, what it actually did, and what changed when the bug was fixed.

## Repository direction

The current repository contains a console project under `Rasterizer/`. Start
there, but separate responsibilities as the project grows:

```text
Rasterizer.sln
  Rasterizer.Core/       Image buffer and rendering algorithms
  Rasterizer.Formats/    PPM, TGA, and later other image formats
  Rasterizer.App/        Console demos and, later, a window
  Rasterizer.Tests/      Unit and image-output tests
```

Do not create all of these projects immediately. The first few milestones can
remain in the existing project. Add a project when the separation solves a
real problem.

The current project targets .NET 9. Keep using that target unless there is a
specific reason to change it. Learn the language features already used by the
project before adding third-party dependencies.

## Milestone 0: Learn the minimum C# and .NET

### Goal

Become comfortable enough with the language to write small programs without
confusing language problems with graphics problems.

### Learn

- variables, expressions, and value types;
- classes, structs, records, and enums;
- methods and constructors;
- arrays and loops;
- `if`, `switch`, and exceptions;
- namespaces and files;
- properties and access modifiers;
- nullable reference types;
- project files and `dotnet` commands;
- basic debugging in the IDE.

### Exercises

Write small console programs that:

1. print a multiplication table;
2. calculate the distance between two 2D points;
3. find the minimum and maximum value in an array;
4. fill and print a two-dimensional grid;
5. read a text file and write another text file.

### Test/checklist

- The program builds without warnings that you do not understand.
- You can explain the difference between an `int`, `float`, `byte`, and
  `string`.
- You can set a breakpoint and inspect a variable.
- You can run the project with `dotnet run`.
- You can build it with `dotnet build`.
- You can explain which code is in the application and which code is supplied
  by the .NET runtime.

### Sources

- [Microsoft C# documentation](https://learn.microsoft.com/dotnet/csharp/)
- [C# fundamentals](https://learn.microsoft.com/dotnet/csharp/fundamentals/)
- [.NET CLI overview](https://learn.microsoft.com/dotnet/core/tools/)
- [C# debugger documentation](https://learn.microsoft.com/visualstudio/debugger/)

## Milestone 1: Represent an image in memory

### Goal

Create a small image type that stores pixels in a one-dimensional array.

Suggested concepts:

```text
Color: red, green, blue, alpha
Image: width, height, pixel array
SetPixel(x, y, color)
GetPixel(x, y)
Clear(color)
```

Use a one-dimensional array with the index:

```text
index = y * width + x
```

Choose an origin convention and document it. A good first choice is `(0, 0)`
at the top-left corner, with X increasing rightward and Y increasing
downward.

### Test/checklist

Test all of the following:

- an image has the requested width and height;
- all pixels initially have a defined value;
- `Clear` changes every pixel;
- setting `(0, 0)` changes the first pixel;
- setting the bottom-right pixel changes the correct array element;
- pixels outside the image do not corrupt another pixel;
- negative coordinates are handled according to the documented policy;
- invalid image dimensions are rejected;
- `GetPixel` and `SetPixel` agree on valid coordinates;
- a 1x1 image works;
- a non-square image, such as 3x7, works.

Write a small demo that creates an image, clears it, changes a few pixels, and
prints a simple text representation of the pixels.

### Sources

- [Microsoft arrays documentation](https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/arrays)
- [Microsoft structs documentation](https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/struct)
- [System.Numerics namespace](https://learn.microsoft.com/dotnet/api/system.numerics)

## Milestone 2: Save the image

### Goal

Make the image visible outside the program by saving it to a file.

Start with **PPM**. It is intentionally simple and useful for learning:

- write a P3 text PPM first;
- validate the output using an image viewer;
- optionally add P6 binary PPM later.

Do not start with PNG. PNG is a compressed format with more details than are
useful for the first rasterizer lesson.

### Test/checklist

- a 1x1 image saves and opens;
- a solid red image has the expected pixel values;
- a checkerboard has the expected dimensions and colors;
- width and height are written correctly;
- no unexpected extra pixels are written;
- a color with all channels at 0 is black;
- a color with all channels at the maximum is white;
- the output path is reported clearly when saving fails;
- file streams are disposed correctly;
- a saved image can be loaded by a separate image viewer.

Keep one or two small output images as milestone artifacts. Visual inspection
is valuable, but it should supplement automated tests rather than replace them.

### Sources

- [Netpbm PPM format](https://netpbm.sourceforge.net/doc/ppm.html)
- [Microsoft file and stream I/O](https://learn.microsoft.com/dotnet/standard/io/)
- [FileStream API](https://learn.microsoft.com/dotnet/api/system.io.filestream)

## Milestone 3: Draw lines

### Goal

Implement a line rasterizer. Begin with a simple algorithm, then implement
Bresenham's algorithm and compare the results.

The line routine must handle:

```text
horizontal lines
vertical lines
diagonal lines
steep lines
lines drawn in either direction
lines partly outside the image
single-point lines
```

### Test/checklist

For each test, verify exact pixels, not only whether an image "looks right":

- a horizontal line covers the expected endpoints;
- a vertical line covers the expected endpoints;
- a line from left to right equals the reverse line;
- steep and shallow slopes both work;
- negative coordinates do not throw unexpectedly;
- lines touching an image boundary work;
- lines completely outside the image do not change it;
- a one-point line changes exactly one pixel;
- the algorithm does not produce duplicate or skipped pixels that violate its
  documented endpoint convention.

Use a 10x10 image for tests so that expected pixels can be listed manually.

### Sources

- [TinyRenderer lesson 1](https://haqr.eu/tinyrenderer/line/)
- [Bresenham's line algorithm](https://en.wikipedia.org/wiki/Bresenham%27s_line_algorithm)
- [Amit Patel's line drawing notes](https://www.redblobgames.com/grids/line-drawing/)

## Milestone 4: Draw and fill triangles

### Goal

Render triangle outlines and solid triangles. First use a simple bounding-box
algorithm. Do not optimize until the behavior is correct.

For each triangle:

1. Calculate its bounding box.
2. Clamp the box to the image.
3. Visit every candidate pixel.
4. Determine whether the pixel is inside the triangle.
5. Write the color when it is inside.

You can initially use edge functions or barycentric coordinates.

### Test/checklist

- a triangle with horizontal, vertical, and diagonal edges works;
- clockwise and counter-clockwise vertex order produce the documented result;
- a degenerate triangle does not crash;
- a triangle of zero area writes no pixels, or follows a documented policy;
- a triangle partly outside the image is clipped safely;
- a triangle larger than the image does not overflow;
- a triangle touching each image edge works;
- a one-pixel triangle behaves consistently;
- the filled triangle contains its expected interior pixels;
- the outline and filled versions agree on edge conventions.

Create several output images:

- one triangle in the center;
- triangles touching all four borders;
- overlapping triangles in different colors;
- triangles with very different shapes.

### Sources

- [TinyRenderer lesson 2](https://haqr.eu/tinyrenderer/triangle/)
- [Barycentric coordinates](https://en.wikipedia.org/wiki/Barycentric_coordinate_system)
- [Scratchapixel: rasterization](https://www.scratchapixel.com/lessons/3d-basic-rendering/rasterization-practical-implementation/)

## Milestone 5: Interpolate values across triangles

### Goal

Use barycentric coordinates to interpolate a value from the triangle vertices
to each covered pixel.

Start with colors. Later use the same idea for depth, texture coordinates, and
normals.

### Test/checklist

- a triangle with one red, one green, and one blue vertex produces a smooth
  color gradient;
- each vertex receives approximately its assigned color;
- the triangle center has the expected weighted color;
- interpolation weights sum to approximately one;
- points outside the triangle are rejected;
- reversing vertex order does not unexpectedly change colors;
- interpolation remains stable for small triangles;
- channel values are clamped or converted according to a documented rule.

Use floating-point values during interpolation and convert to bytes only when
writing a pixel.

### Sources

- [TinyRenderer lesson 2](https://haqr.eu/tinyrenderer/triangle/)
- [Scratchapixel: barycentric coordinates](https://www.scratchapixel.com/lessons/3d-basic-rendering/rasterization-practical-implementation/barycentric-coordinates.html)
- [Scratchapixel: perspective-correct interpolation](https://www.scratchapixel.com/lessons/3d-basic-rendering/rasterization-practical-implementation/perspective-correct-interpolation.html)

## Milestone 6: Add automated tests and image comparisons

### Goal

Make regressions visible when the renderer changes.

Create a test project. Unit-test the math and buffer operations separately from
the image-writing code.

Use exact pixel assertions for small images. For larger images, use a
reference image only after defining what differences are acceptable.

### Test/checklist

- tests run from the command line;
- tests fail when the implementation is intentionally broken;
- tests cover image boundaries;
- tests cover degenerate geometry;
- tests cover color conversion;
- tests cover file format headers;
- tests cover deterministic output;
- test names explain the behavior being checked;
- a failed image comparison reports useful coordinates and values.

Do not use screenshots of a window as the first testing mechanism. A saved
image buffer is easier to compare and debug.

### Sources

- [Microsoft .NET testing overview](https://learn.microsoft.com/dotnet/core/testing/)
- [xUnit documentation](https://xunit.net/)
- [Microsoft test project guidance](https://learn.microsoft.com/dotnet/core/testing/unit-testing-best-practices)

## Milestone 7: Add vectors and matrices

### Goal

Prepare the renderer for 3D coordinates.

Learn:

```text
Vector2, Vector3, Vector4
dot product
cross product
vector normalization
matrix multiplication
translation
rotation
scaling
view transformation
perspective projection
```

Use `System.Numerics` initially if it helps you focus on the rendering pipeline.
Implement selected operations yourself in separate exercises so that the
mathematics is understood rather than hidden.

### Test/checklist

- zero vectors behave correctly;
- adding and subtracting vectors works;
- dot products match hand calculations;
- cross products are perpendicular to both inputs;
- normalized vectors have length approximately one;
- identity matrices do not change a vector;
- translation moves a point but not a direction;
- scaling changes lengths as expected;
- rotation preserves length;
- matrix multiplication order is documented and tested;
- singular or invalid operations have a defined policy.

Use tolerances for floating-point comparisons instead of exact equality.

### Sources

- [System.Numerics.Vector3](https://learn.microsoft.com/dotnet/api/system.numerics.vector3)
- [System.Numerics.Matrix4x4](https://learn.microsoft.com/dotnet/api/system.numerics.matrix4x4)
- [Scratchapixel: matrices](https://www.scratchapixel.com/lessons/mathematics-physics-for-computer-graphics/geometry/transforming-points-and-vectors.html)
- [LearnOpenGL coordinate systems](https://learnopengl.com/Getting-started/Coordinate-Systems)

## Milestone 8: Render a 3D wireframe model

### Goal

Project 3D vertices onto the 2D image and render model edges.

Implement this sequence:

```text
model coordinates
-> world coordinates
-> camera/view coordinates
-> projection coordinates
-> perspective divide
-> screen coordinates
```

Start with hard-coded vertices for a cube. Add OBJ loading only after the cube
works.

### Test/checklist

- a cube can be rendered from several camera positions;
- moving the camera changes the image predictably;
- changing field of view changes apparent size;
- objects farther from the camera appear smaller under perspective;
- vertices behind the camera are handled safely;
- screen coordinates map to the correct image pixels;
- the cube remains centered when expected;
- wireframe edges do not access pixels outside the image;
- an OBJ with a single triangle loads correctly;
- malformed OBJ input produces a clear error.

### Sources

- [TinyRenderer lesson 3](https://haqr.eu/tinyrenderer/model/)
- [TinyRenderer lesson 4](https://haqr.eu/tinyrenderer/geometry/)
- [Wavefront OBJ overview](https://en.wikipedia.org/wiki/Wavefront_.obj_file)
- [Scratchapixel: projection](https://www.scratchapixel.com/lessons/3d-basic-rendering/3d-viewing-pinhole-camera.html)

## Milestone 9: Filled 3D triangles and the depth buffer

### Goal

Render solid 3D triangles and make nearer surfaces hide farther surfaces.

Add a depth buffer with one depth value per image pixel. Clear it at the start
of each frame. For each covered pixel:

1. interpolate depth;
2. compare it with the stored depth;
3. write the color and new depth only when the new value is nearer.

### Test/checklist

- the depth buffer clears between frames;
- a nearer triangle hides a farther triangle;
- reversing draw order produces the same visible result;
- equal-depth behavior is documented;
- depth values are interpolated correctly;
- a triangle partly behind the camera is handled safely;
- depth-buffer dimensions always match image dimensions;
- a large depth value does not overflow or create invalid results;
- a rotating cube has no incorrectly visible rear faces.

### Sources

- [TinyRenderer lesson 5](https://haqr.eu/tinyrenderer/z-buffer/)
- [Scratchapixel: depth buffering](https://www.scratchapixel.com/lessons/3d-basic-rendering/rasterization-practical-implementation/z-buffering.html)
- [LearnOpenGL depth testing](https://learnopengl.com/Advanced-OpenGL/Depth-testing)

## Milestone 10: Back-face culling and lighting

### Goal

Reduce unnecessary work and make the 3D model visually understandable.

Implement:

```text
triangle normals
face orientation
back-face culling
ambient light
diffuse Lambert lighting
```

Start with one directional light. Keep the lighting calculation separate from
the rasterizer so that both can be tested independently.

### Test/checklist

- front-facing triangles are rendered;
- back-facing triangles are culled when enabled;
- culling can be disabled;
- reversing winding order changes culling as documented;
- a face directly facing the light is brighter than a side face;
- a face facing away from the light receives no negative diffuse light;
- normal vectors are normalized before lighting;
- changing the light direction changes brightness predictably.

### Sources

- [TinyRenderer lesson 6](https://haqr.eu/tinyrenderer/lighting/)
- [Scratchapixel: flat shading](https://www.scratchapixel.com/lessons/3d-basic-rendering/introduction-to-shading/flat-shading.html)
- [LearnOpenGL lighting](https://learnopengl.com/Lighting/Basic-Lighting)

## Milestone 11: Textures and texture coordinates

### Goal

Map an image onto a triangle or 3D model.

Implement:

```text
texture coordinates
nearest-neighbor sampling
texture wrapping or clamping
perspective-correct interpolation
```

Use a generated checkerboard before using external image files. This makes
incorrect coordinates obvious.

### Test/checklist

- a checkerboard maps to a rectangle correctly;
- all four corners use the expected texture corners;
- nearest-neighbor sampling is deterministic;
- coordinates at 0 and 1 follow the documented convention;
- out-of-range coordinates wrap or clamp as documented;
- a perspective-distorted triangle does not visibly shear the texture
  incorrectly;
- a missing texture produces an explicit error;
- texture dimensions are validated.

### Sources

- [TinyRenderer lesson 7](https://haqr.eu/tinyrenderer/texture/)
- [Scratchapixel: texture mapping](https://www.scratchapixel.com/lessons/3d-basic-rendering/introduction-to-texturing/introduction-to-texturing.html)
- [LearnOpenGL textures](https://learnopengl.com/Getting-started/Textures)

## Milestone 12: Clipping and a complete software pipeline

### Goal

Handle triangles that cross the camera or screen boundaries correctly.

Implement clipping gradually:

1. screen-boundary clipping;
2. near-plane clipping;
3. remaining view-frustum planes;
4. perspective-correct interpolation after clipping.

### Test/checklist

- triangles entirely inside the view are unchanged;
- triangles crossing each screen edge are clipped without crashes;
- triangles crossing the near plane do not produce invalid huge geometry;
- triangles entirely outside the view produce no pixels;
- clipped triangles preserve color, depth, and texture interpolation;
- no NaN or infinity reaches the pixel-writing stage;
- extreme but valid camera values are handled or rejected clearly.

### Sources

- [TinyRenderer lesson 8](https://haqr.eu/tinyrenderer/afperspective/)
- [Scratchapixel: clipping](https://www.scratchapixel.com/lessons/3d-basic-rendering/rasterization-practical-implementation/clipping.html)
- [LearnOpenGL face culling and clipping concepts](https://learnopengl.com/Advanced-OpenGL/Face-culling)

## Milestone 13: Display the image in a window

### Goal

Show the software-rendered image interactively without placing windowing code
inside the core rasterizer.

Keep this separation:

```text
Core renderer -> Image pixel buffer -> presentation layer -> window
```

Choose one presentation technology only after the saved-image renderer works.
SDL is a reasonable low-level choice for a first interactive application.
Avalonia is more suitable if a desktop UI is a major goal.

### Test/checklist

- the window opens and closes cleanly;
- the image has the correct orientation;
- the image has the correct color channel order;
- resizing behavior is documented;
- keyboard input is received;
- a camera can be moved;
- the application exits without hanging;
- the core renderer can still run without opening a window;
- window failures are reported clearly.

### Sources

- [SDL3 documentation](https://wiki.libsdl.org/SDL3/FrontPage)
- [Silk.NET documentation](https://dotnet.github.io/Silk.NET/)
- [Avalonia documentation](https://docs.avaloniaui.net/)
- [Microsoft .NET application architecture guidance](https://learn.microsoft.com/dotnet/architecture/)

## Milestone 14: Measure before optimizing

### Goal

Measure the renderer and improve real bottlenecks instead of guessing.

Measure:

```text
frame time
triangles processed
pixels tested
pixels written
memory allocations
image size
```

Only after measurement consider:

- reducing allocations;
- using `Span<T>`;
- reusing buffers;
- using `ArrayPool<T>`;
- improving bounding boxes;
- parallelizing independent work;
- changing memory layout.

### Test/checklist

- a repeatable benchmark scene exists;
- measurements are made in Release mode;
- warm-up and repeated runs are understood;
- correctness tests still pass after optimization;
- allocation changes are measured rather than assumed;
- performance is compared before and after each change;
- optimization does not make the public API unclear without a demonstrated
  benefit.

### Sources

- [BenchmarkDotNet](https://benchmarkdotnet.org/)
- [Microsoft .NET performance guidance](https://learn.microsoft.com/dotnet/framework/performance/)
- [Microsoft memory and spans guidance](https://learn.microsoft.com/dotnet/standard/memory-and-spans/)
- [dotnet-trace](https://learn.microsoft.com/dotnet/core/diagnostics/dotnet-trace)

## Milestone 15: Packaging, documentation, and portability

### Goal

Make the project understandable and usable by another developer.

Document:

- coordinate-system conventions;
- color format and channel order;
- image origin;
- depth range and comparison direction;
- matrix multiplication order;
- texture-coordinate conventions;
- supported input formats;
- supported operating systems;
- known limitations;
- how to run examples and tests.

Keep the core renderer independent from a specific windowing system. Add
platform-specific presentation implementations only when there is a concrete
test application for them.

### Test/checklist

- a fresh clone can build using documented commands;
- all tests pass on a clean build;
- at least one sample image can be generated;
- the public API has XML documentation where useful;
- unsupported platforms are stated honestly;
- a release build can be copied to a new directory and run;
- no generated `bin` or `obj` files are required in source control;
- the license and third-party notices are present where needed.

### Sources

- [Microsoft .NET deployment overview](https://learn.microsoft.com/dotnet/core/deploying/)
- [Microsoft class library guidance](https://learn.microsoft.com/dotnet/standard/library-guidance/)
- [Semantic Versioning](https://semver.org/)
- [Git documentation](https://git-scm.com/doc)

## Recommended first six weeks

This schedule is only a suggestion. Understanding is more important than
finishing a calendar week.

### Week 1

- complete the basic C# exercises;
- understand the existing project and `dotnet build`;
- implement `Color` and `Image`;
- test `SetPixel`, `GetPixel`, and `Clear`.

### Week 2

- implement PPM output;
- create solid-color and checkerboard images;
- implement lines;
- test boundary and reverse-direction cases.

### Week 3

- implement triangle outlines;
- implement filled triangles;
- draw several diagnostic images;
- add unit tests for degenerate and clipped triangles.

### Week 4

- implement barycentric coordinates;
- interpolate colors;
- add the test project;
- commit a stable 2D rasterizer milestone.

### Week 5

- learn vectors and matrices;
- render a transformed wireframe cube;
- test coordinate-system and matrix conventions.

### Week 6

- add filled 3D triangles;
- add a depth buffer;
- render overlapping triangles;
- do not add a window until the saved output is correct.

## Definition of a successful first version

The first meaningful version is complete when it can:

1. create an image buffer;
2. clear it;
3. draw pixels, lines, and filled triangles;
4. interpolate colors;
5. save a deterministic image file;
6. pass automated tests for boundaries and degenerate inputs;
7. render a rotating or transformed cube;
8. correctly hide farther triangles with a depth buffer.

At that point you have a real software rasterizer. Everything after that is
an extension: lighting, textures, clipping, windows, performance, GPU
backends, and portability.

## First action to take now

Do not begin with TGA, a window, or 3D. Begin with this short task:

1. Create an `Image` type.
2. Create a `Color` type.
3. Add `Clear`, `SetPixel`, and `GetPixel`.
4. Generate a 16x16 image.
5. Set four different corner colors.
6. Save a PPM file.
7. Open the file and verify it visually.
8. Add tests for the image dimensions and four corner pixels.

Once that works, move to line drawing. Every later stage builds on this small
foundation.
