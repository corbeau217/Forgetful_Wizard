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

    public PassageMaskData[] passageMasks;


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

    // ================================================================
    // ================================================================
    // ------------------------------------------------ public methods

    public LevelData GetLevelData(){
        return this.baseLevelData;
    }

    public RoomLayerMaskData GetRoomPassageFromType(TileType typeIn){
        for (int index = 0; index < this.passageMasks.Length; index++) {
            if(this.passageMasks[index].passageType == typeIn){
                return this.passageMasks[index].roomMask;
            }
        }
        Debug.Log("failed to find in "+this.passageMasks.Length);
        return null;
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
