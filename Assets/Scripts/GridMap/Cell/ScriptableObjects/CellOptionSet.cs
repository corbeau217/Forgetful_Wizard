using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CellOptionSet", menuName = "ScriptableObjects/TileMapping/CellOptionSet", order = 1)]
public class CellOptionSet : ScriptableObject
{
    public CellOptionBase[] options;
    public CellOptionBase defaultOption;
}