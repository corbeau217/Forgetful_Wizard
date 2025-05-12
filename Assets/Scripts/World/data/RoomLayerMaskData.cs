using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomLayerMask", menuName = "ScriptableObjects/RoomLayerMaskData", order = 1)]
public class RoomLayerMaskData : ScriptableObject
{
    // ================================================================
    // ================================================================
    // -------------------------------------------- public data fields

    // where the layer is loaded from
    public Texture2D layerFillMaskImage;
    // colour that this layer uses to fill
    public Color layerFillColour = Color.white;
    // when Color rgba is Vector4, what is the
    //  range of magnitude difference we accept
    public float maskTollerance;

    // ================================================================
    // ================================================================
    // ------------------------------------------- private data fields

    // retreived pixel colouring from the layer fill mask
    private Color[] pixelColourData;

    // the filling of cells
    private bool[,] cellFillValues;


    private Vector2Int layerDimensions = new Vector2Int(0, 0);

    // ================================================================
    // ================================================================
    // ------------------------------------------------- event methods
    
    public void Initialise(){
        this.layerDimensions.x = this.layerFillMaskImage.width;
        this.layerDimensions.y = this.layerFillMaskImage.height;

        // gather pixel information
        this.pixelColourData = this.layerFillMaskImage.GetPixels(0);
        // prepare fill mask array
        this.cellFillValues = new bool[ this.layerDimensions.y, this.layerDimensions.x ];

        // hand off for layer mask building
        this.InitialiseFillMask();
    }

    // ================================================================
    // ================================================================
    // ----------------------------------------------- private methods

    // loading our cellFillValues from pixelColourData
    //  assumes that we've already loaded pixelColourData from texture
    private void InitialiseFillMask(){
        // each row
        for(int rowIndex = 0; rowIndex < this.layerDimensions.y; rowIndex++){
            // gather that rows bits, int is 32 bits but we ignore signed bit, left to right for columns
            for(int colIndex = 0; colIndex < this.layerDimensions.x; colIndex++){
                // fetch colour
                Color cellColor = this.pixelColourData[CoordAsPixelIndex(rowIndex, colIndex)];

                // test fits our layer masking and save in our mask
                this.cellFillValues[rowIndex,colIndex] = this.ColourDesiredByLayer( cellColor );
            }
        }
    }

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

        return ( (magnitudeDifference < this.maskTollerance));
    }


    // ================================================================
    // ================================================================
    // ----------------------------------------- public getter methods

    public Vector2Int GetDimensions(){
        return this.layerDimensions;
    }

    // determine if a location should be labeled as filled
    public bool GetLocationIsFilled(int rowIndex, int colIndex){
        // handle errors
        //  not loaded / out of bounds
        if( (rowIndex < 0) || (colIndex < 0) || (rowIndex >= this.layerDimensions.y) || (colIndex >= this.layerDimensions.x) ) {
            return false;
        }
        // otherwise
        //  check actually filled
        return this.cellFillValues[rowIndex, colIndex];
    }
    public void SetLocationIsFilled(int rowIndex, int colIndex, bool value){
        this.cellFillValues[rowIndex, colIndex] = value;
    }


    // ================================================================
    // ================================================================
}
