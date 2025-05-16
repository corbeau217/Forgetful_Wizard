using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CellPlacementStyle", menuName = "ScriptableObjects/TileMapping/CellPlacementStyle", order = 1)]
public class CellPlacementStyle : ScriptableObject
{
    // cell region filling
    public Texture2DArray cellFilledMap;
    // nearby required filled cells
    public Texture2DArray cellAdjacencyMap;
    // nearby required vacant cells
    public Texture2DArray cellVacancyMap;
}