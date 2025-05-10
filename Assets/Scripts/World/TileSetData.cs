using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileSet", menuName = "ScriptableObjects/TileSetData", order = 1)]
public class TileSetData : ScriptableObject
{   
    public TileData[] tileDataList;

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
                if( (checkingTile.filledRequired[k] && !adjacentFilled[k]) || (checkingTile.vacancyRequired[k] && adjacentFilled[k]) ){
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
            // Debug.Log("NO DESIRABLE TILES");
            // default to block
            return null;
        }
        if(desirableCount > 1){
            Debug.Log("multiple desirable for placement: "+desirableCount);
        }

        return this.tileDataList[desirableIndex];
    }
}
