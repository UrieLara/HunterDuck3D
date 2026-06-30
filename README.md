# 🦁 Hunter 3D

<p align="center">
  <img src="Screenshots/menu-principal.png" width="45%">
  <img src="Screenshots/gameplay.png" width="45%">
</p>

First-person hunting game prototype developed in Unity, featuring randomized objectives, enemy AI, Object Pooling, and an Object-Oriented architecture based on inheritance, polymorphism, the Singleton pattern, and a finite state machine (FSM).

## 🎮 Gameplay

Complete the hunting list while surviving attacks from wild animals. Defeat every objective to win before losing all your lives.

## ✨ Features

- First-person gameplay
- Randomized hunting objectives
- Enemy AI
- Raycast shooting
- Dynamic HUD
- Pause system
- Victory and defeat states
- Object Pooling for animals and particles

## 🏗️ Architecture

- **GameManager** — Controls the game flow and state machine.
- **MissionManager** — Generates and tracks hunting objectives.
- **Player** — Handles player health and animations.
- **HUD** — Updates the user interface.
- **FactoryAnimalsPooling** — Reuses animal instances.
- **FactoryParticlesPooling** — Reuses particle effects.

## 💡 Programming Concepts

- Object-Oriented Programming (OOP)
- Inheritance
- Polymorphism
- Encapsulation
- Abstract Classes
- Singleton Pattern
- Object Pool Pattern
- Finite State Machine (FSM)
- Coroutines
- Raycasting

## 🛠️ Built With

- Unity
- C#
- TextMesh Pro

## 🎮 Play

👉 **Play on itch.io:** [urielara](https://urielara.itch.io/hunter3d)
