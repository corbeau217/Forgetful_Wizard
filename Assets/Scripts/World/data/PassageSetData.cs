using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PassageSet", menuName = "ScriptableObjects/PassageSetData", order = 1)]
public class PassageSetData : ScriptableObject
{   
    public PassageMaskData[] tileDataList;

    public PassageMaskData defaultTile;

    // tile region filling
    public Texture2DArray tileFilledMap;
    // nearby required filled tiles
    public Texture2DArray tileAdjacencyMap;
    // nearby required vacant tiles
    public Texture2DArray tileVacancyMap;

    public bool randomiseTileOnMultipleValidOptions;

    public PassageMaskData GetPassageMaskData(bool[] adjacentFilled){
        List<int> desirableIndices = new List<int>();
        int resultTileIndex = -1;
        
        // find a matching hash
        for(int i = 0; i < this.tileDataList.Length; i++){
            PassageMaskData checkingTile = this.tileDataList[i];
            bool yuckyOption = false;
            // loop across adjacent filled and find when a fill violates our mapping
            for(int k = 0; k < 9; k++){
                // adjacency was filled?
                if( adjacentFilled[k] ){
                    // and it was meant to be vacant?
                    if( checkingTile.vacancyRequired[k] ){
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
                    if( checkingTile.adjacencyRequired[k] ){
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
        PassageMaskData result;
        // stash the first tile if we have an option
        if(desirableIndices.Count >= 1){
            resultTileIndex = desirableIndices[((this.randomiseTileOnMultipleValidOptions)?Random.Range(0,desirableIndices.Count):0)];
            result = this.tileDataList[resultTileIndex];
        }
        // say if it didnt happen
        else {
            Debug.Log("didnt find tile for: ["+
                ((adjacentFilled[0])?'#':'_')+((adjacentFilled[1])?'#':'_')+((adjacentFilled[2])?'#':'_')+"]["+
                ((adjacentFilled[3])?'#':'_')+((adjacentFilled[4])?'#':'_')+((adjacentFilled[5])?'#':'_')+"]["+
                ((adjacentFilled[6])?'#':'_')+((adjacentFilled[7])?'#':'_')+((adjacentFilled[8])?'#':'_')+"]");
            result = this.defaultTile;
        }
        // TODO : announcing multiples in payload?

        // give
        return result;
    }

    private void InitialiseTiles(){
        // all tiles
        for (int index = 0; index < this.tileDataList.Length; index++) {
            // Debug.Log("working on "+this.tileDataList.Length+" tiles");
            // the current working tile
            PassageMaskData currPassage = this.tileDataList[index];
            // get the type
            TileType currPassageType  = currPassage.passageType;

            // Debug.Log("preparing tile["+index+"] with type["+currPassageType.GetIndex()+"]");

            Color[] tileFilledMapPixels = currPassageType.GetPixelsFromSpritesheet(this.tileFilledMap);
            Color[] tileAdjacencyMapPixels = currPassageType.GetPixelsFromSpritesheet(this.tileAdjacencyMap);
            Color[] tileVacancyMapPixels = currPassageType.GetPixelsFromSpritesheet(this.tileVacancyMap);

            // then fetch the information for it and initialise
            currPassage.Initialise(
                tileFilledMapPixels,
                tileAdjacencyMapPixels,
                tileVacancyMapPixels
            );
        }
    }

    

    public void Initialise(){
        this.InitialiseTiles();
    }
}
