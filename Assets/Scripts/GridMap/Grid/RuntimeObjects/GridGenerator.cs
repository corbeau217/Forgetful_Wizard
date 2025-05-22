using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridGenerator
{
    public GridGeneratorSettings settings;
    public GridMaskData[] masks;
    public GridData gridData;
    public SecondaryCellGenerator[,] secondaryCellGenerators;
    
    public Vector2Int primaryDimensions;
    public Vector2Int secondaryDimensions;

    public GridGenerator(GridGeneratorSettings settings){
        this.settings = settings;
        this.primaryDimensions = this.settings.GetPrimaryDimensions();
        this.secondaryDimensions = this.settings.GetSecondaryDimensions();

        this.InitialiseGridMaskData();
        this.InitialiseGridDataFromMasks();
    }

    private void InitialiseGridMaskData(){
        int maskLayerCount = this.settings.GetLayerCount();
        // prepare array
        this.masks = new GridMaskData[maskLayerCount];
        // then translate each
        for (int maskIndex = 0; maskIndex < maskLayerCount; maskIndex++) {
            this.masks[maskIndex] = new GridMaskData(this.settings.layerMasks[maskIndex]);
        }
    }
    private void InitialiseGridDataFromMasks(){
        // prepare data structure
        this.gridData = new GridData(this.primaryDimensions, this.secondaryDimensions, this.settings.noneCell);

        // load each mask
        for (int maskIndex = 0; maskIndex < this.masks.Length; maskIndex++) {
            // let our grid data know about it
            this.gridData.UpdateWithMask(this.masks[maskIndex]);
        }
    }

    public SecondaryCellGenerator[,] GetSecondaryCellGenerators(){
        this.secondaryCellGenerators = this.gridData.GetSecondaryCellGenerators();
        return secondaryCellGenerators;
    }

    public bool[,] GetEmptyPrimaryCells(){
        return this.gridData.GetEmptyPrimaryCells();
    }
}