# 20250520 ideas
## about
* ideas for the day relating to tilemapping
* written on the day

## day's ideas

### layering dual grid tiles
* having floor be separate from the wall tile sets means we can mesh floors together
* would allow nice floor patterns
* could look at render textures for the floor pattern when there's no holes/below level things
* should look at render textures for walls too? 

### conversion to dual grid update
* below is the suggested class diagram to change to

[![image](/docs/notes/20250520_updatedClassDiagram.png)](/docs/notes/20250520_updatedClassDiagram.png)
[![image](/docs/notes/20250520_updatedClassDiagramV2.png)](/docs/notes/20250520_updatedClassDiagramV2.png)

* `CellGenerator` still feels out of place and will likely need work as we move towards completing the change over