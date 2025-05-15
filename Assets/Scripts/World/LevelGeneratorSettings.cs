using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GeneratorSettings", menuName = "ScriptableObjects/LevelGeneratorSettings", order = 1)]
public class LevelGeneratorSettings : ScriptableObject
{
    // ================================================================
    // ================================================================
    // -------------------------------------------- public data fields

    private PassageSetData passageTileset;
    public RoomLayerMaskData levelShapeLayer;

    // ================================================================
    // ================================================================
    // ------------------------------------------- private data fields

    // ================================================================
    // ================================================================
    // ------------------------------------------------- event methods
    
    public void Initialise(){
        Debug.Log("LevelGeneratorSettings.Initialise() called");
        // this.passageTileset.Initialise();
        // this.passageTileset = passageTileset;
        this.levelShapeLayer.Initialise();
    }

    // ================================================================
    // ================================================================
    // ----------------------------------------------- private methods

    // ================================================================
    // ================================================================
    // ----------------------------------------- public getter methods

    public Vector2Int GetDimensions(){
        return this.levelShapeLayer.GetDimensions();
    }

    public bool IsRoomUsedCell( int rowIndex, int colIndex ){
        return this.levelShapeLayer.GetLocationIsFilled( rowIndex, colIndex );
    }

    // ================================================================
    // ================================================================
}
