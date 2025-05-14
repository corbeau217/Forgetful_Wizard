using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CellGenerationType {
    // TODO : include this in utils
    Empty,
    Filled,
    Passage,
    Movement,
    Shelf,
    Pillar
}
public static class CellGenerationTypeUtils {
    public static CellGenerationType GetTypeFromMask( bool passageUsed, bool movementUsed, bool shelfUsed, bool pillarUsed ){
        // passage tiles dont care about any other type of cell
        if (passageUsed) { return CellGenerationType.Movement; }
        
        // still movement if shelf/pillar isnt using it
        else if (movementUsed && !(shelfUsed||pillarUsed)) { return CellGenerationType.Movement; }
        
        // shelf/pillar wants to use it and would be free? pillar takes priority
        else if (movementUsed && (shelfUsed||pillarUsed)) { return (pillarUsed)? CellGenerationType.Pillar : CellGenerationType.Shelf; }

        // otherwise fill it
        else { return CellGenerationType.Filled; }
    }

    public static bool AllowsDetailTile(this CellGenerationType typeIn){
        switch (typeIn) {
            default:
            case CellGenerationType.Shelf:
            case CellGenerationType.Pillar:
            case CellGenerationType.Movement:
                return true;
            case CellGenerationType.Filled:
            case CellGenerationType.Passage:
                return false;
        }
    }
    public static bool FindsOtherTypeOccupied(this CellGenerationType typeIn, CellGenerationType otherIn){
        // what we care about?
        switch (typeIn) {
            default:
            case CellGenerationType.Movement:
            case CellGenerationType.Passage:
                // only filled cells
                return otherIn == CellGenerationType.Filled;
            case CellGenerationType.Shelf:
            case CellGenerationType.Pillar:
            case CellGenerationType.Filled:
                // anything that fills a cells
                return otherIn.IsOccupied();
        }
    }
    public static bool IsOccupied(this CellGenerationType typeIn){
        switch (typeIn) {
            default:
            case CellGenerationType.Movement:
            case CellGenerationType.Passage:
                return false;
            case CellGenerationType.Shelf:
            case CellGenerationType.Pillar:
            case CellGenerationType.Filled:
                return true;
        }
    }
    public static TileSetData TryToUseDetailTileset(this CellGenerationType typeIn, TileSetData baseTileset, TileSetData shelfTileset, TileSetData pillarTileset){
        if(typeIn.AllowsDetailTile()){
            if(typeIn==CellGenerationType.Pillar) return pillarTileset;
            else return shelfTileset;
        }
        else {
            return baseTileset;
        }
    }
}


// [CreateAssetMenu(fileName = "RoomGenerator", menuName = "ScriptableObjects/RoomGenerator", order = 1)]
public class RoomGenerator {

    public RoomGeneratorSettings roomSettings;

    RoomLayerMaskData roomPassageMask;

    Vector2Int roomDimensions;

    CellGenerationType[,] cellGenerationTypes;

    bool[,] roomOccupiedCells;

    TileData[,] roomTileLayout;


    public RoomGenerator(RoomGeneratorSettings settings, RoomLayerMaskData roomPassageMask){
        this.roomSettings = settings;
        this.roomPassageMask = roomPassageMask;
    }

    public void Initialise(){
        this.roomSettings.Initialise(this.roomPassageMask);

        // get the room sizing
        this.roomDimensions = this.roomSettings.GetDimensions();

        this.cellGenerationTypes = new CellGenerationType[this.roomDimensions.y, this.roomDimensions.x];

        this.roomTileLayout = new TileData[this.roomDimensions.y, this.roomDimensions.x];

        this.GatherCellTypes();

        // find the actual tiledata to use
        this.GenerateCellData();
    }

    private void GatherCellTypes(){
        for (int rowIndex = 0; rowIndex < this.roomDimensions.y; rowIndex++) {
            for (int colIndex = 0; colIndex < this.roomDimensions.x; colIndex++) {
                this.cellGenerationTypes[rowIndex, colIndex] = CellGenerationTypeUtils.GetTypeFromMask(
                    this.roomSettings.IsPassageUsedCell(rowIndex,colIndex),
                    this.roomSettings.IsMovementUsedCell(rowIndex,colIndex),
                    this.roomSettings.IsShelfUsedCell(rowIndex,colIndex),
                    this.roomSettings.IsPillarUsedCell(rowIndex,colIndex)
                );

            }
        }
    }

    private void GenerateCellData(){
        for (int rowIndex = 0; rowIndex < this.roomDimensions.y; rowIndex++) {
            for (int colIndex = 0; colIndex < this.roomDimensions.x; colIndex++) {
                // prepare receipts
                bool[] adjacency = this.GetAdjacencyList( rowIndex, colIndex );

                // find our tileset for this tile
                TileSetData tileSetOfTile;
                if(this.cellGenerationTypes[rowIndex,colIndex].IsOccupied()){
                    tileSetOfTile = this.cellGenerationTypes[rowIndex,colIndex].TryToUseDetailTileset(this.roomSettings.roomBaseTileset, this.roomSettings.shelfTileset, this.roomSettings.pillarTileset);
                }
                else {
                    tileSetOfTile = this.roomSettings.roomBaseTileset;
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
        bool[] adjacencyList = new bool[9];
        int i = 0;
        for (int rowOffset = -1; rowOffset < 2; rowOffset++)
        {
            for (int colOffset = -1; colOffset < 2; colOffset++)
            {
                adjacencyList[i++] = this.cellGenerationTypes[rowIndex,colIndex].FindsOtherTypeOccupied( this.GetCellGenerationTypeAt(rowIndex+rowOffset, colIndex+colOffset) );
            }
        }
        return adjacencyList;
    }
    private CellGenerationType GetCellGenerationTypeAt( int rowIndex, int colIndex ){
        if( (rowIndex < 0) || (colIndex < 0) || (rowIndex >= this.roomDimensions.y) || (colIndex >= this.roomDimensions.x) ){
            return CellGenerationType.Empty;
        }
        return this.cellGenerationTypes[ rowIndex, colIndex ];
    }


    public TileData GetTileData( int rowIndex, int colIndex ){
        return this.roomTileLayout[ rowIndex, colIndex ];
    }
    public Vector2Int GetDimensions(){
        return this.roomDimensions;
    }
}