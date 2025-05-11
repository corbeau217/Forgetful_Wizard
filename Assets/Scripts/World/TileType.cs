using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ##########################################################################################
// ######################################################################### ENUM DEFINITION
// ##########################################################################################

public enum TileType {
    // ----------------------------------
    // -------------- row 0
    Tile_C1_FR,
    Tile_C1_BR,
    Tile_C1_BL,
    Tile_C1_FL,
    // ---
    Tile_W1_F,
    Tile_W1_R,
    Tile_W1_B,
    Tile_W1_L,
    // ----------------------------------
    // -------------- row 1
    Tile_C2_AF,
    Tile_C2_AR,
    Tile_C2_AB,
    Tile_C2_AL,
    // ---
    Tile_W1_F_C1_R,
    Tile_W1_R_C1_R,
    Tile_W1_B_C1_R,
    Tile_W1_L_C1_R,
    // ----------------------------------
    // -------------- row 2
    Tile_C3_BR,
    Tile_C3_BL,
    Tile_C3_FL,
    Tile_C3_FR,
    // --- 
    Tile_W1_F_C1_L,
    Tile_W1_R_C1_L,
    Tile_W1_B_C1_L,
    Tile_W1_L_C1_L,
    // ----------------------------------
    // -------------- row 3
    Tile_Empty,
    Tile_C2_DL,
    Tile_C2_DR,
    Tile_C4,
    // --- 
    Tile_W1_F_C2,
    Tile_W1_R_C2,
    Tile_W1_B_C2,
    Tile_W1_L_C2,
    // ----------------------------------
    // -------------- row 4
    Tile_Pillar,
    Placeholder_R4C1, // filler space
    Placeholder_R4C2, // filler space
    Placeholder_R4C3, // filler space
    // --- 
    Tile_W2_FR,
    Tile_W2_BR,
    Tile_W2_BL,
    Tile_W2_FL,
    // ----------------------------------
    // -------------- row 5
    Placeholder_R5C0, // filler space
    Placeholder_R5C1, // filler space
    Placeholder_R5C2, // filler space
    Placeholder_R5C3, // filler space
    // --- 
    Tile_W2_FR_C1,
    Tile_W2_BR_C1,
    Tile_W2_BL_C1,
    Tile_W2_FL_C1,
    // ----------------------------------
    // -------------- row 6
    Placeholder_R6C0, // filler space
    Placeholder_R6C1, // filler space
    Placeholder_R6C2, // filler space
    Placeholder_R6C3, // filler space
    // --- 
    Tile_W3_NB,
    Tile_W3_NL,
    Tile_W3_NF,
    Tile_W3_NR,
    // ----------------------------------
    // -------------- row 7
    Placeholder_R7C0, // filler space
    Placeholder_R7C1, // filler space
    Placeholder_R7C2, // filler space
    Placeholder_R7C3, // filler space
    // --- 
    Tile_W2_PF,
    Tile_W2_PL,
    Tile_W4,
    Tile_Block,
    // ----------------------------------
}

// ##########################################################################################
// ################################################################### ENUM MEMBER FUNCTIONS
// ##########################################################################################

// helpers for tile types
static class TileTypeUtils {

    public const int TILE_ROWS = 8;
    public const int TILE_COLS = 8;
    public const int TILE_COUNT = 64;

    // supplying the index of the type
    public static int GetIndex(this TileType input){
        return (int)input;
        // int columnCount = 8;
        // int rowCount = 8;

        // int topleftIndex = (int)input;

        // // extract column index, stays same
        // int topleftColIndex = topleftIndex % columnCount;
        
        // // extract topleft row index
        // int topleftRowIndexTopToBottom = topleftIndex / columnCount;

        // // convert to pixel row index
        // int lasttopleftRow = (rowCount-1);
        // int bottomleftRowIndex = lasttopleftRow - topleftRowIndexTopToBottom;

        // // combine for the bottomleft index
        // return (bottomleftRowIndex*columnCount)+topleftColIndex;
    }

    // get the pixel information by type
    public static Color[] GetPixelsFromSpritesheet(this TileType input, Texture2DArray arrayToSearch){
        int index = input.GetIndex();
        // Debug.Log("the typed index is "+index);
        return arrayToSearch.GetPixels(index, 0);
    }
}

// ##########################################################################################
// ##########################################################################################
// ##########################################################################################