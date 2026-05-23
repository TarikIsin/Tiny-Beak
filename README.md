🐣 TinyBeak

A 3D platformer built in Unity where you play as a chick navigating a kitchen environment — collecting eggs while avoiding hazards and outsmarting a roaming cat.

🎮 Gameplay Overview
TinyBeak is a 3D kitchen-themed platformer. The player controls a small chick navigating elevated countertops, collecting 5 eggs to win — while managing health, movement modifiers, and a persistent threat from a cat AI below.
Win condition: Collect all 5 eggs
Lose conditions: Run out of lives (fire contact) or get caught by the cat

✨ Features & Technical Highlights
🧠 AI — Cat Behavior (NavMesh)

The cat roams the kitchen floor randomly using Unity's NavMesh pathfinding
When the player falls to the ground level, the cat dynamically switches to a chase state and pursues the player
Uses a state machine pattern (Idle / Roam / Chase) driven by proximity and player height detection

🌾 Power-up System — Wheat Collectibles
Three distinct wheat types with different runtime effects applied to the player controller:
Wheat TypeEffectSpeed WheatIncreases movement speedJump WheatIncreases jump forceSlow WheatReduces movement speed

Effects are time-limited and managed via coroutines
Each wheat type uses a ScriptableObject for configurable values without code changes

🔥 Hazard System — Stove Fire

Stoves on countertops emit Particle System fire effects
Collision with fire particles triggers a damage event and decrements player health
Health system supports multiple lives with visual UI feedback

🪃 Spatula Launch Mechanic

Spatulas placed near counters act as launch pads
On contact, applies an upward impulse force to the player's Rigidbody, enabling vertical traversal

🎵 Audio System

Full AudioManager with singleton pattern
Separate volume controls for SFX and Music persisted via PlayerPrefs
Sound effects on: egg collection, wheat pickup, fire damage, cat catch, UI interactions
Button hover sounds implemented via EventTrigger components on UI elements

🖥️ UI & Menu Systems

Main Menu with play, settings, and quit options
Settings Panel: master/SFX/music volume sliders, return to menu button
HUD: live health display, egg counter (collected / total)
Pause Menu: resume, settings access, main menu navigation
Win / Game Over screens with restart and menu options
Scene transitions handled via SceneManager with a fade animation

⚙️ Architecture Notes

Player input handled via Unity's Input System
Game state managed with a GameManager singleton (handles win/lose/pause logic)
Modular component design: hazards, collectibles, and power-ups each have standalone scripts
Physics-based movement using Rigidbody with custom ground detection via raycasts


🛠️ Built With
Unity
C#
Unity NavMesh for AI pathfinding
Unity Particle System for fire hazards
Unity Input System (new)
Unity UI Toolkit / Legacy UI (Canvas)


🧩 What I Learned / Challenges

Implementing a dual-state AI that transitions smoothly between roaming and chasing based on player position
Designing a modular power-up system using ScriptableObjects to keep wheat behavior data-driven and designer-friendly
Managing particle-based collision detection for fire hazards without performance overhead
Building a centralized AudioManager that persists across scenes and responds to UI settings in real time
