using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// for grid manipulation
//  shhhh it's silly but duct tape
//  makes the projects stop floating
using UnityEngine.UI;

// this is for generating the tile room in the world, and should just be runtime logic
public class RoomRenderer : MonoBehaviour
{
    // ================================================================
    // ================================================================
    // ------------------------------------------ private const fields

    private const float DEFAULT_PIXELS_PER_WORLD_UNIT = 100.0f;
    private const float TILE_SHAPE_INNER_SIZE_X = 1.8f;
    private const float TILE_SHAPE_INNER_SIZE_Y = 1.8f;


    // ================================================================
    // ================================================================
    // -------------------------------------------- public data fields

    public Vector2 tileOverlayScale = new Vector2(DEFAULT_PIXELS_PER_WORLD_UNIT * TILE_SHAPE_INNER_SIZE_X, DEFAULT_PIXELS_PER_WORLD_UNIT * TILE_SHAPE_INNER_SIZE_Y);
    // X and Z are cell size in grid, Y is grid scale for height
    public Vector3 cellDimensions = Vector3.one;

    public float overlayOpacity = 1.0f;


    // ================================================================
    // ================================================================
    // ------------------------------------------- private data fields
    
    private WorldGenData worldGenData;
    private RoomData roomData;

    private RoomLayerMaskData passageMaskData;

    private GameObject tileOverlayPrefab;

    private GameObject tileContainer;
    private GameObject tileOverlayContainer;

    private RectTransform containerRectTransform;
    private RectTransform overlayContainerRectTransform;

    private GridLayoutGroup containerGridLayoutGroup;
    private GridLayoutGroup overlayContainerGridLayoutGroup;
    
    private GameObject[,] tileObjects;
    private GameObject[,] tileOverlayObjects;

    // ================================================================
    // ================================================================
    // ------------------------------------------------- event methods

    public void GenerateFromData(WorldGenData worldGenData, RoomData roomDataToUse, RoomLayerMaskData passageMaskData){
        this.worldGenData = worldGenData;
        this.roomData = roomDataToUse;

        this.passageMaskData = passageMaskData;

        this.tileOverlayPrefab = this.worldGenData.tileOverlayPrefab;

        this.tileContainer = this.gameObject.transform.Find("TileGrid").gameObject;
        this.tileOverlayContainer = this.gameObject.transform.Find("TileOverlayGrid").gameObject;
        if(roomData == null){
            Debug.Log("erm, what the sigma??");
        }
        this.Initialise();
        this.LoadRoomData();
        this.GenerateTileObjects();
    }

    public void Initialise(){
        // prepare references

        this.containerRectTransform = this.tileContainer.GetComponent<RectTransform>();
        this.containerGridLayoutGroup = this.tileContainer.GetComponent<GridLayoutGroup>();
        this.overlayContainerRectTransform = this.tileOverlayContainer.GetComponent<RectTransform>();
        this.overlayContainerGridLayoutGroup = this.tileOverlayContainer.GetComponent<GridLayoutGroup>();

        // prepare room data
        
        this.roomData.Initialise(passageMaskData);
    }

    // ================================================================
    // ================================================================
    // ----------------------------------------------- private methods

    private void LoadRoomData(){
        // now generate our information
        if(this.roomData.RowCount() > 0 && this.roomData.ColCount() > 0){
            // =====================================
            // ---------------- prepare data arrays

            // prepare the grid
            this.tileObjects = new GameObject[ this.roomData.RowCount(), this.roomData.ColCount() ];
            this.tileOverlayObjects = new GameObject[ this.roomData.RowCount(), this.roomData.ColCount() ];

            // =====================================
            // ------------ prepare grid properties

            // prepare grid sizing
            float colSize = this.cellDimensions.x;
            float rowSize = this.cellDimensions.z;
            
            this.containerRectTransform.sizeDelta = new Vector2( colSize*this.roomData.ColCount(), rowSize*this.roomData.RowCount() );
            this.containerGridLayoutGroup.cellSize = new Vector2( colSize, rowSize );


            this.overlayContainerRectTransform.sizeDelta = new Vector2( colSize*this.roomData.ColCount(), rowSize*this.roomData.RowCount() );
            this.overlayContainerGridLayoutGroup.cellSize = new Vector2( colSize, rowSize );
            
        }
    }

    // get the tile objects from our room data and place them in to our tile room
    private void GenerateTileObjects(){
        Quaternion tileRotation = Quaternion.identity;
        Quaternion overlayRotation = Quaternion.identity;
        tileRotation.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        overlayRotation.eulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
        // every row
        for(int rowIndex = 0; rowIndex < this.roomData.RowCount(); rowIndex++){
            // every column
            for(int colIndex = 0; colIndex < this.roomData.ColCount(); colIndex++){

                // make the tile
                this.GenerateTile( rowIndex, colIndex, tileRotation );

                // make the overlay sprite
                this.GenerateTileOverlay( rowIndex, colIndex, overlayRotation );

            }
        }
    }

    private void GenerateTile( int rowIndex, int colIndex, Quaternion tileRotation ){
        GameObject tile = this.roomData.GetTileObject(rowIndex,colIndex);
        if(tile==null){
            Debug.Log("lol woops");
        }
        // generate tile for location
        GameObject tileInstance = (GameObject)Instantiate(
            // tile object
            tile,
            // parent transform
            this.tileContainer.transform
        );
        
        // anti rotation
        tileInstance.transform.rotation = tileRotation;

        // add rect transform to them
        RectTransform rt = tileInstance.AddComponent(typeof(RectTransform)) as RectTransform;
        
        // use it
        this.tileObjects[rowIndex,colIndex] = tileInstance;
    }


    private void GenerateTileOverlay( int rowIndex, int colIndex, Quaternion tileRotation ){
        // prepare tile information
        CellData cellData = this.roomData.GetCellData(rowIndex,colIndex);

        // generate tile for location
        GameObject tileOverlayInstance = (GameObject)Instantiate(
            this.tileOverlayPrefab,
            // parent transform
            this.tileOverlayContainer.transform
        );
    
        TileOverlayObject overlayObjectHandler = tileOverlayInstance.GetComponent<TileOverlayObject>();

        overlayObjectHandler.Initialise(
            cellData.cellPlacementRules.filledMaskTexture,
            cellData.cellPlacementRules.adjacencyTexture,
            cellData.cellPlacementRules.vacancyTexture,
            this.overlayOpacity
        );

        // anti rotation
        tileOverlayInstance.transform.rotation = tileRotation;

        // add rect transform to them
        RectTransform rt = tileOverlayInstance.AddComponent(typeof(RectTransform)) as RectTransform;

        // force our scaling
        float tileScaleX = (1.0f/overlayObjectHandler.textureWidth) * this.tileOverlayScale.x;
        float tileScaleY = (1.0f/overlayObjectHandler.textureHeight) * this.tileOverlayScale.y;
        tileOverlayInstance.transform.localScale = new Vector3(tileScaleX, tileScaleY, 1.0f);

        // give the location our created object
        this.tileOverlayObjects[rowIndex,colIndex] = tileOverlayInstance;
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
