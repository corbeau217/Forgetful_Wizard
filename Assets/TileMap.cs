using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// for grid manipulation
//  shhhh it's silly but duct tape
//  makes the projects stop floating
using UnityEngine.UI;

// this is for generating the tile map in the world, and should just be runtime logic
public class TileMap : MonoBehaviour
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


    public MapData mapData;
    // contains the room tiles
    public GameObject tileContainer;
    // has the sprites for debugging room tiles
    public GameObject tileOverlayContainer;

    public GameObject tileOverlayPrefab;

    public Vector2 tileOverlayScale = new Vector2(DEFAULT_PIXELS_PER_WORLD_UNIT * TILE_SHAPE_INNER_SIZE_X, DEFAULT_PIXELS_PER_WORLD_UNIT * TILE_SHAPE_INNER_SIZE_Y);
    // X and Z are cell size in grid, Y is grid scale for height
    public Vector3 cellDimensions = Vector3.one;

    public float overlayOpacity = 1.0f;

    public bool fillOnStart;


    // ================================================================
    // ================================================================
    // ------------------------------------------- private data fields

    private RectTransform containerRectTransform;
    private GridLayoutGroup containerGridLayoutGroup;
    private RectTransform overlayContainerRectTransform;
    private GridLayoutGroup overlayContainerGridLayoutGroup;
    
    private GameObject[,] tileObjects;
    private GameObject[,] tileOverlayObjects;

    // ================================================================
    // ================================================================
    // ------------------------------------------------- event methods

    public void Initialise(){
        // prepare references

        this.containerRectTransform = this.tileContainer.GetComponent<RectTransform>();
        this.containerGridLayoutGroup = this.tileContainer.GetComponent<GridLayoutGroup>();
        this.overlayContainerRectTransform = this.tileOverlayContainer.GetComponent<RectTransform>();
        this.overlayContainerGridLayoutGroup = this.tileOverlayContainer.GetComponent<GridLayoutGroup>();

        // prepare map data
        
        this.mapData.Initialise();
    }

    // ================================================================
    // ================================================================
    // ----------------------------------------------- private methods

    private void LoadMapData(){
        // now generate our information
        if(this.mapData.RowCount() > 0 && this.mapData.ColCount() > 0){
            // =====================================
            // ---------------- prepare data arrays

            // prepare the grid
            this.tileObjects = new GameObject[ this.mapData.RowCount(), this.mapData.ColCount() ];
            this.tileOverlayObjects = new GameObject[ this.mapData.RowCount(), this.mapData.ColCount() ];

            // =====================================
            // ------------ prepare grid properties

            // prepare grid sizing
            float colSize = this.cellDimensions.x;
            float rowSize = this.cellDimensions.z;
            
            this.containerRectTransform.sizeDelta = new Vector2( colSize*this.mapData.ColCount(), rowSize*this.mapData.RowCount() );
            this.containerGridLayoutGroup.cellSize = new Vector2( colSize, rowSize );


            this.overlayContainerRectTransform.sizeDelta = new Vector2( colSize*this.mapData.ColCount(), rowSize*this.mapData.RowCount() );
            this.overlayContainerGridLayoutGroup.cellSize = new Vector2( colSize, rowSize );
            
        }
    }

    // get the tile objects from our map data and place them in to our tile map
    private void GenerateTileObjects(){
        Quaternion tileRotation = Quaternion.identity;
        Quaternion overlayRotation = Quaternion.identity;
        tileRotation.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        overlayRotation.eulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
        // every row
        for(int rowIndex = 0; rowIndex < this.mapData.RowCount(); rowIndex++){
            // every column
            for(int colIndex = 0; colIndex < this.mapData.ColCount(); colIndex++){

                // make the tile
                this.GenerateTile( rowIndex, colIndex, tileRotation );

                // make the overlay sprite
                this.GenerateTileOverlay( rowIndex, colIndex, overlayRotation );

            }
        }
    }

    private void GenerateTile( int rowIndex, int colIndex, Quaternion tileRotation ){
        GameObject tile = this.mapData.GetTileObject(rowIndex,colIndex);
        // generate tile for location
        this.tileObjects[rowIndex,colIndex] = (GameObject)Instantiate(
            // tile object
            tile,
            // parent transform
            this.tileContainer.transform
        );
        
        // anti rotation
        this.tileObjects[rowIndex,colIndex].transform.rotation = tileRotation;

        // add rect transform to them
        RectTransform rt = this.tileObjects[rowIndex,colIndex].AddComponent(typeof(RectTransform)) as RectTransform;
    }


    private void GenerateTileOverlay( int rowIndex, int colIndex, Quaternion tileRotation ){
        // prepare tile information
        TileData tileData = this.mapData.GetTileData(rowIndex,colIndex);

        // generate tile for location
        GameObject tileOverlayInstance = (GameObject)Instantiate(
            this.tileOverlayPrefab,
            // parent transform
            this.tileOverlayContainer.transform
        );

        TileOverlayObject overlayObjectHandler = tileOverlayInstance.GetComponent<TileOverlayObject>();
        overlayObjectHandler.Initialise(
            tileData.filledMaskTexture,
            tileData.adjacencyTexture,
            tileData.vacancyTexture,
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
        if(fillOnStart){
            this.Initialise();
            this.LoadMapData();
            this.GenerateTileObjects();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ...
    }

    // ================================================================
    // ================================================================
}
