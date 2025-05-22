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
### [x] Milestone 013 - restructuring room generator
- [x] documenting new structure
- [x] scriptable objects
    - [x] `GridMask` - mask and option set
    - [x] `GridGeneratorSettings` - mask layers, error tiles
- [x] runtime objects
    - [x] `GridMaskData` - interpretation of mask image as 2d bool with size
    - [x] `CellGenerator` - cell option with priority
    - [x] `GridData` - handle for cell generators
    - [x] `GridGenerator` - makes griddata from settings, then makes grid data fill cell generators
- [x] components
    - [x] `CellRenderer` - abstract class handling instantiation of cells
    - [x] `GridRenderer` - spawns `GridGenerator`, reads created information and then spawns `CellRenderer`s
### [x] Milestone 014 - room dual grid
- [x] `CellPlacementRules` removed
- [x] `CellPlacementStyle` removed
- [x] `CellType` removed
    - [x] gutted old style script files
- [x] `TileOption` removed and squashed in to base
- [x] clearing out mentions of the removed files
- [x] `CellSetData` removed
- [x] code structure reconciled with fourth revision
    * this was to correct some overlooked items and using better naming of variables/methods
- [x] `CellOptionSet` handles secondary grid indexing by mask
- [x] `CellData` removed
- [x] `GridData` uses the `CellOptionSet` form of cell retrieval during baking
    - [x] has secondary grid as smaller size to the mask
    - [x] asks for cell that would fit secondary grid
    - [x] saves cell in `finalCells`
- [x] `GridGenerator` handles secondary grid generation
    - [x] has secondary grid as smaller size to the mask
    - [x] verified working
        * lots of errors though ahahaha
- [x] update tile models to not have offsets
- [x] trim unseen faces for backup set
- [x] correctly orient tile directions
### [x] Milestone 016 - room dual grid fixes
- [x] `SecondaryCellLayer` created
    - [x] has primary cell quadrant fills
    - [x] has `CellOptionSet` to use
    - [x] makes the `CellOptionBase` game object
- [x] `SecondaryCellGenerator` created
    - [x] provided with layers to make
    - [x] has some sort of latch/lock to prevent further updates?
        * highest contained layer priority
    - [x] combines all layers in to one tile to render
- [x] `CellGenerator` removed
- [x] `SecondaryCellGenerator` updated
    - [x] given no layers, would generate fill using defaultOption
- [x] `GridMaskData` creates `SecondaryCellLayer` by top left primary cell location
- [x] `GridData` uses `SecondaryCellGenerator` and `SecondaryCellLayer`
- [x] experiment with adding in layers
- [x] really hacky fix for layer reconciling until later
- [x] minimalist placeholders for shelves/passage/pillar tiles
### [ ] Milestone 017 - initial interactables
- [x] simple chest shape
- [x] planning out the way that interactable objects should happen
- [ ] spawning interactable objects
    * tertiary grid used to spawn interactable objects, using same alignment as primary grid
    - [ ] `GridGenerator` provides information on primary cell occupancy to `GridRenderer`
    - [ ] `GridRenderer` selects cells to be legal for interactions to spawn
    - [ ] `InteractableBehaviour` component for interactable objects
        - [ ] shape for renderering
        - [ ] tooltip behaviour
            - [ ] shows tooltip if in range of
            - [ ] no tool tip
        - [ ] interaction style behaviour
            - [ ] mouse triggered (small box collider)
            - [ ] player nearby triggered (big box colllider around)
            - [ ] player touching (bounding box collider)
        - [ ] trigger behaviour
            - [ ] in-range binding press
            - [ ] on touch
        - [ ] re-activation behaviour
            - [ ] no timeout
            - [ ] timeout between reactivating
        - [ ] usage behaviour
            - [ ] hide object
            - [ ] change tooltip
            - [ ] attempt to damage player
            - [ ] attempt to trade player
    - [ ] `InteractableObjectConfig` scriptable object with config for interactables
        - [ ] settings for each of the behaviours
    - [ ] `InteractableBuilder` class for constructing interactable objects
        - [ ] builds the runtime interactable object from `InteractableObjectConfig`
    - [ ] `InteractableSpawner` component made
        * added to an object that has `GridRenderer`
        - [ ] creates child empty game object as a collection for interaction spawns
        - [ ] fetches interaction spawn locations from `GridRenderer`
        - [ ] rolls for spawning interactables at the locations
        - [ ] creates the objects using `InteractableBuilder`
- [ ] actor / living entity / being interface
    - [ ] dummy that just exists and does nothing
### [ ] Milestone 018 - tilemap cleanups
- [ ] cleaning up tilemapping so it behaves nicer
### [ ] Milestone 019 - room structuring
- [ ] introspection and investigation on how to mesh rooms together again
    * likely uses something similar to the old style of cell generation
### [ ] Milestone 020 - more interactables
- [ ] interactable tile
    - [ ] interaction states
    - [ ] strategy pattern for state changes
- [ ] pickup item
    - [ ] item that can be picked up by the player
    - [ ] deleted when picked up
    - [ ] fires event
- [ ] actor / living entity / being interface
    - [ ] movement strategy pattern
### [ ] Milestone 021 - simple resources
- [ ] design player resource architecture
    - [ ] player health
    - [ ] player magic resource
### [ ] Milestone 022 - development documentation cleanup
- [ ] housekeeping for the development documentation structure
    - [x] renaming and restructuring notes
    - [ ] renaming and restructuring diagrams
    - [ ] cleaning up `readme.md` and removing bloat
- [ ] moving development documentation to separate repository
- [ ] adding the documentation repository as a sub repository/link in this repository



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
* using the dual grid system with meshes, by chopping up the meshes and constructing new ones at runtime
    * this could use [https://docs.unity3d.com/ScriptReference/Mesh.html](https://docs.unity3d.com/ScriptReference/Mesh.html)
    * 16 shapes for a tileset?
* barrel with rotating portions to create the spell?
    * bike lock with the numbers and rotate them out
    * strategy pattern for spells