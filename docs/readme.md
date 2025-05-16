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
### [x] Milestone 004 - multi layer map creation
- [x] map data holds multiple layers
    - [x] first map layer is background (cutaway to create the room shape)
    - [x] later layers are details (overwriting background layer with another tile)
    - [x] map layer holds
        - [x] layer masking
        - [x] tileset to use
    - [x] map data fetches tile to use
    - [x] tile map handles creating objects in world
### [x] Milestone 005 - spritesheet map creation
- [x] using texture to determine tile placement rules
- [x] `TileType` for determining tile similarity
- [x] `TileType` fetches correct data for a tile
    - [x] organise `TileType`s in sprite sheet order
        - [x] gather sprite sheet ordering
        - [x] input data in the [`TileType` file](/Assets/Scripts/World/TileType.cs)
        - [x] assign the correct `TileType`s to existing tiles
- [x] create tiles for missing tile types using closest shape
### [x] Milestone 006 - tile updates
- [x] debug tile sprite overlay
    - [x] shows expected sprite shape above tiles
    - [x] double check tile type sprite sheet
### [x] Milestone 007 - room connecting
- [x] debugging system
    - [x] tile overlay has 3 layers for each of tile mask types
    - [x] hotkey to cycle overlay type
    - [x] hotkey to show/hide
- [x] room mapping changes
    - [x] world now has room based tile maps using fixed room selection
    - [x] rooms allow movement between using room based entry/exit 
- [x] floor contains rooms
    - [x] floor is data object containing room information
### [x] Milestone 008 - room modularity
- [x] inverting data ownership in layers holding tiles
    - [x] room has tiles that have layers
- [x] rooms have their passage type, and their room map
    - [x] primary background layer is the passage way the room has to other rooms
    - [x] secondary background layer is the room shape
- [x] maps have allowed passage types
- [x] rooms generate using passage and map information
    - [x] resulting room cutaway is union of passage background and map background
    - [x] level using hard coded passages
### [x] Milestone 009 - simple magic system
- [x] tinker with spell object structuring
- [x] spell data object abstract class
- [x] projectile spell abstract class
- [x] bolt spell abstract class
- [x] convert sprint spell to new format
- [x] convert blink to new format
- [x] create second bolt option
- [x] create third bolt option
- [x] create fourth bolt option
### [x] Milestone 010 - simple magic selecting
- [x] player spell book data object for all spells
- [x] hide/show spell book menu
    - [x] uses grid for spell options
        - [x] experiment with hexagonal tiles?
            * *it's cursed, need to make our own later*
- [ ] minimalist ui showing equipped spells
### [x] Milestone 011 - simple detail tiles
- [x] adding missing tile objects to project
    - [x] making tile object shapes
- [ ] tiles show option count text above them when more than 1 legal option??
- [x] tiles classified as movement or detail
    - [x] movement tiles are the previous tile types
    - [x] detail tiles are part of movement tiles?? idk
    - [x] nicer tile objects made for detail tiles
        - [x] make the shapes
        - [x] swap out the pillar shape to the new style
        - [x] add in the type handling
        - [x] fill the tileset with the new objects
    - [x] code supports new objects for detail layers
    - [x] optional extra detail layers
### [x] Milestone 012 - room passage types
- [x] adding in room passage type sets
    - [x] rough sketch the room generation structure for tile selection
    - [x] plan out the same structure as with tiles in rooms
    - [x] swap over to a shared tile base class that works at tile and room generation
### [ ] Milestone 013 - restructuring room generator
- [x] documenting new structure
- [x] scriptable objects
    - [x] `GridMask` - mask and option set
    - [x] `GridGeneratorSettings` - mask layers, error tiles
- [ ] runtime objects
    - [x] `GridMaskData` - interpretation of mask image as 2d bool with size
    - [x] `CellGenerator` - cell option with priority
    - [x] `GridData` - handle for cell generators
    - [ ] `GridGenerator` - makes griddata from settings, then makes grid data fill cell generators
- [ ] components
    - [ ] `CellRenderer` - abstract class handling instantiation of cells
    - [ ] `GridRenderer` - spawns `GridGenerator`, reads created information and then spawns `CellRenderer`s
### [ ] Milestone 014 - level generation
- [ ] generating level based on adjacency of rooms
    - [ ] room adjacency / vacancy rules
### [ ] Milestone 015 - initial interactables
- [ ] interactable tile interface
    - [ ] tile that can be activated in some way
- [ ] actor / living entity / being interface
    - [ ] dummy that just exists and does nothing
### [ ] Milestone 016 - more interactables
- [ ] interactable tile
    - [ ] interaction states
    - [ ] strategy pattern for state changes
- [ ] pickup item
    - [ ] item that can be picked up by the player
    - [ ] deleted when picked up
    - [ ] fires event
- [ ] actor / living entity / being interface
    - [ ] movement strategy pattern
### [ ] Milestone 017 - simple resources
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

## performance improvements
### code execution
- [ ] map layer should buffer the tile data for overlays
- [ ] having shared map floor shape / combining meshes into one game object


## ideas

### random ideas
* probably could hashmap our tiles by filled/adjacency/vacancy to help find similar tiles
* composition for tile making
* glowy mushrooms (other colours), `omphalotus nidiformis` ahh