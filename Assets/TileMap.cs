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
    // -------------------------------------------- public data fields

    public MapData mapData;
    // contains the room tiles
    public GameObject tileContainer;
    // has the sprites for debugging room tiles
    public GameObject tileOverlayContainer;

    public GameObject tileOverlayPrefab;

    public Vector2 tileOverlayScale = new Vector2(80.0f, 80.0f);
    // X and Z are cell size in grid, Y is grid scale for height
    public Vector3 cellDimensions = Vector3.one;

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

                // TODO: make the overlay sprite
                this.GenerateTileOverlay( rowIndex, colIndex, overlayRotation );

            }
        }
    }

    private void GenerateTile( int rowIndex, int colIndex, Quaternion tileRotation ){
        // generate tile for location
        this.tileObjects[rowIndex,colIndex] = (GameObject)Instantiate(
            // tile object
            this.mapData.GetTileObject(rowIndex,colIndex),
            // parent transform
            this.tileContainer.transform
        );
        
        // anti rotation
        this.tileObjects[rowIndex,colIndex].transform.rotation = tileRotation;

        // add rect transform to them
        RectTransform rt = this.tileObjects[rowIndex,colIndex].AddComponent(typeof(RectTransform)) as RectTransform;
    }


    private void GenerateTileOverlay( int rowIndex, int colIndex, Quaternion tileRotation ){
        // generate tile for location
        this.tileOverlayObjects[rowIndex,colIndex] = (GameObject)Instantiate(
            this.tileOverlayPrefab,
            // parent transform
            this.tileOverlayContainer.transform
        );

        // anti rotation
        this.tileOverlayObjects[rowIndex,colIndex].transform.rotation = tileRotation;

        // add rect transform to them
        RectTransform rt = this.tileOverlayObjects[rowIndex,colIndex].AddComponent(typeof(RectTransform)) as RectTransform;

        // fetch the texture to use
        Texture2D tileTexture = this.mapData.GetTileOverlayTexture(rowIndex,colIndex);
        Sprite textureSprite = Sprite.Create(tileTexture, new Rect(0, 0, tileTexture.width, tileTexture.height), new Vector2(0.0f, 0.0f));
        SpriteRenderer spriteRender = this.tileOverlayPrefab.GetComponent<SpriteRenderer>();
        spriteRender.sprite = textureSprite;

        float tileScaleX = (1.0f/tileTexture.width) * this.tileOverlayScale.x;
        float tileScaleY = (1.0f/tileTexture.height) * this.tileOverlayScale.y;
        this.tileOverlayObjects[rowIndex,colIndex].transform.localScale = new Vector3(tileScaleX, tileScaleY, 1.0f);
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
