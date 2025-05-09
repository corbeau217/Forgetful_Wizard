using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilemap3D : MonoBehaviour
{
    public TileSetData tileSet;
    public MapLayoutData mapLayout;
    private TileData[,] tileGridData;
    private GameObject[,] tileObjects;

    private bool loadedTileData;

    private void LoadGridData(){
        this.mapLayout.Load();
        if(this.mapLayout.RowCount() > 0 && this.mapLayout.ColCount() > 0){
            // prepare the grid
            this.tileGridData = new TileData[this.mapLayout.RowCount(),this.mapLayout.ColCount()];
            this.tileObjects = new GameObject[this.mapLayout.RowCount(),this.mapLayout.ColCount()];

            // TODO: process grid data
            // for each cell in the grid
            //   go and find the adjacency data
            //      then find a tile that fits that data
            for(int rowIndex = 0; rowIndex < this.mapLayout.RowCount(); rowIndex++){
                for(int colIndex = 0; colIndex < this.mapLayout.ColCount(); colIndex++){
                    // TODO handle swapping out for the required tile

                    // // for now we just check if it's filled
                    // if(this.mapLayout.IsLocationFilled(rowIndex,colIndex)){
                    //     this.tileGridData[rowIndex,colIndex] = this.tileSet.tileDataBlock;
                    // }
                    // else {
                    //     this.tileGridData[rowIndex,colIndex] = this.tileSet.tileDataEmpty;
                    // }
                    
                    // gather the adjacency information for the tile
                    bool[] adjacencyData = this.mapLayout.GetAdjacency(rowIndex,colIndex);
                    // try fetch the required tile and save it
                    this.tileGridData[rowIndex,colIndex] = this.tileSet.GetTileData(adjacencyData);
                }
            }

            // label as loaded
            this.loadedTileData = true;
        }
        else {
            Debug.Log("Tilemap aborting load");
        }
    }
    private void GenerateTileObjects(){
        if(this.loadedTileData){
            // every row
            for(int rowIndex = 0; rowIndex < this.mapLayout.RowCount(); rowIndex++){
                // every column
                for(int colIndex = 0; colIndex < this.mapLayout.ColCount(); colIndex++){
                    // generate tile for location
                    this.tileObjects[rowIndex,colIndex] = (GameObject)Instantiate(
                        // tile object
                        this.tileGridData[rowIndex,colIndex].TilePrefab,
                        // parent transform
                        this.gameObject.transform
                    );
                    // add rect transform to them
                    RectTransform rt = this.tileObjects[rowIndex,colIndex].AddComponent(typeof(RectTransform)) as RectTransform;
                    // then??
                }
            }
        }
        else {
            Debug.Log("Tilemap3D didnt have tile data to load");
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
        this.GenerateTileObjects();
    }

    // Update is called once per frame
    void Update()
    {
        // ...
    }
}
