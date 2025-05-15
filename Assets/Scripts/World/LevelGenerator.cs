using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator {

    public LevelGeneratorSettings levelSettings;

    PassageSetData passageTileset;

    Vector2Int levelDimensions;
    // int colCount = 31;
    // int rowCount = 31;

    // bool[,] levelRoomCells;

    private PassageMaskData[,] passageMasks;

    public LevelGenerator(LevelGeneratorSettings settings, PassageSetData passageTileset){
        Debug.Log("LevelGenerator constructed");
        this.levelSettings = settings;
        this.passageTileset = passageTileset;
    }
    public void Initialise(){
        Debug.Log("LevelGenerator.Initialise() called");
        // handoff initialise others
        // this.levelSettings.Initialise();

        // gather our data
        this.levelDimensions = this.levelSettings.GetDimensions();

        // make our arrays
        // this.levelRoomCells = new bool[this.levelDimensions.y, this.levelDimensions.x];
        this.passageMasks = new PassageMaskData[this.levelDimensions.y,this.levelDimensions.x];

        this.GeneratePassageMasks();
    }
    // generate passage masks
    public void GeneratePassageMasks(){
        for (int rowIndex = 0; rowIndex < this.levelDimensions.x; rowIndex++) {
            for (int colIndex = 0; colIndex < this.levelDimensions.y; colIndex++) {
                // when room at the place
                if(this.levelSettings.IsRoomUsedCell(rowIndex,colIndex)){
                    // we make the room
                    bool[] adjacency = this.GetAdjacencyList(rowIndex,colIndex);
                    // find the passage
                    this.passageMasks[rowIndex,colIndex] = this.passageTileset.GetPassageMaskData(adjacency);
                }
                // otherwise? 
                else {
                    // fill it with junk :D 
                    // whatever the default is
                    this.passageMasks[rowIndex,colIndex] = this.passageTileset.defaultTile;
                }
            }
        }
    }




    // fetching the adjacency information for a given
    //  location within our grid
    // 
    // where CC is the current cell we want the following:
    // 
    //   [ TL ][ TC ][ TR ]
    //   [ CL ][ CC ][ CR ]
    //   [ BL ][ BC ][ BR ]
    // 
    // this is fetched where the top left is the first, and
    //  bottom right is the last in row major order
    public bool[] GetAdjacencyList( int rowIndex, int colIndex ){
        bool[] adjacencyList = new bool[9];
        int i = 0;
        for (int rowOffset = -1; rowOffset < 2; rowOffset++)
        {
            for (int colOffset = -1; colOffset < 2; colOffset++)
            {
                adjacencyList[i++] = this.GetRoomExistsAt(rowIndex+rowOffset, colIndex+colOffset);
            }
        }
        return adjacencyList;
    }

    public bool GetRoomExistsAt(int rowIndex, int colIndex){
        // out of bounds
        if( (rowIndex < 0) || (colIndex < 0) || (rowIndex >= this.levelDimensions.y) || (colIndex >= this.levelDimensions.x) ){
            return false;
        }
        return this.levelSettings.IsRoomUsedCell(rowIndex,colIndex);
    }
    public PassageMaskData GetPassageMask(int rowIndex, int colIndex){
        return this.passageMasks[rowIndex,colIndex];
    }
    public Vector2Int GetDimensions(){
        return this.levelDimensions;
    }
}