using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilemap3D : MonoBehaviour
{
    public TileSetData tileSet;
    public MapLayoutData mapLayout;
    private TileData[,] tileGridData;

    private bool loadedTileData;

    private void LoadGridData(){
        this.mapLayout.Load();
        if(this.mapLayout.RowCount() > 0 && this.mapLayout.ColCount() > 0){
            // prepare the grid
            this.tileGridData = new TileData[this.mapLayout.RowCount(),this.mapLayout.ColCount()];

            // TODO: process grid data
            // for each cell in the grid
            //   go and find the adjacency data
            //      then find a tile that fits that data

            // label as loaded
            this.loadedTileData = true;
        }
        else {
            Debug.Log("Tilemap aborting load");
        }
    }
    private void GenerateTileObjects(){
        if(this.loadedTileData){
            // TODO create tiles based on supplied data
            Debug.Log("tiles to be generated");
        }
        else {
            Debug.Log("loading tiles failure");
        }
    }
    void DumpData(){
        // int lineIndex = 0;
        // Debug.Log("line["+(lineIndex++)+"]: "+(0b01111111111111111111111111111111) );
        // Debug.Log("line["+(lineIndex++)+"]: "+(0b01111111111111111111111111111111) );
        // Debug.Log("line["+(lineIndex++)+"]: "+(0b01111111111111111111111111111111) );
        // Debug.Log("line["+(lineIndex++)+"]: "+(0b01111111111110000011111111111111) );
        // Debug.Log("line["+(lineIndex++)+"]: "+(0b01111111111100000001111111111111) );
        // Debug.Log("line["+(lineIndex++)+"]: "+(0b01111111111100000001111111111111) );
        // Debug.Log("line["+(lineIndex++)+"]: "+(0b01111111111100000001111111111111) );
        // Debug.Log("line["+(lineIndex++)+"]: "+(0b01111111111110000011111111111111) );
        // Debug.Log("line["+(lineIndex++)+"]: "+(0b01111111111111010111111111111111) );
        // Debug.Log("line["+(lineIndex++)+"]: "+(0b01111100001110000011100001111111) );
        // Debug.Log("line["+(lineIndex++)+"]: "+(0b01111000000100000001000000111111) );
        // Debug.Log("line["+(lineIndex++)+"]: "+(0b01111000000000000000000000111111) );
        // Debug.Log("line["+(lineIndex++)+"]: "+(0b01111000000100000001000000111111) );
        // Debug.Log("line["+(lineIndex++)+"]: "+(0b01111100001110000011100001111111) );
        // Debug.Log("line["+(lineIndex++)+"]: "+(0b01111000000100000001000000111111) );
        // Debug.Log("line["+(lineIndex++)+"]: "+(0b01111000000100000001000000111111) );
        // Debug.Log("line["+(lineIndex++)+"]: "+(0b01111000000100000001000000111111) );
        // Debug.Log("line["+(lineIndex++)+"]: "+(0b01111100001110000011100001111111) );
        // Debug.Log("line["+(lineIndex++)+"]: "+(0b01111000000100000001000000111111) );
        // Debug.Log("line["+(lineIndex++)+"]: "+(0b01111000000000000000000000111111) );
        // Debug.Log("line["+(lineIndex++)+"]: "+(0b01111000000100000001000000111111) );
        // Debug.Log("line["+(lineIndex++)+"]: "+(0b01111100001110000011100001111111) );
        // Debug.Log("line["+(lineIndex++)+"]: "+(0b01111111111111101111111111111111) );
        // Debug.Log("line["+(lineIndex++)+"]: "+(0b01111111111111101111111111111111) );
        // Debug.Log("line["+(lineIndex++)+"]: "+(0b01111111111111000111111111111111) );
        // Debug.Log("line["+(lineIndex++)+"]: "+(0b01111111111111000111111111111111) );
        // Debug.Log("line["+(lineIndex++)+"]: "+(0b01111111111111000111111111111111) );
        // Debug.Log("line["+(lineIndex++)+"]: "+(0b01111111111111111111111111111111) );
        // Debug.Log("line["+(lineIndex++)+"]: "+(0b01111111111111111111111111111111) );
        // Debug.Log("line["+(lineIndex++)+"]: "+(0b01111111111111111111111111111111) );
        // Debug.Log("line["+(lineIndex++)+"]: "+(0b01111111111111111111111111111111) );
        // Debug.Log("line["+(lineIndex++)+"]: "+(0b01111111111111111111111111111111) );
    }
    // Start is called before the first frame update
    void Start()
    {
        this.DumpData();
        this.LoadGridData();
        // this.GenerateTileObjects();
    }

    // Update is called once per frame
    void Update()
    {
        // ...
    }
}
