using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridGenerator
{
    public GridGeneratorSettings settings;
    public GridMaskData[] masks;
    public GridData gridData;
    public CellData[,] finalCells;
    
    public Vector2Int dimensions;

    public GridGenerator(GridGeneratorSettings settings){
        this.settings = settings;

        this.InitialiseGridMaskData();
        this.InitialiseGridDataFromMasks();
    }

    private void InitialiseGridMaskData(){
        // prepare array
        this.masks = new GridMaskData[this.settings.layerMasks.Length];
        // then translate each
        for (int maskIndex = 0; maskIndex < this.settings.layerMasks.Length; maskIndex++)
        {
            this.masks[maskIndex] = new GridMaskData(this.settings.layerMasks[maskIndex]);
        }
        this.dimensions = this.masks[0].GetDimensions();
    }
    private void InitialiseGridDataFromMasks(){
        // prepare data structure
        this.gridData = new GridData(this.dimensions, this.settings.noneCell);

        // load each mask
        for (int maskIndex = 0; maskIndex < this.masks.Length; maskIndex++)
        {
            this.gridData.UpdateWithMask(this.masks[maskIndex]);
        }
    }

    public CellData[,] BakeCells(){
        this.finalCells = this.gridData.BakeCells();
        return finalCells;
    }

}