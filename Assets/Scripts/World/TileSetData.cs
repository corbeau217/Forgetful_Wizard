using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileSet", menuName = "ScriptableObjects/TileSetData", order = 1)]
public class TileSetData : ScriptableObject
{   
    public TileData[] tileDataList;
    public TileData tileDataEmpty;
    public TileData tileDataBlock;

    public TileData GetTileData(bool[] adjacentFilled){
        // prepare the hash of what we have
        int lookingAdjacencyHash = TileData.GetAdjacencyHashFrom(adjacentFilled);

        // find a matching hash
        for(int i = 0; i < tileDataList.Length; i++){
            int tileVacancyHash = tileDataList[i].GetVacancyHash();
            int tileAdjacencyHash = tileDataList[i].GetAdjacencyHash();

            // use the vancancy has as a bitwise mask and see if we match something,
            //  but also that we have the required adjacency for the tile
            if( (tileVacancyHash & lookingAdjacencyHash) == 0 && (tileAdjacencyHash & lookingAdjacencyHash) == tileAdjacencyHash){
                // found a hit!
                Debug.Log("filled["+(lookingAdjacencyHash)+"] -> tile["+(tileAdjacencyHash)+"] matched!");
                return this.tileDataList[i];
            }
            // // just vacancy matches 
            // else if( (tileVacancyHash & lookingAdjacencyHash) > 0 ){
            //     Debug.Log("filled["+(lookingAdjacencyHash)+"] -> tile["+(tileAdjacencyHash)+"] :: ONLY VACANT MATCHED");
            // }
            // // just adjacency matches
            // else if( tileAdjacencyHash & lookingAdjacencyHash == tileAdjacencyHash ){
            //     Debug.Log("filled["+(lookingAdjacencyHash)+"] -> tile["+(tileAdjacencyHash)+"] :: ONLY ADJACENT MATCHED");
            // }
            // else {
            //     Debug.Log("filled["+(lookingAdjacencyHash)+"] -> tile["+(tileAdjacencyHash)+"] :: DOESNT MATCH");
            // }
        }

        // default to block
        return tileDataBlock;
    }
}
