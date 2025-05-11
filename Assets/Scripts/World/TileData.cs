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
        Vector4 fullVec = Vector4.one;
        // only have 9 pixels in the sprite mask
        for (int pixelIndex = 0; pixelIndex < 9; pixelIndex++) {
            // prepare the current pixel vectors to determine mask value
            Vector4 filledVec = new Vector4( filledMask[pixelIndex].r, filledMask[pixelIndex].g, filledMask[pixelIndex].b, filledMask[pixelIndex].a );
            Vector4 adjacencyVec = new Vector4( adjacencyMask[pixelIndex].r, adjacencyMask[pixelIndex].g, adjacencyMask[pixelIndex].b, adjacencyMask[pixelIndex].a );
            Vector4 vacancyVec = new Vector4( vacancyMask[pixelIndex].r, vacancyMask[pixelIndex].g, vacancyMask[pixelIndex].b, vacancyMask[pixelIndex].a );

            // find when it's similar to the mask
            this.filledRequired[pixelIndex] = Mathf.Abs((filledVec.magnitude) - (fullVec.magnitude)) < this.maskTollerance; 
            this.adjacencyRequired[pixelIndex] = Mathf.Abs((adjacencyVec.magnitude) - (fullVec.magnitude)) < this.maskTollerance; 
            this.vacancyRequired[pixelIndex] = Mathf.Abs((vacancyVec.magnitude) - (fullVec.magnitude)) < this.maskTollerance; 
        }
    }
}
