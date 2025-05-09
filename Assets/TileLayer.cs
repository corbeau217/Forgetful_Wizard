using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLayer : MonoBehaviour
{
    // ================================================================
    // ================================================================
    // -------------------------------------------- public data fields

    public TileSetData tileSet;
    public MapLayerMaskData mapLayerMask;
    public bool fillOnStart;

    // ================================================================
    // ================================================================
    // ------------------------------------------- private data fields

    private TileData[,] tileGridData;
    private GameObject[,] tileObjects;

    private bool loadedTileData;

    // ================================================================
    // ================================================================
    // ----------------------------------------------- private methods

    private void LoadLayerData(){
        if(this.mapLayerMask.RowCount() > 0 && this.mapLayerMask.ColCount() > 0){
            // prepare the grid
            this.tileGridData = new TileData[ this.mapLayerMask.RowCount(), this.mapLayerMask.ColCount() ];
            this.tileObjects = new GameObject[ this.mapLayerMask.RowCount(), this.mapLayerMask.ColCount() ];

            // for each cell in the grid
            //   go and find the adjacency data
            //      then find a tile that fits that data
            for(int rowIndex = 0; rowIndex < this.mapLayerMask.RowCount(); rowIndex++){
                for(int colIndex = 0; colIndex < this.mapLayerMask.ColCount(); colIndex++){
                    // gather the adjacency information for the tile
                    bool[] adjacencyData = this.mapLayerMask.GetAdjacency(rowIndex,colIndex);
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
            Quaternion tileRotation = Quaternion.identity;
            tileRotation.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            // every row
            for(int rowIndex = 0; rowIndex < this.mapLayerMask.RowCount(); rowIndex++){
                // every column
                for(int colIndex = 0; colIndex < this.mapLayerMask.ColCount(); colIndex++){
                    // generate tile for location
                    this.tileObjects[rowIndex,colIndex] = (GameObject)Instantiate(
                        // tile object
                        this.tileGridData[rowIndex,colIndex].TilePrefab,
                        // parent transform
                        this.gameObject.transform
                    );
                    // anti rotation
                    this.tileObjects[rowIndex,colIndex].transform.rotation = tileRotation;
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

    // ================================================================
    // ================================================================
    // ------------------------------------------------- unity methods

    // Start is called before the first frame update
    void Start()
    {
        if(fillOnStart){
            this.mapLayerMask.Initialise();
            this.LoadLayerData();
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
