using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CellOptionBase", menuName = "ScriptableObjects/TileMapping/CellOptionBase", order = 1)]
public class CellOptionBase : ScriptableObject
{
    public GameObject shape;
    
    public GameObject Generate(GameObject optionParent){
        GameObject cellObject = (GameObject)Instantiate(
            this.shape,
            optionParent.transform
        );
        // stash it and dont use so it waits for the adding
        // RectTransform rt = renderer.AddComponent(typeof(RectTransform)) as RectTransform;
        return cellObject;
    }
}