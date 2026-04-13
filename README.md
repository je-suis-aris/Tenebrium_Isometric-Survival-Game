<h1 align="center"><strong>Tenebrium</strong></h1>
<h3 align="center" style="color: gray; font-weight: normal;">isometric survival RTS built in Unity with real-time exploration, gathering and zombie encounters</h3>

<p align="center">
  <strong>Author:</strong> Aris-Georgian ILIE &nbsp;|&nbsp; 
  <strong>Engine:</strong> Unity &nbsp;|&nbsp;
  <strong>Language:</strong> C#
</p>

---

## TABLE OF CONTENTS

- [DESCRIPTION](#description)
- [MOTIVATION](#motivation)
- [ARCHITECTURE](#architecture)
  - [GAMEPLAY OVERVIEW](#gameplay-overview)
  - [TECHNICAL STRUCTURE](#technical-structure)
- [FEATURES](#features)
- [HOW TO PLAY](#how-to-play)
- [PROJECT STRUCTURE](#project-structure)
- [DEVELOPMENT NOTES](#development-notes)
- [TECHNOLOGIES USED](#technologies-used)
- [SCREENSHOTS](#screenshots)
- [FUTURE IMPROVEMENTS](#future-improvements)

---

## <span style="color:gray;">DESCRIPTION</span>

<p align="justify">
<strong>Tenebrium</strong> is an isometric survival RTS prototype developed in <strong>Unity with C#</strong>. The game combines real-time movement, resource gathering, inventory management, map exploration and zombie encounters inside a dark fantasy survival setting. The player controls a survivor on a 3D terrain viewed from an isometric perspective and interacts with the world through mouse input.
</p>

<p align="justify">
Movement is based on <strong>Unity NavMesh pathfinding</strong>. By clicking on the terrain, the player unit automatically navigates to the selected destination while playing the proper movement animations and footstep sounds. This creates smooth point-and-click control that fits the strategy-survival style of the project.
</p>

<p align="justify">
The world contains <strong>interactable resource objects</strong> and other collectible items. When the player gets close enough and interacts with a node, the object is added to the inventory, feedback sounds are played and the object is removed from the scene. The inventory supports stacking, drag and drop slot management and item consumption for healing. This makes the gathering loop useful both for progression and survival.
</p>

<p align="justify">
Combat is handled in real time. The player can target zombies and launch a projectile-based cure attack. When a zombie is hit, its infection level is reduced and once that value reaches zero, the zombie is transformed into a human survivor. This gives the project a distinct combat identity: instead of classic damage-only combat, the system is designed around <strong>curing infected enemies</strong>. Zombies use a behavior loop built with animator state machines and NavMesh movement, allowing them to idle, patrol, detect the player, chase and attack dynamically.
</p>

<p align="justify">
The game also includes important survival interface systems such as a <strong>health bar, game over screen, minimap, fullscreen map, fog of war, inventory UI and main menu options</strong>. Random spawning systems place resources, animals and enemies across the terrain while following placement rules related to altitude, slope and navigable areas. Altogether, the project delivers a complete playable loop built around <strong>exploration, collection, survival and enemy interaction</strong>.
</p>

---

## <span style="color:gray;">MOTIVATION</span>

<p align="justify">
The purpose of this project was to build a complete and playable Unity game prototype that brings together several important gameplay systems inside one coherent experience. Instead of focusing on a single mechanic, the goal was to combine character control, world interaction, enemy AI, UI systems, procedural placement and audio-visual feedback into one survival-oriented project.
</p>

<p align="justify">
A second objective was to practice the design of modular C# scripts in Unity. Each major system such as player movement, interaction, inventory, enemy logic, fog of war and map controls is separated into dedicated scripts, which makes the project easier to expand and maintain. This structure also helped test how gameplay systems communicate with one another in a real-time environment.
</p>

<p align="justify">
The project was also a way to explore how atmosphere and technical implementation can support each other. The dark environment, the minimap, the fog of war, the sound effects and the enemy conversion mechanic all work together to create a survival experience that is simple to understand but still interesting to play and extend.
</p>

---

## <span style="color:gray;">ARCHITECTURE</span>

### GAMEPLAY OVERVIEW

<p align="center">
  <img src="menu.PNG" alt="Main menu" width="700">
</p>

<p align="justify">
The game starts from a main menu where the player can begin a new session and open the options panel. From there, the player enters the map and controls the survivor using point-and-click movement. Exploration reveals the environment gradually through a fog of war system, while the minimap and large map help the player understand the surrounding space.
</p>

<p align="justify">
During exploration, the player can gather objects from the map, store them in the inventory and use consumable items when needed. Zombies move through the environment using a simple but reliable AI flow. Once the player enters their detection range, they switch from passive movement to active pursuit and attack. This creates constant pressure and gives meaning to movement, planning and healing.
</p>

---

### TECHNICAL STRUCTURE

<p align="center">
  <img src="map.PNG" alt="Map view" width="700">
</p>

<p align="justify">
From a technical point of view, the project is organized into several connected gameplay modules:
</p>

<p align="justify">
<strong>Player control</strong> is managed through <code>PlayerController.cs</code>, which handles NavMesh movement, click detection, animation speed blending and footstep audio playback.
</p>

<p align="justify">
<strong>World interaction</strong> is managed through <code>PlayerInteraction.cs</code> and <code>InteractableObject.cs</code>. These scripts detect right-click interactions, check interaction range, play animations and send collectible items into the inventory.
</p>

<p align="justify">
<strong>Inventory logic</strong> is built with <code>InventorySystem.cs</code>, <code>InventoryItem.cs</code>, <code>ItemSlot.cs</code> and <code>DragDrop.cs</code>. Together they support UI opening, item stacking, slot swapping, drag and drop and consumption of healing items.
</p>

<p align="justify">
<strong>Enemy behavior</strong> is built around <code>EnemyManager.cs</code> and animator state machine scripts such as <code>ZIdleState.cs</code>, <code>ZWalkState.cs</code>, <code>ZChaseState.cs</code> and <code>ZAttackState.cs</code>. This setup controls sight range, chasing, attacking, infection level and transformation into a human survivor.
</p>

<p align="justify">
<strong>World generation</strong> is handled by <code>AdvancedResourceSpawner.cs</code>, <code>EnemySpawner.cs</code> and <code>AnimalSpawner.cs</code>. These scripts place objects across the map using rules for valid terrain, slope limits, altitude limits and NavMesh compatibility.
</p>

<p align="justify">
<strong>UI and game state systems</strong> include <code>PlayerState.cs</code> for health and game over, <code>FogOfWar.cs</code> for visibility reveal, <code>MapController.cs</code> for minimap and large map toggling and <code>Menu_Controller.cs</code> / <code>LoadPrefs.cs</code> for settings persistence through <code>PlayerPrefs</code>.
</p>

---

## <span style="color:gray;">FEATURES</span>

<div align="justify">

### 1. Isometric real-time movement
- Point-and-click movement on terrain
- Navigation powered by Unity NavMesh
- Smooth animation transitions based on movement speed
- Footstep audio with timing that changes between walking and running

### 2. Resource gathering and interaction
- Interactable world objects detected through layers
- Range-based interaction to prevent unrealistic collection
- Harvest/interaction animation on use
- Item pickup sound and object removal from the world
- Easy connection between interactable objects and inventory items by item ID

### 3. Inventory system
- Inventory window toggle from keyboard
- Stackable items with configurable maximum stack size
- Drag and drop item management between slots
- Slot swapping when dropping on an occupied slot
- Consumable items that restore player health
- UI quantity text for stacked items

### 4. Zombie combat and conversion
- Right-click targeting of enemies
- Projectile-based cure attack
- Hit reaction and infection reduction
- Zombie transformation into a human survivor when fully cured
- Attack, hit and cure sound feedback
- Health bar / infection bar above enemies

### 5. Enemy AI
- Idle state
- Random patrol/walk state
- Player detection based on sight range
- Chase state with faster movement speed
- Attack state with repeated timed damage
- Animator-driven state machine behavior
- NavMesh pathfinding for enemy pursuit

### 6. Survival UI and world feedback
- Player health bar and health text
- Game over screen
- Minimap and fullscreen map
- Fog of war that clears around the player over time
- Floating and billboard effects for world objects
- Main menu with audio and gameplay settings
- Saved preferences using PlayerPrefs

### 7. Spawn and world population systems
- Procedural placement of resources
- Procedural placement of enemies
- Procedural placement of ambient animals
- Terrain validation using slope and altitude checks
- NavMesh-aware spawn placement for better gameplay reliability

</div>

---

## <span style="color:gray;">HOW TO PLAY</span>

<div align="justify">

### Controls

- <strong>Left Click</strong> → Move the player to the selected ground position
- <strong>Right Click</strong> → Interact with a nearby object or attack a zombie
- <strong>I</strong> → Open / close the inventory
- <strong>E</strong> → Consume a hovered consumable item in the inventory
- <strong>M</strong> → Toggle between minimap view and large map view
- <strong>Ctrl + H</strong> → Return to the main menu

### Basic gameplay loop

1. Start the game from the main menu
2. Explore the map and reveal hidden areas
3. Collect useful items and resources from interactable objects
4. Manage inventory space and item stacks
5. Avoid or engage zombies when they detect you
6. Use cure attacks to neutralize infected enemies
7. Stay alive by preserving health and using consumables when needed

</div>

---

## <span style="color:gray;">PROJECT STRUCTURE</span>

<div align="justify">

### Core gameplay scripts

- <code>PlayerController.cs</code>  
  Handles player navigation, click movement, animation speed updates and footstep sounds.

- <code>PlayerInteraction.cs</code>  
  Handles right-click interaction with enemies and collectable objects.

- <code>PlayerState.cs</code>  
  Manages player health, healing, damage reactions and game over logic.

- <code>CurePotion.cs</code>  
  Controls projectile movement toward a targeted enemy and applies the cure effect on impact.

### Inventory scripts

- <code>InventorySystem.cs</code>  
  Main inventory manager with slot handling and item spawning inside the UI.

- <code>InventoryItem.cs</code>  
  Stores stack size, consumable settings and item UI logic.

- <code>ItemSlot.cs</code>  
  Handles dropping and swapping items between inventory slots.

- <code>DragDrop.cs</code>  
  Supports drag and drop behavior for inventory objects.

### Enemy scripts

- <code>EnemyManager.cs</code>  
  Stores zombie combat values, sound logic, infection level and the cure transformation.

- <code>ZIdleState.cs</code>  
  Idle enemy state with detection checks.

- <code>ZWalkState.cs</code>  
  Random patrol movement on the NavMesh.

- <code>ZChaseState.cs</code>  
  Pursuit behavior when the player is detected.

- <code>ZAttackState.cs</code>  
  Attack logic with timed repeated damage.

### World and environment scripts

- <code>AdvancedResourceSpawner.cs</code>  
  Spawns resources on valid terrain positions.

- <code>EnemySpawner.cs</code>  
  Spawns enemy groups on valid and playable parts of the map.

- <code>AnimalSpawner.cs</code>  
  Adds ambient wildlife for world life and variation.

- <code>FogOfWar.cs</code>  
  Dynamically reveals areas around the player.

- <code>MapController.cs</code>  
  Toggles minimap and large map UI modes.

- <code>MinimapFollow.cs</code>  
  Keeps minimap tracking aligned with the player.

### Menu and utility scripts

- <code>Menu_Controller.cs</code>  
  Main menu, options, audio controls and gameplay settings.

- <code>LoadPrefs.cs</code>  
  Loads saved player preferences.

- <code>GameOverManager.cs</code>  
  Returns the player to the main menu after defeat.

- <code>CameraFacingBillboard.cs</code>  
  Makes UI-like world elements face the camera.

- <code>MysticalFloater.cs</code>  
  Adds simple floating and rotation effects to objects.

</div>

---

## <span style="color:gray;">DEVELOPMENT NOTES</span>

<div align="justify">

### World interaction design

<p align="justify">
The interaction system was built around a clear separation between movement and action input. Left click is reserved for navigation and right click is used for actions. This makes the controls easy to understand and helps the player quickly switch between exploration, gathering and combat.
</p>

### Combat direction

<p align="justify">
A notable part of the project is the choice to use a <strong>cure mechanic</strong> instead of a standard damage-only attack. Technically, zombies still behave as hostile enemies, but the player attack reduces an infection value. Once the infection reaches zero, the zombie is replaced by a human survivor prefab. This gives combat a different tone and also demonstrates prefab replacement, projectile logic and enemy state reaction.
</p>

### UI integration

<p align="justify">
A lot of attention was given to the UI because it connects nearly every system in the project. Health, inventory, map, options and game over feedback are all part of the complete gameplay loop. The result is a prototype that does not only showcase mechanics in isolation, but presents them as part of a more complete game structure.
</p>

### Procedural placement

<p align="justify">
The spawn systems were designed to avoid random placement that feels broken or unfair. Resources, enemies and animals are placed only after checking terrain slope, allowed height and nearby NavMesh validity. This helps keep the map readable and playable while still giving enough randomness to make each session feel more natural.
</p>

</div>

---

## <span style="color:gray;">TECHNOLOGIES USED</span>

<div align="justify">

| <strong>Technology</strong> | <strong>Role in the project</strong> |
|---|---|
| <strong>Unity</strong> | Main game engine used to build the entire project |
| <strong>C#</strong> | Core scripting language for gameplay systems |
| <strong>Unity NavMesh</strong> | Used for player and enemy navigation across the terrain |
| <strong>Animator + State Machine Behaviours</strong> | Used for player animation flow and zombie AI states |
| <strong>Unity UI</strong> | Used for inventory, health bar, menus, minimap and map screens |
| <strong>TextMeshPro</strong> | Used for crisp and flexible UI text rendering |
| <strong>PlayerPrefs</strong> | Used to save and load user settings such as volume and gameplay preferences |
| <strong>AudioSource / AudioClip</strong> | Used for footsteps, attacks, hits, pickups and cure effects |
| <strong>Physics Raycasting</strong> | Used for click detection, interaction checks and fog of war reveal |
| <strong>Layer Masks</strong> | Used to separate ground, enemies, interactables and fog systems |

</div>

---

## <span style="color:gray;">SCREENSHOTS</span>

### Main Menu

<p align="center">
  <img src="menu.PNG" alt="Tenebrium main menu" width="700">
</p>

<p align="justify">
The main menu provides access to the game and options panel. It is also connected to the saved settings system so the player can keep audio and gameplay preferences between sessions.
</p>

---

### Combat Encounter

<p align="center">
  <img src="battle.PNG" alt="Tenebrium combat" width="700">
</p>

<p align="justify">
This screenshot shows the player in an active encounter. The scene highlights the isometric perspective, the dark atmosphere, the health HUD and the enemy presence on the map.
</p>

---

### Inventory System

<p align="center">
  <img src="inventory.PNG" alt="Tenebrium inventory" width="700">
</p>

<p align="justify">
The inventory screen supports item storage, stack management and drag and drop organization. Consumable items can also be used directly from the inventory when the player needs healing.
</p>

---

### Map Interface

<p align="center">
  <img src="map.PNG" alt="Tenebrium map interface" width="700">
</p>

<p align="justify">
The map system helps the player understand the environment and move more safely through dangerous areas. It works together with the minimap and fog of war systems to improve exploration.
</p>

---

### Zombie Scene

<p align="center">
  <img src="zombie.PNG" alt="Tenebrium zombie encounter" width="700">
</p>

<p align="justify">
This view shows the hostile presence that drives the survival side of the game. Zombies detect, chase and attack the player, forcing constant movement and resource awareness.
</p>

---
