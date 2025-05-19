# 20250519 ideas
## about
* ideas for the day relating to tilemapping
* written on the day

## day's ideas

### dual grid mistake
* turns out the tilemap we copied from for our previous calculations had a mistake on the left side, corrected in this version:

[![image](/docs/notes/20250519_correctedNewSet.png)](/docs/notes/20250519_correctedNewSet.png)

### notes about dual grid
* swapping over to dual grid removes a tonne of overhead and allows us to make much cleaner models where we can leave out faces entirely
* entire tileset can just be carved out of a 4x4x2 poly to make out tiles
* some of the conversion from primary grid to secondary grid tile index has been mulled over
* primary grid responsible for the shape of the room, simple fill or not filled
* secondary grid responsible for the rendered tiles, each tile located where the 4 tiles meet
* perhaps there's a way to do something like sprite masking for shapes in unity?
* below is truth table + karnaugh mapping for the tiles index

[![image](/docs/notes/20250519_indexingNewSet.png)](/docs/notes/20250519_indexingNewSet.png)

### blender template file
* blender template using subtraction to create the tileset shape
* intersection to get the tile shapes
* should have the final result be that we bake the shape, then cull the hidden faces
* baking the shape also means that we can add details to the mesh
* current version likely has an issue with positioning, we should change positioning to be temporary so that it can be removed in the exporting

### conversion to dual grid
* drew up a diagram of the current grid format, 1/3 of it will likely be removed

[![image](/docs/notes/20250519_oldSetClassDiagram.png)](/docs/notes/20250519_oldSetClassDiagram.png)

* below is the suggested class diagram to change to

[![image](/docs/notes/20250519_newSetSuggestedClassDiagram.png)](/docs/notes/20250519_newSetSuggestedClassDiagram.png)