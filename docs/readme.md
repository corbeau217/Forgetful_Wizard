# Forgetful Wizard project
## About
> experiment project, we'll see where this goes

## Importamt

put this somewhere:
```
The five boxing wizards jump quickly
```


## TO-DO - project milestones
### [x] Milestone 001 - project research
- [x] simple scene
    - [x] light weight debug scene to experiment in
    - [x] basic movement
    - [x] basic bolt
    - [x] dash
    - [x] blink
### [x] Milestone 002 - simple map creation
- [x] tile mapping changes
    - [x] prefabs for walls
        - [x] rotating shared objects
    - [x] hard coded maps
        - [x] single layer maps
            - [x] built using editor time binary values
            - [x] yes/no to fill the tiles
### [x] Milestone 003 - tiled map creation
- [x] tile mapping changes
    - [x] tile specific shapes
        - [x] object per tile type
    - [x] tileset holds data about tile options
        - [x] object to use
        - [x] adjacency/vacancy rules
    - [x] semi hard coded maps using tileset and map layer data
### [ ] Milestone 004 - spritesheet map creation
- [x] using texture to determine tile placement rules
- [x] `TileType` for determining tile similarity
- [ ] `TileType` fetches correct data for a tile
    - [x] organise `TileType`s in sprite sheet order
        - [x] gather sprite sheet ordering
        - [x] input data in the [`TileType` file](/Assets/Scripts/World/TileType.cs)
        - [ ] assign the correct `TileType`s to existing tiles
- [ ] create tiles for missing tile types using closest shape
### [ ] Milestone 005 - multi layer map creation
- [ ] map data holds multiple layers
    - [ ] first map layer is background (cutaway to create the room shape)
    - [ ] later layers are details (overwriting background layer with another tile)
    - [ ] map layer holds
        - [ ] layer masking
        - [ ] tileset to use
    - [ ] map data fetches tile to use
    - [ ] tile map handles creating objects in world
### [ ] Milestone 006 - tile updates
- [ ] debug tile sprite overlay
    - [ ] shows expected sprite shape above tiles
        - [ ] making code able to handle ajacency/vacancy sprites
    - [ ] hotkey to cycle overlay type
- [ ] adding missing tile objects to project
    - [ ] making tile object shapes
- [ ] tiles show option count text above them when more than 1 legal option
### [ ] Milestone 007 - room connecting
- [ ] room mapping changes
    - [ ] world now has room based tile maps using fixed room selection
    - [ ] rooms allow movement between using room based entry/exit 
### [ ] Milestone 008 - room modularity
- [ ] floor contains rooms
    - [ ] floor is data object containing room information
- [ ] rooms have their passage type, and their room map
    - [ ] primary background layer is the passage way the room has to other rooms
    - [ ] secondary background layer is the room shape
- [ ] maps have allowed passage types
- [ ] rooms generate using passage and map information
    - [ ] resulting room cutaway is union of passage background and map background
### [ ] Milestone 009 - simple magic system
- [ ] muddle out spell usage architecture in documentation
    - [ ] spell types
    - [ ] spell event handling 
    - [ ] resource usage?
- [ ] spell data object interface
    - [ ] strategy pattern for spell type handling
### [ ] Milestone 010 - simple magic selecting
- [ ] player spell book data object for all spells
- [ ] hide/show spell book menu
    - [ ] uses grid for spell options
        - [ ] experiment with hexagonal tiles?
- [ ] minimalist ui showing equipped spells
### [ ] Milestone 011 - simple detail tiles
- [ ] tiles classified as movement or detail
    - [ ] movement tiles are the previous tile types
    - [ ] detail tiles are part of movement tiles?? idk
    - [ ] tile objects made for detail tiles
    - [ ] code supports new objects for detail layers
### [ ] Milestone 012 - initial interactables
- [ ] interactable tile interface
    - [ ] tile that can be activated in some way
- [ ] actor / living entity / being interface
    - [ ] dummy that just exists and does nothing
### [ ] Milestone 013 - more interactables
- [ ] interactable tile
    - [ ] interaction states
    - [ ] strategy pattern for state changes
- [ ] pickup item
    - [ ] item that can be picked up by the player
    - [ ] deleted when picked up
    - [ ] fires event
- [ ] actor / living entity / being interface
    - [ ] movement strategy pattern
### [ ] Milestone 014 - simple resources
- [ ] design player resource architecture
    - [ ] player health
    - [ ] player magic resource


## TO-DO - easter eggs
### [ ] easter egg batch 001 - misc
- [ ] put this somewhere: `The five boxing wizards jump quickly`

## TO-DO - spell type inclusion
### [ ] warding
- [ ] map warding
    - [ ] fixed location warding
        - [ ] permanent
            - [ ] baked into tile map
            - [ ] controller made code/editor time
                - [ ] coordinated based 
                - [ ] on path based
        - [ ] toggleable
            - [ ] baked into tile map
            - [ ] controller made code/editor time
                - [ ] coordinated based 
                - [ ] on path based
    - [ ] runtime location warding
        - [ ] hacky keybinds for testing
        - [ ] mouse drawing
- [ ] player warding
    - [ ] straight line
    - [ ] bubble
        - [ ] full bubble
        - [ ] conical slice of bubble
    - [ ] orbiters
- [ ] entity warding
### [ ] movement
- [ ] sprinting
    - [ ] constant
    - [ ] random burst
### [ ] teleporting
- [ ] short range
    - [ ] displacement preserving velocity
        - [ ] movement direction
        - [ ] mouse direction
        - [ ] movement/mouse pair
    - [ ] remake at location removing velocity
        - [ ] mouse location
        - [ ] movement direction
        - [ ] mouse direction
- [ ] mid range
    - [ ] respawn
    - [ ] room selecting
- [ ] long range
    - [ ] magic circle?
### [ ] portals max 1 set
- [ ] simple touch and teleport
- [ ] pass through using masking
    - [ ] center of mass passed portal boundary causes teleport
    - [ ] show object at both portals
### [ ] portals multiple sets
- [ ] decouple portals from being single set
- [ ] determine portal owner for effects
    - [ ] take damage if enemy portal
    - [ ] block if warded
### [ ] bolt
- [ ] bolt action
- [ ] semi automatic
- [ ] automatic slow
- [ ] automatic rapid
### [ ] beam
- [ ] straight
    - [ ] uses same ray as blink
- [ ] pathing to point
    - [ ] uses path to mouse point 
- [ ] drawn
    - [ ] draw then spawns
### [ ] mouse follower
- [ ] straight line to mouse
- [ ] pathing to mouse
### [ ] reflecting bolt
- [ ] one that reflects always
    - [ ] INF bounces with lifetime
    - [ ] X bounces
- [ ] one that relects off specific materials
### [ ] physics object
- [ ] indestructible spawn
- [ ] lifetime spawn
- [ ] spawn health
### [ ] projectile
- [ ] simple
- [ ] exploding
    - [ ] simple
    - [ ] spawns sub projectiles
- [ ] sticky
    - [ ] simple spikey
    - [ ] delayed explosion
        - [ ] simple
        - [ ] spawns sub projectiles
- [ ] bouncy
    - [ ] simple spikey
    - [ ] delayed explosion
        - [ ] simple
        - [ ] spawns sub projectiles
### [ ] clones
- [ ] at location spawn
    - [ ] dummy
    - [ ] shoots target
- [ ] radial following spawn
    - [ ] orbit player at specific angle
    - [ ] displaces player with shared orbit


## references

### normal map generation
* github project to generate normal maps in web page [https://cpetry.github.io/NormalMap-Online/](https://cpetry.github.io/NormalMap-Online/)
* strategy pattern [refactoring guru](https://refactoring.guru/design-patterns/strategy)

## ideas

### random ideas
* probably could hashmap our tiles by filled/adjacency/vacancy to help find similar tiles