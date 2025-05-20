using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CellOptionSet", menuName = "ScriptableObjects/TileMapping/CellOptionSet", order = 1)]
public class CellOptionSet : ScriptableObject
{
    public CellOptionBase[] options;
    public CellOptionBase defaultOption;

    public int GetIndexByMask(bool sameTopLeft, bool sameTopRight, bool sameBottomLeft, bool sameBottomRight){
        // TODO: convert primary grid filling to secondary grid tile index
        return 0;
    }
}