using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilemap3D : MonoBehaviour
{
    public TileSetData tileSet;
    public MapLayoutData mapLayout;
    public bool fillOnStart;

    private TileData[,] tileGridData;
    private GameObject[,] tileObjects;

    private bool loadedTileData;

    public Texture2D fillTexture;

    private void LoadGridData(){
        this.mapLayout.Load();
        if(this.mapLayout.RowCount() > 0 && this.mapLayout.ColCount() > 0){
            // prepare the grid
            this.tileGridData = new TileData[this.mapLayout.RowCount(),this.mapLayout.ColCount()];
            this.tileObjects = new GameObject[this.mapLayout.RowCount(),this.mapLayout.ColCount()];

            // for each cell in the grid
            //   go and find the adjacency data
            //      then find a tile that fits that data
            for(int rowIndex = 0; rowIndex < this.mapLayout.RowCount(); rowIndex++){
                for(int colIndex = 0; colIndex < this.mapLayout.ColCount(); colIndex++){
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
            Quaternion tileRotation = Quaternion.identity;
            tileRotation.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
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
    // Start is called before the first frame update
    void Start()
    {
        Color[] pixelColours = fillTexture.GetPixels(0);
        for (int i = 0; i < pixelColours.Length; i++)
        {
            Debug.Log("pixel["+i+"]: "+pixelColours[i].ToString());
        }

        if(fillOnStart){
            this.LoadGridData();
            this.GenerateTileObjects();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ...
    }
}
