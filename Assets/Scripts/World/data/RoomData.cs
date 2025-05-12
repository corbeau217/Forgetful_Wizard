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

    // ================================================================
    // ================================================================
    // ------------------------------------------- private data fields
    
    private RoomGenerator roomGenerator;

    private Vector2Int roomDimensions = new Vector2Int(0, 0);

    // ================================================================
    // ================================================================
    // ------------------------------------------------- event methods

    public void Initialise(){

        // TODO : choose from list??????
        this.roomGenerator = new RoomGenerator(this.generatorSettings, PassageType.P4);
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

    public TileData GetTileData( int rowIndex, int colIndex ){
        return this.roomGenerator.GetTileData(rowIndex,colIndex);
    }
    public GameObject GetTileObject( int rowIndex, int colIndex ){
        return this.GetTileData(rowIndex, colIndex).TilePrefab;
    }

    // return type???
    public Texture2D GetTileOverlayTexture( int rowIndex, int colIndex ){
        return this.GetTileData(rowIndex, colIndex).filledMaskTexture;
    }

    // ================================================================
    // ================================================================
}
