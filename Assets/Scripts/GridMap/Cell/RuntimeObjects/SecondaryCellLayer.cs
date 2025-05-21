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

    public GameObject GenerateCell(GameObject optionParent){
        if(this.layerSet == null){
            return new GameObject("LayerError");
        }
        return this.layerSet.GenerateOption(optionParent, this.tl_filled, this.tr_filled, this.bl_filled, this.br_filled);
    }
}