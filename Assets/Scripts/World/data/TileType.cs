using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ##########################################################################################
// ######################################################################### ENUM DEFINITION
// ##########################################################################################

public enum TileType {
    // ====================================================================
    // ====================================================================
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
    // ====================================================================
    // ====================================================================
    // ----------------------------------
    // -------------- detail row 0
    Detail_W3_NF,
    Detail_W3_NR,
    Detail_W3_NB,
    Detail_W3_NL,
    // ---
    Detail_W2_BL,
    Detail_W2_FL,
    Detail_W2_FR,
    Detail_W2_BR,
    // ----------------------------------
    // -------------- detail row 1
    Detail_W1_B,
    Detail_W1_L,
    Detail_W1_F,
    Detail_W1_R,
    // ---
    Detail_W2_PF,
    Detail_W2_PL,
    Detail_W0,
    Detail_W4,
    // ----------------------------------
    // -------------- detail row 2
    Detail_Pillar,
    Placeholder_Detail_R2C1, // filler space
    Placeholder_Detail_R2C2, // filler space
    Placeholder_Detail_R2C3, // filler space
    // --- 
    Placeholder_Detail_R2C4, // filler space
    Placeholder_Detail_R2C5, // filler space
    Placeholder_Detail_R2C6, // filler space
    Placeholder_Detail_R2C7, // filler space
    // ----------------------------------
    // -------------- detail row 3
    Placeholder_Detail_R3C0, // filler space
    Placeholder_Detail_R3C1, // filler space
    Placeholder_Detail_R3C2, // filler space
    Placeholder_Detail_R3C3, // filler space
    // --- 
    Placeholder_Detail_R3C4, // filler space
    Placeholder_Detail_R3C5, // filler space
    Placeholder_Detail_R3C6, // filler space
    Placeholder_Detail_R3C7, // filler space
    // ----------------------------------
    // -------------- detail row 4
    Placeholder_Detail_R4C0, // filler space
    Placeholder_Detail_R4C1, // filler space
    Placeholder_Detail_R4C2, // filler space
    Placeholder_Detail_R4C3, // filler space
    // --- 
    Placeholder_Detail_R4C4, // filler space
    Placeholder_Detail_R4C5, // filler space
    Placeholder_Detail_R4C6, // filler space
    Placeholder_Detail_R4C7, // filler space
    // ----------------------------------
    // -------------- detail row 5
    Placeholder_Detail_R5C0, // filler space
    Placeholder_Detail_R5C1, // filler space
    Placeholder_Detail_R5C2, // filler space
    Placeholder_Detail_R5C3, // filler space
    // --- 
    Placeholder_Detail_R5C4, // filler space
    Placeholder_Detail_R5C5, // filler space
    Placeholder_Detail_R5C6, // filler space
    Placeholder_Detail_R5C7, // filler space
    // ----------------------------------
    // -------------- detail row 6
    Placeholder_Detail_R6C0, // filler space
    Placeholder_Detail_R6C1, // filler space
    Placeholder_Detail_R6C2, // filler space
    Placeholder_Detail_R6C3, // filler space
    // --- 
    Placeholder_Detail_R6C4, // filler space
    Placeholder_Detail_R6C5, // filler space
    Placeholder_Detail_R6C6, // filler space
    Placeholder_Detail_R6C7, // filler space
    // ----------------------------------
    // -------------- detail row 7
    Placeholder_Detail_R7C0, // filler space
    Placeholder_Detail_R7C1, // filler space
    Placeholder_Detail_R7C2, // filler space
    Placeholder_Detail_R7C3, // filler space
    // --- 
    Placeholder_Detail_R7C4, // filler space
    Placeholder_Detail_R7C5, // filler space
    Placeholder_Detail_R7C6, // filler space
    Placeholder_Detail_R7C7, // filler space
    // ----------------------------------
    // ====================================================================
    // ====================================================================
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
        // mod 64 to handle if it's part of the standard set or secondary detail set
        return (int)input%64;
    }
    public static bool IsDetailTile(this TileType input){
        return (int)input >= 64;
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