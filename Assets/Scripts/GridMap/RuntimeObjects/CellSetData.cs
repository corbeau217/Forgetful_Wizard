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
        this.defaultOption = new CellData(this.optionSet.defaultOption, new CellPlacementRules(new Color[9],new Color[9],new Color[9]));
        this.GenerateCellSetData();
    }


    public void GenerateCellSetData(){
        // all cells
        for (int index = 0; index < this.optionSet.options.Length; index++) {
            // the current working cell
            CellOptionBase currCellOption = this.optionSet.options[index];
            // get the type
            CellType currCellOptionType  = currCellOption.cellType;

            Color[] cellFilledMapPixels = currCellOptionType.GetPixelsFromSpritesheet(this.optionSet.placementStyle.cellFilledMap);
            Color[] cellAdjacencyMapPixels = currCellOptionType.GetPixelsFromSpritesheet(this.optionSet.placementStyle.cellAdjacencyMap);
            Color[] cellVacancyMapPixels = currCellOptionType.GetPixelsFromSpritesheet(this.optionSet.placementStyle.cellVacancyMap);

            CellPlacementRules placementRules = new CellPlacementRules( cellFilledMapPixels, cellAdjacencyMapPixels, cellVacancyMapPixels);

            this.cellDataList[index] = new CellData(currCellOption, placementRules);
        }

    }


    public CellData GetCellData(bool[] adjacentFilled){
        List<int> desirableIndices = new List<int>();
        int resultCellIndex = -1;
        
        // find a matching cell
        for(int i = 0; i < this.cellDataList.Length; i++){
            CellData checkingCell = this.cellDataList[i];
            bool yuckyOption = false;
            // loop across adjacent filled and find when a fill violates our mapping
            for(int k = 0; k < 9; k++){
                // adjacency was filled?
                if( adjacentFilled[k] ){
                    // and it was meant to be vacant?
                    if( checkingCell.cellPlacementRules.vacancyRequired[k] ){
                        yuckyOption = true;
                        break;
                    }
                    // didnt care if filled, brancher
                    else {
                        // ...
                    }
                }
                // not filled
                else {
                    // and wanted it filled
                    if( checkingCell.cellPlacementRules.adjacencyRequired[k] ){
                        yuckyOption = true;
                        break;
                    }
                    // didnt care if vacant, brancher
                    else {
                        // ...
                    }
                }
            }
            // check for not yucky to use
            if(!yuckyOption){
                desirableIndices.Add(i);
            }
        }
        CellData result;
        // stash the first tile if we have an option
        if(desirableIndices.Count >= 1){
            resultCellIndex = desirableIndices[0];
            result = this.cellDataList[resultCellIndex];
        }
        // say if it didnt happen
        else {
            Debug.Log("didnt find tile for: ["+
                ((adjacentFilled[0])?'#':'_')+((adjacentFilled[1])?'#':'_')+((adjacentFilled[2])?'#':'_')+"]["+
                ((adjacentFilled[3])?'#':'_')+((adjacentFilled[4])?'#':'_')+((adjacentFilled[5])?'#':'_')+"]["+
                ((adjacentFilled[6])?'#':'_')+((adjacentFilled[7])?'#':'_')+((adjacentFilled[8])?'#':'_')+"]");
            result = this.defaultOption;
        }

        // give
        return result;
    }
}
