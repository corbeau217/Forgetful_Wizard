using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum TileType {
    Tile_Block,
    Tile_C1_BL,
    Tile_C1_BR,
    Tile_C1_FL,
    Tile_C1_FR,
    Tile_C2_AB,
    Tile_C2_AF,
    Tile_C2_AL,
    Tile_C2_AR,
    Tile_C2_DL,
    Tile_C2_DR,
    Tile_C3_BL,
    Tile_C3_BR,
    Tile_C3_FL,
    Tile_C3_FR,
    Tile_C4,
    Tile_Empty,
    Tile_W1_B,
    Tile_W1_F,
    Tile_W1_L,
    Tile_W1_R,
    Tile_W2_BL,
    Tile_W2_BR,
    Tile_W2_FL,
    Tile_W2_FR,
    Tile_W2_PF,
    Tile_W2_PL,
    Tile_W3_NB,
    Tile_W3_NF,
    Tile_W3_NL,
    Tile_W3_NR,
    Tile_W4
}

static class TileTypeHelpers {

    // supplying the index of the type
    public static int GetIndex(this TileType input){
        return (int)input;
    }

    // get the pixel information by type
    public static Color[] GetPixelsFromSpritesheet(this TileType input, Texture2DArray arrayToSearch){
        int index = input.GetIndex();
        // can we just???
        return arrayToSearch.GetPixels(0, index);
    }
}