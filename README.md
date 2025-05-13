# ZOMBAE (Sprite Shooter Game)

## Description

ZOMBAE is a simple 2D game developed using MonoGame/XNA in C#. The player controls a sprite using the WASD keys, navigating through various challenges while facing different types of enemies. The game includes various animations based on the sprite's direction, an enemy spawn system, and a scoring mechanism.

## Objective

The objective of the game is to survive for 60 seconds while shooting down enemies that appear from the corners of the screen. Each enemy type has a distinct speed and damage value, contributing to a dynamic gameplay experience.

## Gameplay/Technical Features

- **Buttons**: In the first game state, there is an interactive button that can be pressed to open up the playing game state.
- **Collision Detection**: For collision detection I've used bounding boxes around four of the sprites. Collisions are detected when a bullet hits an enemy and when an enemy touches the player.
- **Player Control**: Move the sprite using the `W`, `A`, `S`, and `D` keys.
- **Sprite Animations**: Using a sprite sheet, the sprite has different animations using a depending on the direction it's facing (up, down, left, right).
- **Enemy Types**: 
  - Three different types of enemies that spawn from the corners of the screen.
  - Each enemy has varying speeds and damage values to the player's health.
- **Score & Health System**: I have implemented both a score and health system. The score increases if an enemy is hit with a bullet, and both health and score decrease when an enemy collides with the player
- **Timer**: A countdown timer set to 60 seconds for each game session, which then exits the game upon reaching 0.
