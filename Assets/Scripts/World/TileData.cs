using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TileData", order = 1)]
public class TileData : ScriptableObject
{   
    public GameObject TilePrefab;
    // top row to bottom row, left to right each row
    public bool[] vacantTiles = new bool[]{
        false, false, false,
        false, true,  false,
        false, false, false
    };


}
