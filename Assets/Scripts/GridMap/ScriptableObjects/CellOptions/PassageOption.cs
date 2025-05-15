using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PassageOption", menuName = "ScriptableObjects/TileMapping/PassageOption", order = 1)]
public class PassageOption : CellOptionBase
{
    public RoomLayerMaskData roomMask;
}