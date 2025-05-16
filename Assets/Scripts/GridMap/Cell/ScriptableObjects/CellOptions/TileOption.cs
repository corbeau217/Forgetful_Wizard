using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileOption", menuName = "ScriptableObjects/TileMapping/TileOption", order = 1)]
public class TileOption : CellOptionBase
{
    public GameObject tilePrefab;

    public override GameObject Generate(GameObject parent){
        GameObject cellObject = (GameObject)Instantiate(
            this.tilePrefab,
            parent.transform
        );
        // stash it and dont use so it waits for the adding
        RectTransform rt = cellObject.AddComponent(typeof(RectTransform)) as RectTransform;
        return cellObject;
    }
}