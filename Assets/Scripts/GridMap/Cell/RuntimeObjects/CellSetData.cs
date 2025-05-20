using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSetData
{   

    public CellOptionSet optionSet;

    public CellData[] cellDataList;
    public CellData defaultOption;

    public CellSetData(CellOptionSet optionSet){
        this.optionSet = optionSet;
        this.cellDataList = new CellData[this.optionSet.options.Length];
        this.defaultOption = new CellData(this.optionSet.defaultOption);
        this.GenerateCellSetData();
    }


    public void GenerateCellSetData(){
        // all cells
        for (int index = 0; index < this.optionSet.options.Length; index++) {
            // the current working cell
            CellOptionBase currCellOption = this.optionSet.options[index];
            // put it in our list
            this.cellDataList[index] = new CellData(currCellOption);
        }

    }

    public int GetIndexByMask(bool sameTopLeft, bool sameTopRight, bool sameBottomLeft, bool sameBottomRight){
        // TODO: convert primary grid filling to secondary grid tile index
        return 0;
    }
}
