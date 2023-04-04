# Rube Goldberg
### Davide Zordan

This project implements a game targeting Meta Quest 2/Pro and touch controllers using the Unity XRI plugin.

The goal is to collect all the stars available in the environment with a single launch of the ball from the platform and then reach the goal target.
Some helpers objects are available to construct a path for completing the game including: 2 types of planks, a fan, a trampoline and teleportation platforms for the ball.

The user can navigate the environment using the touch controllers and teleportation using the left hand thumbstick.
The object selection menu can be activated with the right thumbstick.
Trigger controls permit to interact and interact with objects using the trigger buttons.

![Screenshot](Screenshot.png)
![Touch controllers input](Screenshot-1.png)
![Teleportation](Screenshot-2.png)
![Positioning Objects](Screenshot-3.png)

## Getting Started

# Build and Test

# Using Unity
Scenes required (the order should be respected):

- RubeGoldberg\Assets\Scenes\Level 1.unity
- RubeGoldberg\Assets\Scenes\Level 2.unity
- RubeGoldberg\Assets\Scenes\Level 3.unity
- RubeGoldberg\Assets\Scenes\Level 4.unity

# Steps:
- Unzip the zip file
- Launch Unity (the project is targeting Unity 2021.3.22f1)
- Open the project located under the folder “RubeGoldberg"
- Open the scene "Level 1" to explore the hierarchy
- VR mode has been set on the Android build platform in the Build settings
- Build Android package and deploy to the headset to experience the game

# Versions Used
- [Unity LTS Release 2021.3.22f1](https://unity3d.com/unity/qa/lts-releases?version=2021.3)
- [Unity XR Interaction Toolkit v2.3.0](https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@2.3/manual/installation.html)
