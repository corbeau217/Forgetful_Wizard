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
        // TODO : handle adjacency matrix, hash and find by hash

        return tileDataBlock;
    }

    public int GetAdjacencyHash(bool[] adjacentFilled){
        int result = 0;
        for(int i = 0; i < adjacentFilled.Length; i++){
            // add left shifted bit based on adjacency
            //  index is the power of 2 to add
            if(adjacentFilled[i]){
                result += (0x01 << i);
            }
        }
        return result;
    }
}
