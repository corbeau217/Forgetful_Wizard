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
        if(maskData == null){
            Debug.Log("null mask data update requested?");
        }
        for (int rowIndex = 0; rowIndex < this.primaryDimensions.y; rowIndex++)
        {
            for (int colIndex = 0; colIndex < this.primaryDimensions.x; colIndex++)
            {
                if(maskData.GetLocationIsFilled(rowIndex, colIndex)) {
                    this.primaryCells[rowIndex,colIndex].GiveOption(maskData.GetPriority(), maskData.GetCellOptionSet());
                }
            }
        }
    }

    public CellOptionBase[,] BakeCells(){
        // prepare tthe structure
        CellOptionBase[,] resultCells = new CellOptionBase[this.secondaryDimensions.y, this.secondaryDimensions.x];
        for (int secondaryRowIndex = 0; secondaryRowIndex < this.secondaryDimensions.y; secondaryRowIndex++)
        {
            for (int secondaryColIndex = 0; secondaryColIndex < this.secondaryDimensions.x; secondaryColIndex++)
            {

                CellGenerator topLeftGenerator = this.primaryCells[secondaryRowIndex, secondaryColIndex];
                CellOptionSet topLeftSet = topLeftGenerator.GetOptionSet();
                CellGenerator topRightGenerator = this.primaryCells[secondaryRowIndex, secondaryColIndex+1];
                CellOptionSet topRightSet = topRightGenerator.GetOptionSet();
                CellGenerator bottomLeftGenerator = this.primaryCells[secondaryRowIndex+1, secondaryColIndex];
                CellOptionSet bottomLeftSet = bottomLeftGenerator.GetOptionSet();
                CellGenerator bottomRightGenerator = this.primaryCells[secondaryRowIndex+1, secondaryColIndex+1];
                CellOptionSet bottomRightSet = bottomRightGenerator.GetOptionSet();

                // no sets for our primary cells?
                if(
                    (topLeftSet==null) &&
                    (topRightSet==null) &&
                    (bottomLeftSet==null) &&
                    (bottomRightSet==null)
                ){
                    // all null
                    // TODO: probably have an error cell for this?
                    resultCells[secondaryRowIndex, secondaryColIndex] = this.noneCell;
                }
                // find which one
                else {
                    // prepare
                    CellGenerator comparisonGenerator = topLeftGenerator;
                    CellOptionSet usingSet = topLeftSet;
                    // didnt have null on top left
                    if(topLeftSet != null){
                        comparisonGenerator = topLeftGenerator;
                        usingSet = topLeftSet;
                    }
                    // top left null try top right
                    if(topRightSet != null){
                        comparisonGenerator = topRightGenerator;
                        usingSet = topRightSet;
                    }
                    // top right null try bottom left
                    if(bottomLeftSet != null){
                        comparisonGenerator = bottomLeftGenerator;
                        usingSet = bottomLeftSet;
                    }
                    // was bottom right
                    else {
                        comparisonGenerator = bottomRightGenerator;
                        usingSet = bottomRightSet;
                    }
                    // compare the quadrants
                    bool tl_filled = comparisonGenerator.MatchesFillWith(topLeftGenerator);
                    bool tr_filled = comparisonGenerator.MatchesFillWith(topRightGenerator);
                    bool bl_filled = comparisonGenerator.MatchesFillWith(bottomLeftGenerator);
                    bool br_filled = comparisonGenerator.MatchesFillWith(bottomRightGenerator);

                    if(usingSet == null){
                        Debug.Log("what? HOW");
                        resultCells[secondaryRowIndex, secondaryColIndex] = this.noneCell;
                    }
                    else {
                        int optionIndex = usingSet.GetIndexByMask(tl_filled, tr_filled, bl_filled, br_filled);
                        resultCells[secondaryRowIndex, secondaryColIndex] = usingSet.options[ optionIndex ];
                    }
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