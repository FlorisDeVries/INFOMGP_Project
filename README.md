# INFOMGP Project

For the 2018-2019 INFOMGP Project, we created a game/sandbox based on the laws of gravity.

We wrote a governing gravity simulation class, that takes into account all present bodies and calculates the gravitational forces they apply to eachother. In the last step of the simulation tick, we integrate the new velocities and move the bodies accordingly.

![Levels](/screen_1.png)

We created a few levels to play with the gravity simulation, in which you control a rocket at liftoff. Your goal is to set the rocket in the right path to its destination, avoiding obstacles and performing fly-bys.

![Solar System](/screen_2.png)

While this level mode is fun to mess around in, it sets some unrealistic values for masses and pins certain bodies to a fixed position to improve gameplay. To also show the full power of the simulation, we modeled the solar system and all its bodies in a seperate scenario. Here, you can navigate through the solar system, change the time step, and zoom in or out.

![Sandbox mode](/screen_3.png)

Finally, we provided a sandbox mode where you can create your own gravitational playground. You can place bodies, edit their velocities and masses, and perhaps try to emulate a binary star system!

## Installation

A build for Windows can be found [here](https://github.com/FlorisDeVries/INFOMGP_Project/releases/tag/0.1).
To check out our source code or create builds for other platforms, clone this repository and open the project in [Unity](https://unity.com/) (recommended version 2018.3.10 or up).

## Credits

Music from https://filmmusic.io

All by Kevin MacLeod (https://incompetech.com)

Licence: CC BY (http://creativecommons.org/licenses/by/4.0/)
