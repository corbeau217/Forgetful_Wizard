using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileSet", menuName = "ScriptableObjects/TileSetData", order = 1)]
public class TileSetData : ScriptableObject
{   
    public TileData[] tileDataList;

    // tile region filling
    public Texture2DArray tileFilledMap;
    // nearby required filled tiles
    public Texture2DArray tileAdjacencyMap;
    // nearby required vacant tiles
    public Texture2DArray tileVacancyMap;

    public TileData GetTileData(bool[] adjacentFilled){
        int desirableIndex = -1;
        int desirableCount = 0;
        
        // find a matching hash
        for(int i = 0; i < this.tileDataList.Length; i++){
            TileData checkingTile = this.tileDataList[i];
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
                desirableIndex = i;
                desirableCount++;
            }
        }
        // purely for debugging, but shouldnt show up anymore
        if(desirableCount == 0){
            // give back null to cause an error
            return null;
        }
        if(desirableCount > 1){
            Debug.Log("multiple desirable for placement: "+desirableCount);
        }

        return this.tileDataList[desirableIndex];
    }

    private void InitialiseTiles(){
        // all tiles
        for (int index = 0; index < this.tileDataList.Length; index++) {
            // Debug.Log("working on "+this.tileDataList.Length+" tiles");
            // the current working tile
            TileData currTile = this.tileDataList[index];
            // get the type
            TileType currTileType  = currTile.tileType;

            // Debug.Log("preparing tile["+index+"] with type["+currTileType.GetIndex()+"]");

            Color[] tileFilledMapPixels = currTileType.GetPixelsFromSpritesheet(this.tileFilledMap);
            Color[] tileAdjacencyMapPixels = currTileType.GetPixelsFromSpritesheet(this.tileAdjacencyMap);
            Color[] tileVacancyMapPixels = currTileType.GetPixelsFromSpritesheet(this.tileVacancyMap);

            // then fetch the information for it and initialise
            currTile.Initialise(
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
