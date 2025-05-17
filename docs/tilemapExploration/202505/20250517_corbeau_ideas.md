# 20250517 ideas
## about
* ideas for the day relating to tilemapping
* retro-active ideas document as we didnt write it on the day

## day's ideas

### gpu tilemap reconciliation
* have the layer information encoded in images with transparency
* background is the "fill colour" for unallocated blocks
* images stacked like cards in a 3D space, drawing back to front
* using orthographic camera to render to texture
* read texture back and use texels
    * using colour value as a hash key to find what type of tile at a location
* additional ideas during retroactive writeup
    * can use difference of gaussians/convolutions to edge detect
    * can depth buffer as well
    * rgb for height information
        * `r` floor height?
        * `g` player height?
        * `b` ceiling height?
    * can use for minimap

### dual grid brainrot
* couldnt stop thinking about the dual grid system and what that would look like/involve

#### grid architecture design
* showing the tile map varieties and how a [dual grid system](https://youtu.be/jEWFSv3ivTg) would look
* currently seems we've reinvented the 47-piece system's wheel

|  |
| --- |
| [![image](/docs/notes/20250517_TilemapVarieties.png)](/docs/notes/20250517_TilemapVarieties.png) |
| [![image](/docs/notes/20250517_TilemapVarietiesAnnotated.png)](/docs/notes/20250517_TilemapVarietiesAnnotated.png) |

#### dual grid tile atlas and indexing

##### alternative ordering
* an experiment which helped to highlight how unfriendly it is to work with from an artist's perspective
* it might be useful instead to translate tilemaps (before use) into this style which would reduce processing overhead at the cost of memory overhead from more textures 

| binary ordering | grey code ordering |
| :---: | :---: |
| [![image](/docs/notes/20250517_15PieceBinary.png)](/docs/notes/20250517_15PieceBinary.png) | [![image](/docs/notes/20250517_15PieceGreyCode.png)](/docs/notes/20250517_15PieceGreyCode.png) |

##### tile quadrant usage

| tiles with topleft to bottomright row major ordering  | shown with values corresponding to quadrant being filled |
| :---: | :---: |
| [![image](/docs/notes/20250517_15PieceQuadrants.png)](/docs/notes/20250517_15PieceQuadrants.png) | [![image](/docs/notes/20250517_15PieceQuadrantValues.png)](/docs/notes/20250517_15PieceQuadrantValues.png) |

##### tile type karnaugh map

| tile values rearranged to grey code ordering | k-mapped tile indices with minterm labels |
| :---: | :---: |
| [![image](/docs/notes/20250517_15PieceQuadrantValuesGreyCode.png)](/docs/notes/20250517_15PieceQuadrantValuesGreyCode.png) | [![image](/docs/notes/20250517_15PieceQuadrantValuesKarnaughMap.png)](/docs/notes/20250517_15PieceQuadrantValuesKarnaughMap.png) |

| quadrant | sum of products |
| --- | --- |
| top left | `y = ((!A)(!C)(!D)) + (( B)(!C)(!D)) + ((!B)( C)( D)) + ((!A)( B)( D))` |
| top right | `y = ((!B)(!C)(!D)) + (( A)( B)(!D)) + ((!A)(!B)( D)) + (( B)( C)( D))` |
| bottom left | `y = ((!A)( B)(!D)) + ((!B)( C)(!D)) + (  (!A)( C)  ) + (( B)( C)( D))` |
| bottom right | `y = ((!A)(!B)(!D)) + (( A)( B)( D)) + (( A)(!B)(!D)) + (( B)( C)(!D))` |
