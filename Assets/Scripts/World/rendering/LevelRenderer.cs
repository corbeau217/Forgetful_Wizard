using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// for grid manipulation
//  shhhh it's silly but duct tape
//  makes the projects stop floating
using UnityEngine.UI;

public class LevelRenderer : MonoBehaviour
{
    // ================================================================
    // ================================================================
    // ------------------------------------------ private const fields

    // ...

    // ================================================================
    // ================================================================
    // -------------------------------------------- public data fields

    // X and Z are room size in grid, Y is grid scale for height
    public Vector3 roomScale = Vector3.one;

    // ================================================================
    // ================================================================
    // ------------------------------------------- private data fields

    private WorldGenData worldGenData;
    private LevelData levelData;

    private GameObject roomContainer;
    
    private GameObject baseRoomPrefab;

    private RectTransform containerRectTransform;
    private GridLayoutGroup containerGridLayoutGroup;

    private GameObject[,] roomObjects;

    private TileType[,] passageTypes;

    private int rowCount;
    private int colCount;

    // ================================================================
    // ================================================================
    // ------------------------------------------------- event methods

    public void Generate(WorldGenData worldGenData){
        // save access to world generating data
        this.worldGenData = worldGenData;

        // gather the things we need
        this.baseRoomPrefab = this.worldGenData.baseRoomPrefab;
        this.levelData = this.worldGenData.GetLevelData();

        this.roomContainer = this.gameObject.transform.Find("LevelGrid").gameObject;

        // start making our level
        this.Initialise();
        this.LoadLevelData();
        this.GenerateRoomPassageTileTypes();
        this.GenerateRoomRenderers();
    }

    public void Initialise(){
        // prepare references

        this.containerRectTransform = this.roomContainer.GetComponent<RectTransform>();
        this.containerGridLayoutGroup = this.roomContainer.GetComponent<GridLayoutGroup>();

        this.rowCount = this.levelData.rowCount;
        this.colCount = this.levelData.colCount;

        // put us in the center?
        this.gameObject.transform.position = new Vector3(
            this.worldGenData.defaultRoomSize.x * (colCount/2),
            0.0f,
            this.worldGenData.defaultRoomSize.y * (rowCount/2)
        );
    }

    // ================================================================
    // ================================================================
    // ----------------------------------------------- private methods

    private void LoadLevelData(){
        // now generate our information
        if(this.rowCount > 0 && this.colCount > 0){
            // =====================================
            // ---------------- prepare data arrays

            // prepare the grid
            this.roomObjects = new GameObject[ this.rowCount, this.colCount ];
            this.passageTypes = new TileType[ this.rowCount, this.colCount ];

            // =====================================
            // ------------ prepare grid properties

            // prepare grid sizing
            float colSize = this.roomScale.x * this.worldGenData.defaultRoomSize.x * this.worldGenData.defaultTileSize.x;
            float rowSize = this.roomScale.y * this.worldGenData.defaultRoomSize.y * this.worldGenData.defaultTileSize.y;
            
            this.containerRectTransform.sizeDelta = new Vector2( colSize*this.colCount, rowSize*this.rowCount );
            this.containerGridLayoutGroup.cellSize = new Vector2( colSize, rowSize );            
        }
    }

    // HARD CODED PASSAGE TYPES FOR NOW
    private void GenerateRoomPassageTileTypes(){
        // every row
        for(int rowIndex = 0; rowIndex < this.rowCount; rowIndex++){
            // every column
            for(int colIndex = 0; colIndex < this.colCount; colIndex++){
                // bottom row row?
                if(rowIndex == 0){
                    // bottom right?
                    if(colIndex == 0){
                        this.passageTypes[rowIndex,colIndex] = TileType.Room_P2_BR;
                    }
                    // bottom left?
                    else if(colIndex == this.colCount-1){
                        this.passageTypes[rowIndex,colIndex] = TileType.Room_P2_BL;
                    }
                    // bottom row?
                    else {
                        this.passageTypes[rowIndex,colIndex] = TileType.Room_P3_NF;
                    }
                }
                // front row?
                else if(rowIndex == this.rowCount-1){
                    // top right?
                    if(colIndex == 0){
                        this.passageTypes[rowIndex,colIndex] = TileType.Room_P2_FR;
                    }
                    // top left?
                    else if(colIndex == this.colCount-1){
                        this.passageTypes[rowIndex,colIndex] = TileType.Room_P2_FL;
                    }
                    // top side?
                    else {
                        this.passageTypes[rowIndex,colIndex] = TileType.Room_P3_NB;
                    }
                }
                // middle rows
                else {
                    // left side?
                    if(colIndex == 0){
                        this.passageTypes[rowIndex,colIndex] = TileType.Room_P3_NL;
                    }
                    // right side?
                    else if(colIndex == this.colCount-1){
                        this.passageTypes[rowIndex,colIndex] = TileType.Room_P3_NR;
                    }
                    else {
                        this.passageTypes[rowIndex,colIndex] = TileType.Room_P4;
                    }
                }
            }
        }
    }
    private void GenerateRoomRenderers(){
        // every row
        for(int rowIndex = 0; rowIndex < this.rowCount; rowIndex++){
            // every column
            for(int colIndex = 0; colIndex < this.colCount; colIndex++){
                // make the room
                this.GenerateRoom( rowIndex, colIndex );
            }
        }
    }
    private void GenerateRoom( int rowIndex, int colIndex ){
        // prepare the room object
        GameObject newRoom = (GameObject)Instantiate(
            this.baseRoomPrefab,
            this.roomContainer.transform
        );
        // give it a rect transform
        RectTransform rt = newRoom.AddComponent(typeof(RectTransform)) as RectTransform;
        // find a room to use
        RoomData randomRoom = this.levelData.GetRandomRoom();
        // make it
        newRoom.GetComponent<RoomRenderer>().GenerateFromData(this.worldGenData, randomRoom, this.passageTypes[rowIndex,colIndex]);
        // save it for accessing
        this.roomObjects[ rowIndex, colIndex ] = newRoom;
        
    }

    // ================================================================
    // ================================================================
    // ------------------------------------------------- unity methods

    // Start is called before the first frame update
    void Start()
    {
        // ...
    }

    // Update is called once per frame
    void Update()
    {
        // ...
    }

    // ================================================================
    // ================================================================
}
