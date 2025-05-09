using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile", menuName = "ScriptableObjects/TileData", order = 1)]
public class TileData : ScriptableObject
{   
    public GameObject TilePrefab;
    // top row to bottom row, left to right each row
    public bool[] filledRequired;

}
