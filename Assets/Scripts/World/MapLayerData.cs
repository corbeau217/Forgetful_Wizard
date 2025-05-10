using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapLayer", menuName = "ScriptableObjects/MapLayerData", order = 1)]
public class MapLayerData : ScriptableObject
{

    // ================================================================
    // ================================================================
    // -------------------------------------------- public data fields

    public TileSetData tileSet;
    // where the layer is loaded from
    public Texture2D layerFillMaskImage;
    // colour that this layer uses to fill
    public Color layerFillColour;
    // when Color rgba is Vector4, what is the
    //  range of magnitude difference we accept
    public float maskTollerance;

    // treat fill mask as its opposite
    public bool invertFillMask;

    public bool fillOnError;

    // ================================================================
    // ================================================================
    // ------------------------------------------- private data fields

    // retreived pixel colouring from the layer fill mask
    private Color[] pixelColourData;

    // the filling of cells
    private bool[,] cellFillMask;

    private Vector2Int layerDimensions = new Vector2Int(0, 0);

    // for determining if safe to give information
    //  in public getters
    private bool loadedLayerData;

    // ================================================================
    // ================================================================
    // ------------------------------------------------- event methods

    public void Initialise(){
        // have a layer mask to use
        if( this.layerFillMaskImage != null ){
            this.layerDimensions.x = this.layerFillMaskImage.width;
            this.layerDimensions.y = this.layerFillMaskImage.height;

            // gather pixel information
            this.pixelColourData = this.layerFillMaskImage.GetPixels(0);
            // prepare fill mask array
            this.cellFillMask = new bool[ this.layerDimensions.y, this.layerDimensions.x ];

            // hand off for layer mask building
            this.InitialiseFillMask();

            // so the public getters are happy
            this.loadedLayerData = true;
        }
        else {
            Debug.Log("Please provide layerFillMaskImage!");
        }
    }
    
    // ================================================================
    // ================================================================
    // ----------------------------------------------- private methods

    // converts coordinates to texture's pixel index
    //  coordinates start from the top left in row major order
    //  texture pixel indices start bottom left also in row major order 
    private int CoordAsPixelIndex(int rowIndex, int colIndex){
        // starting from top
        int topRowIndex = this.layerDimensions.y-1;
        // flipping row index to be bottom to top
        int rowIndexFlipped = topRowIndex-rowIndex;
        // row is number of 'layerDimensions.x' of pixels
        return rowIndexFlipped*this.layerDimensions.x + colIndex;
    }

    // turn both colours in to vectors and then compare their magnitudes
    //  returns true when there's no more than maskTollerance difference
    private bool ColourDesiredByLayer(Color colourToTry){
        Vector4 desiredColour = new Vector4( this.layerFillColour.r, this.layerFillColour.g, this.layerFillColour.b, this.layerFillColour.a );
        Vector4 testingColour = new Vector4(          colourToTry.r,          colourToTry.g,          colourToTry.b,          colourToTry.a );

        float magnitudeDifference = Mathf.Abs(desiredColour.magnitude - testingColour.magnitude);

        return magnitudeDifference < this.maskTollerance;
    }

    // loading our cellFillMask from pixelColourData
    //  assumes that we've already loaded pixelColourData from texture
    private void InitialiseFillMask(){
        // each row
        for(int rowIndex = 0; rowIndex < this.layerDimensions.y; rowIndex++){
            // gather that rows bits, int is 32 bits but we ignore signed bit, left to right for columns
            for(int colIndex = 0; colIndex < this.layerDimensions.x; colIndex++){
                // fetch colour
                Color cellColor = this.pixelColourData[CoordAsPixelIndex(rowIndex, colIndex)];

                // test fits our layer masking and save in our mask
                this.cellFillMask[rowIndex,colIndex] = this.ColourDesiredByLayer( cellColor );
            }
        }
    }

    // ================================================================
    // ================================================================
    // ----------------------------------------- public getter methods

    public int RowCount(){
        return (this.loadedLayerData)?this.layerDimensions.y:-1;
    }
    public int ColCount(){
        return (this.loadedLayerData)?this.layerDimensions.x:-1;
    }

    // determine if a location should be labeled as filled
    //  using fillOnError for value when provided spooky coordinates
    public bool IsLocationFilled(int rowIndex, int colIndex){
        // handle errors
        //  not loaded / out of bounds
        if( (!this.loadedLayerData) || ((rowIndex < 0) || (colIndex < 0) || (rowIndex >= this.layerDimensions.y) || (colIndex >= this.layerDimensions.x)) ) {
            // use error fill value incase we're not background layer
            return this.fillOnError;
        }
        // otherwise
        //  check actually filled
        return ( !this.invertFillMask && this.cellFillMask[rowIndex,colIndex] ) || ( this.invertFillMask && !(this.cellFillMask[rowIndex,colIndex]) );
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
    public bool[] GetAdjacency(int rowIndex, int colIndex){
        return new bool[]{
            this.IsLocationFilled( rowIndex-1, colIndex-1 ),
            this.IsLocationFilled( rowIndex-1, colIndex   ),
            this.IsLocationFilled( rowIndex-1, colIndex+1 ),

            this.IsLocationFilled( rowIndex,   colIndex-1 ),
            this.IsLocationFilled( rowIndex,   colIndex   ),
            this.IsLocationFilled( rowIndex,   colIndex+1 ),

            this.IsLocationFilled( rowIndex+1, colIndex-1 ),
            this.IsLocationFilled( rowIndex+1, colIndex   ),
            this.IsLocationFilled( rowIndex+1, colIndex+1 )
        };
    }

    // ================================================================
    // ================================================================
}
