# Rube Goldberg challenge
## Davide Zordan

This project implements a game targeting Oculus Rift using the SteamVR SDK.
The goal is to use the controllers to control the ball and collect all the stars available in the environment.
Only one launch is available from the platform, and with it the goal needs to be reached after collecting all the stars available.
Some helpers objects are available to construct a path for complete the game.
Teleportation is also available using the left triggers.

Have fun!

![Screenshot](Screenshot.png)

## Getting Started

# Build and Test
It's possible to test the project using Unity. A build targetinng SteamVR and Oculus Rift is available in the "Build" folder.

# Using Unity
Scenes required (the order should be respected):

- RubeGoldberg\Assets\Scenes\Level 1.unity
- RubeGoldberg\Assets\Scenes\Level 2.unity
- RubeGoldberg\Assets\Scenes\Level 3.unity
- RubeGoldberg\Assets\Scenes\Level 4.unity

# Steps:
- Unzip the zip file
- Launch Unity (the project is targeting Unity 2017.4.15f1)
- Open the project located under the folder “RubeGoldberg"
- Open the scene "Level 1" to explore the hierarchy
- VR mode has been set on the Desktop build platform in the Build settings
- Launch Unity player to experience the game in the headset

# Versions Used
- [Unity LTS Release 2017.4.15](https://unity3d.com/unity/qa/lts-releases?version=2017.4)
- [SteamVR Unity plugin v2.2RC2](https://github.com/ValveSoftware/steamvr_unity_plugin/tree/master/Assets/SteamVR)
