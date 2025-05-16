using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GridMask", menuName = "ScriptableObjects/TileMapping/GridMask", order = 1)]
public class GridMask : ScriptableObject
{
    // ================================================================
    // ================================================================
    // -------------------------------------------- public data fields

    // where the layer is loaded from
    public Texture2D maskImage;
    // what we cell set we want to use with it
    public CellOptionSet optionSet;

    public GridLayerPriority priority;

    // ================================================================
    // ================================================================
    // ----------------------------------------- public getter methods

    public Vector2Int GetDimensions(){
        return new Vector2Int( this.maskImage.width, this.maskImage.height );
    }
    public Color[] GetPixels(){
        return this.maskImage.GetPixels(0);
    }

    // ================================================================
    // ================================================================
}
