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
                // not filling a tile that is needed to be filled / no vacancy where it should be?
                if( (checkingTile.adjacencyRequired[k] && !adjacentFilled[k]) || (checkingTile.vacancyRequired[k] && adjacentFilled[k]) ){
                    yuckyOption = true;
                    break;
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
            // the current working tile
            TileData currTile = this.tileDataList[index];
            // get the type
            TileType currTileType  = currTile.tileType;
            // then fetch the information for it and initialise
            currTile.Initialise(
                currTileType.GetPixelsFromSpritesheet(this.tileFilledMap),
                currTileType.GetPixelsFromSpritesheet(this.tileAdjacencyMap),
                currTileType.GetPixelsFromSpritesheet(this.tileVacancyMap)
            );
        }
    }

    

    public void Initialise(){
        this.InitialiseTiles();
    }
}
