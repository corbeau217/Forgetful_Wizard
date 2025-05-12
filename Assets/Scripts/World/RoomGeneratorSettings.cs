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

    public TileSetData detailTileset;
    public RoomLayerMaskData detailLayer;


    // ================================================================
    // ================================================================
    // ------------------------------------------- private data fields

    // ================================================================
    // ================================================================
    // ------------------------------------------------- event methods
    
    public void Initialise(RoomLayerMaskData passageLayerToUse){
        this.passageLayer = passageLayerToUse;

        this.roomBaseTileset.Initialise();
        this.passageLayer.Initialise();
        this.roomShapeLayer.Initialise();

        this.detailTileset.Initialise();
        this.detailLayer.Initialise();
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

    public bool IsMovementLayerCell( int rowIndex, int colIndex ){
        return this.passageLayer.GetLocationIsFilled( rowIndex, colIndex ) || this.roomShapeLayer.GetLocationIsFilled( rowIndex, colIndex );
    }
    public bool IsDetailLayerCell( int rowIndex, int colIndex ){
        return this.detailLayer.GetLocationIsFilled( rowIndex, colIndex );
    }

    // ================================================================
    // ================================================================
}
