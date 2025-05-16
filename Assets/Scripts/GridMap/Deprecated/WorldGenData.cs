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
    // public PassageSetData passageTileset;
    
    
    public CellOptionSet passageOptions;
    public CellSetData passageCellSet;


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
        // this.passageTileset.Initialise();
        passageCellSet = new CellSetData(this.passageOptions);
    }

    // ================================================================
    // ================================================================
    // ------------------------------------------------ public methods

    public LevelData GetLevelData(){
        return this.baseLevelData;
    }

    public RoomLayerMaskData GetRoomMaskFromType(CellType typeIn){
        CellData data = this.passageCellSet.GetCellFromType(typeIn);
        CellOptionBase option = (data.cellOption);
        RoomOption room = option as RoomOption;
        return room.roomMask;
    }

    public RoomLayerMaskData GetRoomMaskFromAdjacent(bool[] adjacency){
        CellData data = this.passageCellSet.GetCellData(adjacency);
        CellOptionBase option = (data.cellOption);
        RoomOption room = option as RoomOption;
        return room.roomMask;
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
