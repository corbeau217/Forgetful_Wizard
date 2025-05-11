using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu(fileName = "Tile", menuName = "ScriptableObjects/TileData", order = 1)]
public class TileData : ScriptableObject
{   
    public GameObject TilePrefab;
    // top row to bottom row, left to right each row
    public bool[] filledRequired;
    public bool[] adjacencyRequired;
    public bool[] vacancyRequired;

    public TileType tileType;

    public float maskTollerance = 0.5f;

    public void Initialise( Color[] filledMask, Color[] adjacencyMask, Color[] vacancyMask ){
        // whadawegot
        // Debug.Log("initialising tile with filledMask["+filledMask.Length+"], adjacencyMask["+adjacencyMask.Length+"], vacancyMask["+vacancyMask.Length+"]");
        // make the things to use if they dont already exist
        this.filledRequired = ((this.filledRequired != null && this.filledRequired.Length == 9)? this.filledRequired : new bool[filledMask.Length]);
        this.adjacencyRequired = ((this.adjacencyRequired != null && this.adjacencyRequired.Length == 9)? this.adjacencyRequired : new bool[adjacencyMask.Length]);
        this.vacancyRequired = ((this.vacancyRequired != null && this.vacancyRequired.Length == 9)? this.vacancyRequired : new bool[vacancyMask.Length]);
        
        Vector4 fullVec = Vector4.one;
        // only have 9 pixels in the sprite mask
        for (int cellIndex = 0; cellIndex < 9; cellIndex++) {
            // cell index row major order starting from top left
            //  pixel index is row major order starting from bottom left
            int pixelIndex = cellToPixelIndex(cellIndex);

            // prepare the current pixel vectors to determine mask value
            Vector4 filledVec = new Vector4( 
                filledMask[ pixelIndex ].r, 
                filledMask[ pixelIndex ].g, 
                filledMask[ pixelIndex ].b, 
                filledMask[ pixelIndex ].a
            );
            Vector4 adjacencyVec = new Vector4( 
                adjacencyMask[ pixelIndex ].r, 
                adjacencyMask[ pixelIndex ].g, 
                adjacencyMask[ pixelIndex ].b, 
                adjacencyMask[ pixelIndex ].a
            );
            Vector4 vacancyVec = new Vector4( 
                vacancyMask[ pixelIndex ].r, 
                vacancyMask[ pixelIndex ].g, 
                vacancyMask[ pixelIndex ].b, 
                vacancyMask[ pixelIndex ].a
            );

            // find when it's similar to the mask
            this.filledRequired[cellIndex] = Mathf.Abs((filledVec.magnitude) - (fullVec.magnitude)) < this.maskTollerance; 
            this.adjacencyRequired[cellIndex] = Mathf.Abs((adjacencyVec.magnitude) - (fullVec.magnitude)) < this.maskTollerance; 
            this.vacancyRequired[cellIndex] = Mathf.Abs((vacancyVec.magnitude) - (fullVec.magnitude)) < this.maskTollerance; 
        }
    }


    // converts tile to texture's pixel index
    //  tile index start from the top left in row major order
    //  texture pixel indices start bottom left also in row major order 

    int cellToPixelIndex(int cellIndex){
        int columnCount = 3;
        int rowCount = 3;

        // extract column index, stays same
        int cellColIndex = cellIndex % columnCount;
        
        // extract cell row index
        int cellRowIndexTopToBottom = cellIndex / columnCount;

        // convert to pixel row index
        int bottomCellRow = (rowCount-1);
        int pixelRowIndex = bottomCellRow - cellRowIndexTopToBottom;

        // combine for the pixel index
        return (pixelRowIndex*columnCount)+cellColIndex;
    }
}
