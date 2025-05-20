using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class GridData
{
    private CellGenerator[,] cells;
    private CellData noneCell;
    
    private Vector2Int dimensions;

    public GridData(Vector2Int dimensions, CellOptionBase noneCellOption){
        this.dimensions = dimensions;
        this.noneCell = new CellData(noneCellOption);

        this.cells = new CellGenerator[this.dimensions.y,this.dimensions.x];

        for (int rowIndex = 0; rowIndex < this.dimensions.y; rowIndex++)
        {
            for (int colIndex = 0; colIndex < this.dimensions.x; colIndex++)
            {
                this.cells[rowIndex,colIndex] = new CellGenerator();
            }
        }
    }

    public void UpdateWithMask(GridMaskData maskData){
        for (int rowIndex = 0; rowIndex < this.dimensions.y; rowIndex++)
        {
            for (int colIndex = 0; colIndex < this.dimensions.x; colIndex++)
            {
                this.cells[rowIndex,colIndex].GiveOption(maskData.GetPriority(), maskData.GetCellOptionSet());
            }
        }
    }

    public CellData[,] BakeCells(){
        // prepare tthe structure
        CellData[,] resultCells = new CellData[this.dimensions.y, this.dimensions.x];
        for (int rowIndex = 0; rowIndex < this.dimensions.y; rowIndex++)
        {
            for (int colIndex = 0; colIndex < this.dimensions.x; colIndex++)
            {
                if(!this.LocationIsFilled(rowIndex, colIndex)){
                    resultCells[rowIndex,colIndex] = this.noneCell;
                }
                else {
                    CellGenerator currentCellGenerator = this.cells[rowIndex, colIndex];
                    CellOptionSet currentOptionSet = currentCellGenerator.GetSetData();
                    // TODO : update with dual grid
                    // // gather adjacency
                    // bool[] currentAdjacency = this.GetAdjacency(rowIndex,colIndex);
                    // // match it and stash
                    // CellData foundData = currentOptionSet.GetCellData(currentAdjacency);
                    // resultCells[rowIndex,colIndex] = foundData;
                }
            }
        }
        return resultCells;
    }

    public bool LocationIsFilled(int rowIndex, int colIndex){
        if( (rowIndex < 0) || (colIndex < 0) || (rowIndex >= this.dimensions.y) || (colIndex >= this.dimensions.x) ) {
            return false;
        }
        // when it was never updated
        return this.cells[rowIndex, colIndex].GetSetData() == null;
    }
}