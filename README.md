# C#-.Net-Graphics-Engine
A low level graphics rasterisatoin and processing engine created in C# and .Net for the 5th semester's course of Software Engineering at VU. The progress specifically for the course will be marked in separate releases. That way it will be easier to distinguish what has been done in uni or out.

Disclaimer: The university course doesn't require students to build something like this. This is something that I research and develop on my own, for my own personal use. The course only states that a project must be built with the .Net framework.

# What's done

## Overview

I created three classes: Pixel, Image and Program.  
* Pixel is a class which represents the pixel. It has only 4 parameters for color: R,G,B and A. The position is determined in the image, so it is not a parameter of Pixel.
* Image is a collection of pixels. It has three parameters: width, height and ekranas. Width and height are in pixels, while ekranas is an array of Pixel objects.
* Program is where everything happens. It has experiments, image saving and printing, everything that's needed for development. No parameters, only termporary variables.  

I also created a few functions to draw on the image. Currently the user can:  
1. change specified pixels,
2. clear the image (or set it as one color),
3. draw a line.

Further functions are still in development with experiments 6-8. These include:
1. Drawing a square.
2. Drawing a triangle.
3. Drawing a circle.
4. Drawing a 3d cube.

## Experiments

Here's an example of an experiment (drawing a square with DrawLine method):  

P3
16 16
255
255 0 0  255 0 0  255 0 0  255 0 0  255 0 0  255 0 0  255 0 0  255 0 0  255 0 0  255 0 0  255 0 0  255 0 0  255 0 0  255 0 0  255 0 0  255 0 0  
255 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  255 0 0  
255 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  255 0 0  
255 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  255 0 0  
255 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  255 0 0  
255 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  255 0 0  
255 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  255 0 0  
255 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  255 0 0  
255 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  255 0 0  
255 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  255 0 0  
255 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  255 0 0  
255 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  255 0 0  
255 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  255 0 0  
255 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  255 0 0  
255 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  0 0 0  255 0 0  
255 0 0  255 0 0  255 0 0  255 0 0  255 0 0  255 0 0  255 0 0  255 0 0  255 0 0  255 0 0  255 0 0  255 0 0  255 0 0  255 0 0  255 0 0  255 0 0   

What it looks like in the console:   
Experiment 5:  

RRRRRRRRRRRRRRRR  
R..............R  
R..............R  
R..............R  
R..............R  
R..............R  
R..............R  
R..............R  
R..............R  
R..............R  
R..............R  
R..............R  
R..............R  
R..............R  
R..............R  
RRRRRRRRRRRRRRRR  

# Project tasks
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


Project is inspired by https://haqr.eu/tinyrenderer/ tutorial. While I am not following it, it is a good reference if I am lost or for comparison.
