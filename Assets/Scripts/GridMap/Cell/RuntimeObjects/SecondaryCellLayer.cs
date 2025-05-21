using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryCellLayer
{
    private CellOptionSet layerSet;
    public GridLayerPriority priority;
    private bool tl_filled; 
    private bool tr_filled; 
    private bool bl_filled; 
    private bool br_filled; 
    
    public SecondaryCellLayer( CellOptionSet layerSet, GridLayerPriority priority, bool tl_filled, bool tr_filled, bool bl_filled, bool br_filled ){
        this.layerSet = layerSet;
        this.priority = priority;
        this.tl_filled = tl_filled;
        this.tr_filled = tr_filled;
        this.bl_filled = bl_filled;
        this.br_filled = br_filled;
    }

    public GameObject GenerateCell(GameObject optionParent, out bool tl_fillOut, out bool tr_fillOut, out bool bl_fillOut, out bool br_fillOut ){
        // declare for return
        GameObject resultObject;

        // update our fills
        tl_fillOut = this.tl_filled;
        tr_fillOut = this.tr_filled;
        bl_fillOut = this.bl_filled;
        br_fillOut = this.br_filled;

        // safety first
        if(this.layerSet == null){
            resultObject = new GameObject("LayerError");
        }
        else{
            resultObject = this.layerSet.GenerateOption(optionParent, this.tl_filled, this.tr_filled, this.bl_filled, this.br_filled);
        }
        

        return resultObject;
    }
}