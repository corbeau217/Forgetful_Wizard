using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ##########################################################################################
// ##########################################################################################
// ##########################################################################################

// ...

// ##########################################################################################
// ##########################################################################################
// ##########################################################################################

// ...

// ##########################################################################################
// ######################################################################### ENUM DEFINITION
// ##########################################################################################



public enum TileType {
    // =========================================
    // ========================== passage tiles
    // =========================================
    // ----------------------------------
    // ---------------- corners 1
    Tile_C1_BL,
    Tile_C1_BR,
    Tile_C1_FL,
    Tile_C1_FR,
    // ---------------- corners 2
    Tile_C2_AB,
    Tile_C2_AF,
    Tile_C2_AL,
    Tile_C2_AR,
    Tile_C2_DL,
    Tile_C2_DR,
    // ---------------- corners 3
    Tile_C3_BL,
    Tile_C3_BR,
    Tile_C3_FL,
    Tile_C3_FR,
    // ----------------------------------
    // ---------------- walls 1
    Tile_W1_B,
    Tile_W1_B_C1_L,
    Tile_W1_B_C1_R,
    Tile_W1_B_C2,
    Tile_W1_F,
    Tile_W1_F_C1_L,
    Tile_W1_F_C1_R,
    Tile_W1_F_C2,
    Tile_W1_L,
    Tile_W1_L_C1_L,
    Tile_W1_L_C1_R,
    Tile_W1_L_C2,
    Tile_W1_R,
    Tile_W1_R_C1_L,
    Tile_W1_R_C1_R,
    Tile_W1_R_C2,
    // ---------------- walls 2
    Tile_W2_BL,
    Tile_W2_BL_C1,
    Tile_W2_BR,
    Tile_W2_BR_C1,
    Tile_W2_FL,
    Tile_W2_FL_C1,
    Tile_W2_FR,
    Tile_W2_FR_C1,
    // ---------------- walls parallel
    Tile_W2_PF,
    Tile_W2_PL,
    // ---------------- walls 3
    Tile_W3_NB,
    Tile_W3_NF,
    Tile_W3_NL,
    Tile_W3_NR,
    // ----------------------------------
    // ---------------- misc
    Tile_Empty,
    Tile_C4,
    Tile_W4,
    Tile_Block,
    // ----------------------------------
    // =========================================
    // =========================== detail tiles
    // =========================================
    // ----------------------------------
    // ---------------- ...
    // ...
    // ----------------------------------
    // =========================================
    // =========================================
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
        int inputAsInt = (int)input;
        int colIndex = inputAsInt % TILE_COLS;
        int prevRowIndex = inputAsInt / TILE_COLS;
        int rowIndex = (TILE_COLS-1) - prevRowIndex;

        // it's row major order but from the bottom not the top
        return (rowIndex*TILE_COLS)+colIndex;
    }

    // get the pixel information by type
    public static Color[] GetPixelsFromSpritesheet(this TileType input, Texture2DArray arrayToSearch){
        int index = input.GetIndex();
        return arrayToSearch.GetPixels(0, index);
    }
}

// ##########################################################################################
// ##########################################################################################
// ##########################################################################################