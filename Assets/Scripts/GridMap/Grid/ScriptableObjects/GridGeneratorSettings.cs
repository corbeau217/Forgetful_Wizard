using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GridGeneratorSettings", menuName = "ScriptableObjects/TileMapping/GridGeneratorSettings", order = 1)]
public class GridGeneratorSettings : ScriptableObject
{
    // ================================================================
    // ================================================================
    // -------------------------------------------- public data fields

    public GridMask[] layerMasks;
    public CellOptionBase errorTile;
    public CellOptionBase noneCell;

    // ================================================================
    // ================================================================
    // ----------------------------------------- public getter methods

    public Vector2Int GetDimensions(){
        return this.layerMasks[0].GetDimensions();
    }

    // ================================================================
    // ================================================================
}
