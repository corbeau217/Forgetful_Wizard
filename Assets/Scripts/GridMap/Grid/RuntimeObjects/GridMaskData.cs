
using System.Collections;
using System.Collections.Generic;
using UnityEngine;






public class GridMaskData
{
    private GridMask gridMask;
    private GridLayerPriority priority;

    private CellOptionSet optionSet;

    private Color[] pixelColourData;
    private bool[,] cellFillValues;

    private Vector2Int primaryDimensions;
    private Color maskFillColour = Color.white;

    private const float maskTollerance = 0.5f;

    public GridMaskData(GridMask gridMask){
        this.gridMask = gridMask;
        this.priority = this.gridMask.priority;
        this.optionSet = this.gridMask.optionSet;
        this.pixelColourData = this.gridMask.GetPixels();
        this.primaryDimensions = this.gridMask.GetPrimaryDimensions();
        this.cellFillValues = new bool[this.primaryDimensions.y, this.primaryDimensions.x];

        this.Initialise();
    }
    
    // loading our cellFillValues from pixelColourData
    //  assumes that we've already loaded pixelColourData from texture
    public void Initialise(){
        // each row
        for(int rowIndex = 0; rowIndex < this.primaryDimensions.y; rowIndex++){
            // gather that rows bits, int is 32 bits but we ignore signed bit, left to right for columns
            for(int colIndex = 0; colIndex < this.primaryDimensions.x; colIndex++){
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
        int topRowIndex = this.primaryDimensions.y-1;
        // flipping row index to be bottom to top
        int rowIndexFlipped = topRowIndex-rowIndex;
        // row is number of 'primaryDimensions.x' of pixels
        return rowIndexFlipped*this.primaryDimensions.x + colIndex;
    }

    // turn both colours in to vectors and then compare their magnitudes
    //  returns true when there's no more than maskTollerance difference
    private bool ColourDesiredByLayer(Color colourToTry){
        Vector4 desiredColour = new Vector4( this.maskFillColour.r, this.maskFillColour.g, this.maskFillColour.b, this.maskFillColour.a );
        Vector4 testingColour = new Vector4(          colourToTry.r,          colourToTry.g,          colourToTry.b,          colourToTry.a );

        float magnitudeDifference = Mathf.Abs(desiredColour.magnitude - testingColour.magnitude);

        return ( (magnitudeDifference < maskTollerance));
    }

    public Vector2Int GetPrimaryDimensions(){
        return this.primaryDimensions;
    }

    // determine if a location should be labeled as filled
    public bool GetLocationIsFilled(int rowIndex, int colIndex){
        // handle errors
        //  not loaded / out of bounds
        if( (rowIndex < 0) || (colIndex < 0) || (rowIndex >= this.primaryDimensions.y) || (colIndex >= this.primaryDimensions.x) ) {
            return false;
        }
        // otherwise
        //  check actually filled
        return this.cellFillValues[rowIndex, colIndex];
    }

    public CellOptionSet GetCellOptionSet(){
        return this.optionSet;
    }
    public GridLayerPriority GetPriority(){
        return this.priority;
    }
}