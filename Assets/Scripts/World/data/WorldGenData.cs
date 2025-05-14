using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WorldGen", menuName = "ScriptableObjects/WorldGenData", order = 1)]
public class WorldGenData : ScriptableObject
{
    // ================================================================
    // ================================================================
    // ------------------------------------------ private const fields

    // ================================================================
    // ================================================================
    // -------------------------------------------- public data fields

    // public PassageMaskData[] passageMasks;
    public PassageSetData passageTileset;

    public LevelData baseLevelData;
    public GameObject baseRoomPrefab;
    public GameObject tileOverlayPrefab;

    public Vector2 defaultRoomSize = new Vector2(31.0f, 31.0f);
    public Vector2 defaultTileSize = new Vector2(2.0f, 2.0f);

    // ================================================================
    // ================================================================
    // ------------------------------------------- private data fields

    // ================================================================
    // ================================================================
    // ------------------------------------------------- event methods

    public void Initialise(){
        this.passageTileset.Initialise();
    }

    // ================================================================
    // ================================================================
    // ------------------------------------------------ public methods

    public LevelData GetLevelData(){
        return this.baseLevelData;
    }

    public PassageMaskData GetRoomPassageFromType(TileType typeIn){
        for (int index = 0; index < this.passageTileset.tileDataList.Length; index++) {
            if(this.passageTileset.tileDataList[index].passageType == typeIn){
                return this.passageTileset.tileDataList[index];
            }
        }
        Debug.Log("failed to find in "+this.passageTileset.tileDataList.Length);
        return null;
    }

    public PassageMaskData GetRoomPassageFromAdjacent(bool[] adjacency){
        return this.passageTileset.GetPassageMaskData(adjacency);
    }

    // ================================================================
    // ================================================================
    // ----------------------------------------------- private methods

    // ================================================================
    // ================================================================
    // ------------------------------------------------- unity methods

    // ================================================================
    // ================================================================
}
