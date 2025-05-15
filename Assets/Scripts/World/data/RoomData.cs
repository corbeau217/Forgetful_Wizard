using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu(fileName = "Room", menuName = "ScriptableObjects/RoomData", order = 1)]
public class RoomData : ScriptableObject
{
    // ================================================================
    // ================================================================
    // -------------------------------------------- public data fields

    public RoomGeneratorSettings generatorSettings;
    public GameObject errorTile;

    // ================================================================
    // ================================================================
    // ------------------------------------------- private data fields
    
    private RoomGenerator roomGenerator;

    private Vector2Int roomDimensions = new Vector2Int(0, 0);

    // ================================================================
    // ================================================================
    // ------------------------------------------------- event methods

    public void Initialise(RoomLayerMaskData roomPassageMask){
        this.roomGenerator = new RoomGenerator(this.generatorSettings, roomPassageMask);
        this.roomGenerator.Initialise();

        this.roomDimensions = this.roomGenerator.GetDimensions();

    }
    
    // ================================================================
    // ================================================================
    // ----------------------------------------------- private methods

    // ================================================================
    // ================================================================
    // ----------------------------------------- public getter methods

    public int RowCount(){
        return this.roomDimensions.y;
    }
    public int ColCount(){
        return this.roomDimensions.x;
    }

    public CellData GetCellData( int rowIndex, int colIndex ){
        return this.roomGenerator.GetCellData(rowIndex,colIndex);
    }
    public GameObject GetTileObject( int rowIndex, int colIndex ){
        CellData data = this.GetCellData(rowIndex, colIndex);
        if(data==null){ return this.errorTile; }
        return ((data.cellOption) as TileOption).tilePrefab;
    }

    // return type???
    public Texture2D GetTileOverlayTexture( int rowIndex, int colIndex ){
        return this.GetCellData(rowIndex, colIndex).cellPlacementRules.filledMaskTexture;
    }

    // ================================================================
    // ================================================================
}
