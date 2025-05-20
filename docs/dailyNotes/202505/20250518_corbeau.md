# 20250518 ideas
## about
* ideas for the day relating to tilemapping
* written on the day

## day's ideas

### 3 or 4 perspectives form the voxel grid
* creating perspective drawings for each of the perspectives
    * have depth information included in the images
    * this is used as a heightmap from that orientation
    * fixed perspectives
        * 3 perspectives create a baseless triangular pyramid pointing in the direction of the camera when centered on the shape
        * 4 perspectives create the base for the pyramid and allow rotation
    * moving perspectives
        * portion of the images dedicated to data storage where perspective matrices are encoded

### mesh generation and cavenous vibes
* having cells be for generating the shape
* using the shape carved out by a dual grid system to then determine changes in the shape
* they're now just points for a '3D' texture of the level at a lower resolution than the game world mesh
* cave walls that extend above the camera height
    * see through the further up you go
    * camera passes through makes the camera a bit hazy to show it's in a rock?
    * opacity of the wall is some sort of function of height?

### foggy cave
* fog could just be an extra terrain-like height map object with a transparent texture applied
* using the depth buffer and normal mapping to determine distance traveled through a preset fog layer
    * normal mapping just uses the tilemap/terrain height map/bumpmapping to say how deep the projection is
    * there's a perlin-noise texture for the fog effect
        * vectors along the walls clamped to the hemisphere that points in to the room
            * normals and cross/dot products to determine this?
    * layered for more depth

### echoey sound
* drippy vibes
* echoey drip?
    * record drip bake the raytracing for echo?
    * light probe/lightmap baked, then access created texture for sound mapping

### square grid meets hexagonal grid
* have the hexagonal for natural formations
* outside or caves
* how to have grids blend at edges?

### poly reconcilliation
* every cell now becomes a node centered in the middle
* may require the use of 47 tile layout to learn first
* shape built by tile adjacency??
* uhhhh it just be the lines then puff them up like in blender with thickness of a face modifier