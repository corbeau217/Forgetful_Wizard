using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CellGenerationType {
    Filled,
    Movement,
    Detail
}

// [CreateAssetMenu(fileName = "RoomGenerator", menuName = "ScriptableObjects/RoomGenerator", order = 1)]
public class RoomGenerator {

    public RoomGeneratorSettings roomSettings;

    Vector2Int roomDimensions;

    bool[,] roomMovementCells;
    bool[,] roomDetailCells;

    CellGenerationType[,] cellGenerationTypes;

    bool[,] roomOccupiedCells;

    TileData[,] roomTileLayout;


    public RoomGenerator(RoomGeneratorSettings settings){
        this.roomSettings = settings;
    }

    public void Initialise(){

        // make sure everything is ready to use
        this.roomSettings.Initialise();

        // get the room sizing
        this.roomDimensions = this.roomSettings.GetDimensions();

        // make the arrays
        this.roomMovementCells = new bool[this.roomDimensions.y, this.roomDimensions.x];
        this.roomDetailCells = new bool[this.roomDimensions.y, this.roomDimensions.x];

        this.cellGenerationTypes = new CellGenerationType[this.roomDimensions.y, this.roomDimensions.x];

        this.roomOccupiedCells = new bool[this.roomDimensions.y, this.roomDimensions.x];

        this.roomTileLayout = new TileData[this.roomDimensions.y, this.roomDimensions.x];


        // find the cell fillings and combine passage and shape layers
        this.GatherCellFills();

        this.GatherCellTypes();

        // find the actual tiledata to use
        this.GenerateCellData();
    }


    private void GatherCellFills(){
        for (int rowIndex = 0; rowIndex < this.roomDimensions.y; rowIndex++) {
            for (int colIndex = 0; colIndex < this.roomDimensions.x; colIndex++) {
                this.roomMovementCells[rowIndex,colIndex] = this.roomSettings.IsMovementLayerCell(rowIndex,colIndex);
                this.roomDetailCells[rowIndex,colIndex] = this.roomSettings.IsDetailLayerCell(rowIndex,colIndex);

                // not a movement cell or it's a detail cell
                this.roomOccupiedCells[rowIndex,colIndex] = !this.roomMovementCells[rowIndex,colIndex] || this.roomDetailCells[rowIndex,colIndex];
            }
        }
    }
    private void GatherCellTypes(){
        for (int rowIndex = 0; rowIndex < this.roomDimensions.y; rowIndex++) {
            for (int colIndex = 0; colIndex < this.roomDimensions.x; colIndex++) {
                
                // when not a movement cell
                if(!this.roomMovementCells[rowIndex,colIndex]){
                    this.cellGenerationTypes[rowIndex,colIndex] = CellGenerationType.Filled;
                    this.roomOccupiedCells[rowIndex,colIndex] = true;
                }
                else{
                    // used for detail?
                    if(this.roomDetailCells[rowIndex,colIndex]){
                        this.cellGenerationTypes[rowIndex,colIndex] = CellGenerationType.Detail;
                        this.roomOccupiedCells[rowIndex,colIndex] = true;
                    }
                    else{
                        this.cellGenerationTypes[rowIndex,colIndex] = CellGenerationType.Movement;
                        this.roomOccupiedCells[rowIndex,colIndex] = false;
                    }
                }
            }
        }
    }

    private void GenerateCellData(){
        for (int rowIndex = 0; rowIndex < this.roomDimensions.y; rowIndex++) {
            for (int colIndex = 0; colIndex < this.roomDimensions.x; colIndex++) {
                TileSetData tileSetOfTile;

                // prepare receipts
                bool[] adjacency = this.GetAdjacencyList( rowIndex, colIndex );

                // find our tileset for this tile
                switch (this.cellGenerationTypes[rowIndex,colIndex]) {
                    default:
                    case CellGenerationType.Filled:
                    case CellGenerationType.Movement:
                        tileSetOfTile = this.roomSettings.roomBaseTileset;
                        break;
                    case CellGenerationType.Detail:
                        tileSetOfTile = this.roomSettings.detailTileset;
                        break;
                }
                // use it
                this.roomTileLayout[ rowIndex, colIndex ] = tileSetOfTile.GetTileData(adjacency);
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
        return new bool[]{
            this.IsOccupied( rowIndex-1, colIndex-1 ),
            this.IsOccupied( rowIndex-1, colIndex   ),
            this.IsOccupied( rowIndex-1, colIndex+1 ),

            this.IsOccupied( rowIndex,   colIndex-1 ),
            this.IsOccupied( rowIndex,   colIndex   ),
            this.IsOccupied( rowIndex,   colIndex+1 ),

            this.IsOccupied( rowIndex+1, colIndex-1 ),
            this.IsOccupied( rowIndex+1, colIndex   ),
            this.IsOccupied( rowIndex+1, colIndex+1 )
        };
    }
    private bool IsOccupied( int rowIndex, int colIndex ){
        if( (rowIndex < 0) || (colIndex < 0) || (rowIndex >= this.roomDimensions.y) || (colIndex >= this.roomDimensions.x) ){
            return false;
        }
        return this.roomOccupiedCells[ rowIndex, colIndex ];
    }


    public TileData GetTileData( int rowIndex, int colIndex ){
        return this.roomTileLayout[ rowIndex, colIndex ];
    }
    public Vector2Int GetDimensions(){
        return this.roomDimensions;
    }
}