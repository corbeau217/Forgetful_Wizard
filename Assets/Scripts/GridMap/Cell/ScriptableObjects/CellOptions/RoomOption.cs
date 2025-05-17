using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomOption", menuName = "ScriptableObjects/TileMapping/RoomOption", order = 1)]
public class RoomOption : CellOptionBase
{
    public RoomLayerMaskData roomMask;

    public override GameObject Generate(GameObject renderer, GameObject parent){
        // honk shoo mimimimi
        return null;
    }
}