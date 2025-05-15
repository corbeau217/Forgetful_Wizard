using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PassageMask", menuName = "ScriptableObjects/PassageMaskData", order = 1)]
public class PassageMaskData : ScriptableObject
{

    // ================================================================
    // ================================================================
    // -------------------------------------------- public data fields

    public CellType passageType;
    public RoomLayerMaskData roomMask;

    // top row to bottom row, left to right each row
    public bool[] filledRequired;
    public bool[] adjacencyRequired;
    public bool[] vacancyRequired;

    public float maskTollerance = 0.5f;

    public Texture2D filledMaskTexture;
    public Texture2D adjacencyTexture;
    public Texture2D vacancyTexture;

    // ================================================================
    // ================================================================
    // ------------------------------------------- private data fields

    // ================================================================
    // ================================================================
    // ------------------------------------------------- event methods

    public void Initialise( Color[] filledMask, Color[] adjacencyMask, Color[] vacancyMask ){
        // whadawegot
        // Debug.Log("initialising tile with filledMask["+filledMask.Length+"], adjacencyMask["+adjacencyMask.Length+"], vacancyMask["+vacancyMask.Length+"]");
        // make the things to use if they dont already exist
        this.filledRequired = ((this.filledRequired != null && this.filledRequired.Length == 9)? this.filledRequired : new bool[filledMask.Length]);
        this.adjacencyRequired = ((this.adjacencyRequired != null && this.adjacencyRequired.Length == 9)? this.adjacencyRequired : new bool[adjacencyMask.Length]);
        this.vacancyRequired = ((this.vacancyRequired != null && this.vacancyRequired.Length == 9)? this.vacancyRequired : new bool[vacancyMask.Length]);
        
        // make the textures
        this.filledMaskTexture = TextureFrom(filledMask);
        this.adjacencyTexture = TextureFrom(adjacencyMask);
        this.vacancyTexture = TextureFrom(vacancyMask);


        Vector4 fullVec = Vector4.one;
        // only have 9 pixels in the sprite mask
        for (int cellIndex = 0; cellIndex < 9; cellIndex++) {
            // cell index row major order starting from top left
            //  pixel index is row major order starting from bottom left
            int pixelIndex = cellIndexToPixelIndex(cellIndex);

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
    
    // ================================================================
    // ================================================================
    // ----------------------------------------------- private methods

    // converts tile to texture's pixel index
    //  tile index start from the top left in row major order
    //  texture pixel indices start bottom left also in row major order 

    int cellIndexToPixelIndex(int cellIndex){
        // return cellIndex;
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

    Texture2D TextureFrom(Color[] colourArray){
        Texture2D result = new Texture2D(3,3, TextureFormat.RGBA32, false, false );
        // make it pixelated
        result.filterMode = FilterMode.Point;
        int i = 0;
        for (int y = 0; y < 3; y++) {
            for (int x = 0; x < 3; x++) {
                result.SetPixel(x, y, colourArray[i++]);
            }
        }
        // apply the setting of the pixels
        result.Apply();


        return result;
    }

    // ================================================================
    // ================================================================
    // ----------------------------------------- public getter methods

    // ================================================================
    // ================================================================
}
