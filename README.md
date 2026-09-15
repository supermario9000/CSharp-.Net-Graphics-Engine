# C#-.Net-Graphics-Engine
A low level graphics rasterisatoin and processing engine created in C# and .Net for the 5th semester's course of Software Engineering at VU. The progress specifically for the course will be marked in separate releases. That way it will be easier to distinguish what has been done in uni or out.

Disclaimer: The university course doesn't require students to build something like this. This is something that I research and develop on my own, for my own personal use. The course only states that a project must be built with the .Net framework.

The primary goal is to learn how to manipulate the physical pixels of a display.

The tasks of this project are as follows:
1. Create a rasterization engine, which let's me draw pixels on screen or on a picture freely. This step requires thorough programming because it is the lowest level, which targets not only the screen but also the CPU and GPU of the computer.
2. Create a layer to manipulate those images and display them on a window. (sort of like SFML)
3. Always monitor hardware usage and make sure code is optimized for all device use.
4. Create a layer of 3D, turning the images displayed into a 3D world. (Like SFML+OpenGL)
5. At that point the graphics engine should be basically done. Optimize for other OS and document the full project's methods.

The goal would be finally achieved IF the product works easily by a plug-and-play principle on any mainstream OS (including iPadOS), except mobile.

Development for mobile is not preferred because using the product on mobile would require a decent text editors etc. 
The only requirement regarding mobile is that this tool could be used in other projects which build for mobile. For example if developing a mobile application for Android, a person may use this engine to drive progress.

Considering all of the above, this project's repo will be branched out for easier sub-system development.
Later, I will consider reworking on this project only in C++. Or even in assembly. To better understand computer graphics.
