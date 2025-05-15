using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileOption", menuName = "ScriptableObjects/TileMapping/TileOption", order = 1)]
public class TileOption : CellOptionBase
{
    public GameObject tilePrefab;
}