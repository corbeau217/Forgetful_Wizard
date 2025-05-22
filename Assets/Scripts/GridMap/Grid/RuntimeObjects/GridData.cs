using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class GridData
{
    private bool[,] primaryCellVacancies;
    private SecondaryCellGenerator[,] secondaryCells;
    
    private CellOptionBase noneCell;
    
    private Vector2Int primaryDimensions;
    private Vector2Int secondaryDimensions;

    public GridData(Vector2Int primaryDimensions, Vector2Int secondaryDimensions, CellOptionBase noneCellOption){
        this.primaryDimensions = primaryDimensions;
        this.secondaryDimensions = secondaryDimensions;
        this.noneCell = noneCellOption;
        
        this.primaryCellVacancies = new bool[this.primaryDimensions.y, this.primaryDimensions.x];
        
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
        if(maskData == null) {
            Debug.Log("null mask data update requested?");
            return;
        }
        for (int primaryRowIndex = 0; primaryRowIndex < this.primaryDimensions.y; primaryRowIndex++) {
            for (int primaryColIndex = 0; primaryColIndex < this.primaryDimensions.x; primaryColIndex++) {
                // at that location do an update
                this.HandleMaskLocation(maskData, primaryRowIndex, primaryColIndex);
            }
        }
    }
    // updates our data from a mask for a specific location
    private void HandleMaskLocation(GridMaskData maskData, int primaryRowIndex, int primaryColIndex){
        // do we need to update our vacancies?
        if(maskData.GetLocationIsFilled(primaryRowIndex, primaryColIndex)){
            // and it's subtractive
            if(maskData.IsSubtractiveLayer()){
                this.primaryCellVacancies[primaryRowIndex,primaryColIndex] = true;
            }
            else {
                this.primaryCellVacancies[primaryRowIndex,primaryColIndex] = false;
            }
        }
        // vacancy for the location stayed the same if the mask didnt use it
        
        // check we're still able to update the secondary grid?
        if(primaryRowIndex < this.secondaryDimensions.y && primaryColIndex < this.secondaryDimensions.x){
            // prepare data for the secondary cell generator
            SecondaryCellLayer currentCellLayer = maskData.GetSecondaryCellLayerFor(primaryRowIndex, primaryColIndex);
            // get it to handle itself
            this.secondaryCells[primaryRowIndex, primaryColIndex].GiveLayer(currentCellLayer);
        }
    }


    public SecondaryCellGenerator[,] GetSecondaryCellGenerators(){
        return this.secondaryCells;
    }
    // directly give it access
    public bool[,] GetEmptyPrimaryCells(){
        return this.primaryCellVacancies;
    }
}