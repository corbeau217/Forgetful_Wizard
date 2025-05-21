using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class GridData
{
    private SecondaryCellGenerator[,] secondaryCells;
    
    private CellOptionBase noneCell;
    
    private Vector2Int primaryDimensions;
    private Vector2Int secondaryDimensions;

    public GridData(Vector2Int primaryDimensions, Vector2Int secondaryDimensions, CellOptionBase noneCellOption){
        this.primaryDimensions = primaryDimensions;
        this.secondaryDimensions = secondaryDimensions;
        this.noneCell = noneCellOption;
        
        this.secondaryCells = new SecondaryCellGenerator[this.secondaryDimensions.y, this.secondaryDimensions.x];

        for (int secondaryRowIndex = 0; secondaryRowIndex < this.secondaryDimensions.y; secondaryRowIndex++)
        {
            for (int secondaryColIndex = 0; secondaryColIndex < this.secondaryDimensions.x; secondaryColIndex++)
            {
                this.secondaryCells[secondaryRowIndex,secondaryColIndex] = new SecondaryCellGenerator(this.noneCell);
            }
        }
    }

    public void UpdateWithMask(GridMaskData maskData){

        // GetSecondaryCellLayerFor

        if(maskData == null) {
            Debug.Log("null mask data update requested?");
            return;
        }
        // using primary indices but we u
        for (int rowIndex = 0; rowIndex < this.secondaryDimensions.y; rowIndex++) {
            for (int colIndex = 0; colIndex < this.secondaryDimensions.x; colIndex++) {
                SecondaryCellLayer currentCellLayer = maskData.GetSecondaryCellLayerFor(rowIndex, colIndex);
                this.secondaryCells[rowIndex, colIndex].GiveLayer(currentCellLayer);
            }
        }
    }

    public SecondaryCellGenerator[,] GetSecondaryCellGenerators(){
        return this.secondaryCells;
    }
}