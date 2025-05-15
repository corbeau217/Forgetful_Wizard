using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GeneratorSettings", menuName = "ScriptableObjects/RoomGeneratorSettings", order = 1)]
public class RoomGeneratorSettings : ScriptableObject
{
    // ================================================================
    // ================================================================
    // -------------------------------------------- public data fields
    
    public TileSetData roomBaseTileset;
    private RoomLayerMaskData passageLayer;
    public RoomLayerMaskData roomShapeLayer;

    public TileSetData shelfTileset;
    public TileSetData pillarTileset;
    public RoomLayerMaskData shelfLayer;
    public RoomLayerMaskData pillarLayer;


    // ================================================================
    // ================================================================
    // ------------------------------------------- private data fields

    // ================================================================
    // ================================================================
    // ------------------------------------------------- event methods
    
    public void Initialise(RoomLayerMaskData passageLayerToUse){
        // Debug.Log("RoomGeneratorSettings.Initialise() called");
        this.passageLayer = passageLayerToUse;

        this.roomBaseTileset.Initialise();
        this.passageLayer.Initialise();
        this.roomShapeLayer.Initialise();

        this.shelfTileset.Initialise();
        this.pillarTileset.Initialise();
        this.shelfLayer.Initialise();
        this.pillarLayer.Initialise();
    }

    // ================================================================
    // ================================================================
    // ----------------------------------------------- private methods

    // ================================================================
    // ================================================================
    // ----------------------------------------- public getter methods

    public Vector2Int GetDimensions(){
        return this.roomShapeLayer.GetDimensions();
    }

    public bool IsPassageUsedCell( int rowIndex, int colIndex ){
        return this.passageLayer.GetLocationIsFilled( rowIndex, colIndex );
    }
    public bool IsMovementUsedCell( int rowIndex, int colIndex ){
        return this.roomShapeLayer.GetLocationIsFilled( rowIndex, colIndex );
    }
    public bool IsShelfUsedCell( int rowIndex, int colIndex ){
        return this.shelfLayer.GetLocationIsFilled( rowIndex, colIndex );
    }
    public bool IsPillarUsedCell( int rowIndex, int colIndex ){
        return this.pillarLayer.GetLocationIsFilled( rowIndex, colIndex );
    }

    // ================================================================
    // ================================================================
}
