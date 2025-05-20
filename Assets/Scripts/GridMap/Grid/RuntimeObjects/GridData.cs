using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class GridData
{
    private CellGenerator[,] primaryCells;
    private CellOptionBase noneCell;
    
    private Vector2Int primaryDimensions;
    private Vector2Int secondaryDimensions;

    public GridData(Vector2Int primaryDimensions, Vector2Int secondaryDimensions, CellOptionBase noneCellOption){
        this.primaryDimensions = primaryDimensions;
        this.secondaryDimensions = secondaryDimensions;
        this.noneCell = noneCellOption;

        this.primaryCells = new CellGenerator[this.primaryDimensions.y,this.primaryDimensions.x];

        for (int rowIndex = 0; rowIndex < this.primaryDimensions.y; rowIndex++)
        {
            for (int colIndex = 0; colIndex < this.primaryDimensions.x; colIndex++)
            {
                this.primaryCells[rowIndex,colIndex] = new CellGenerator();
            }
        }
    }

    public void UpdateWithMask(GridMaskData maskData){
        for (int rowIndex = 0; rowIndex < this.primaryDimensions.y; rowIndex++)
        {
            for (int colIndex = 0; colIndex < this.primaryDimensions.x; colIndex++)
            {
                this.primaryCells[rowIndex,colIndex].GiveOption(maskData.GetPriority(), maskData.GetCellOptionSet());
            }
        }
    }

    public CellOptionBase[,] BakeCells(){
        // prepare tthe structure
        CellOptionBase[,] resultCells = new CellOptionBase[this.secondaryDimensions.y, this.secondaryDimensions.x];
        for (int rowIndex = 0; rowIndex < this.secondaryDimensions.y; rowIndex++)
        {
            for (int colIndex = 0; colIndex < this.secondaryDimensions.x; colIndex++)
            {
                if(!this.LocationIsFilled(rowIndex, colIndex)){
                    resultCells[rowIndex,colIndex] = this.noneCell;
                }
                else {
                    CellGenerator currentCellGenerator = this.primaryCells[rowIndex, colIndex];
                    CellOptionSet currentOptionSet = currentCellGenerator.GetOptionSet();
                    // TODO : update with dual grid
                    // // gather adjacency
                    // bool[] currentAdjacency = this.GetAdjacency(rowIndex,colIndex);
                    // // match it and stash
                    // CellOptionBase foundData = currentOptionSet.GetCellOptionBase(currentAdjacency);
                    // resultCells[rowIndex,colIndex] = foundData;
                }
            }
        }
        return resultCells;
    }

    public bool LocationIsFilled(int rowIndex, int colIndex){
        if( (rowIndex < 0) || (colIndex < 0) || (rowIndex >= this.primaryDimensions.y) || (colIndex >= this.primaryDimensions.x) ) {
            return false;
        }
        // when it was never updated
        return this.primaryCells[rowIndex, colIndex].GetOptionSet() == null;
    }
}