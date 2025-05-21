using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GridGeneratorSettings", menuName = "ScriptableObjects/TileMapping/GridGeneratorSettings", order = 1)]
public class GridGeneratorSettings : ScriptableObject
{
    // ================================================================
    // ================================================================
    // -------------------------------------------- public data fields

    public List<GridMask> layerMasks;
    public CellOptionBase errorTile;
    public CellOptionBase noneCell;

    // ================================================================
    // ================================================================
    // ----------------------------------------- public getter methods

    public Vector2Int GetPrimaryDimensions(){
        return this.layerMasks[0].GetPrimaryDimensions();
    }
    public Vector2Int GetSecondaryDimensions(){
        Vector2Int primaryDimensions = this.layerMasks[0].GetPrimaryDimensions();
        return new Vector2Int(primaryDimensions.x - 1, primaryDimensions.y - 1);
    }
    public int GetLayerCount(){
        return this.layerMasks.Count;
    }

    // ================================================================
    // ================================================================
}
