using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Map", menuName = "ScriptableObjects/MapData", order = 1)]
public class MapData : ScriptableObject
{

    // ================================================================
    // ================================================================
    // -------------------------------------------- public data fields

    public MapLayerData[] layers;

    public GameObject accessErrorTile;
    public Texture2D accessErrorTileSprite;
    public GameObject layerErrorTile;
    public Texture2D layerErrorTileSprite;
    public GameObject loadErrorTile;
    public Texture2D loadErrorTileSprite;

    // ================================================================
    // ================================================================
    // ------------------------------------------- private data fields

    private int[,] tileLayerIndices;
    

    private Vector2Int mapDimensions = new Vector2Int(0, 0);

    // for determining if safe to give information
    //  in public getters
    private bool loadedMapData;

    // ================================================================
    // ================================================================
    // ------------------------------------------------- event methods

    public void Initialise(){
        // have a layer mask to use
        if( this.layers != null && this.layers.Length > 0 ){
            for(int i = 0; i < this.layers.Length; i++){
                this.layers[i].Initialise();
            }
            // use first layer for map dimensions
            this.mapDimensions.y = this.layers[0].RowCount();
            this.mapDimensions.x = this.layers[0].ColCount();

            this.tileLayerIndices = new int[ this.mapDimensions.y, this.mapDimensions.x ];

            this.FindTileFilledLayers();

            // so the public getters are happy
            this.loadedMapData = true;
        }
        else {
            Debug.Log("Please provide map with layers!");
        }
    }
    
    // ================================================================
    // ================================================================
    // ----------------------------------------------- private methods

    // get all the layer indices that we're using
    private void FindTileFilledLayers(){
        // every row
        for(int rowIndex = 0; rowIndex < this.RowCount(); rowIndex++){
            // every column
            for(int colIndex = 0; colIndex < this.ColCount(); colIndex++){
                this.tileLayerIndices[ rowIndex, colIndex ] = this.FindLayerOfTile( rowIndex, colIndex );
            }
        }
    }

    // find the layer index
    //  return -1 when not filled
    private int FindLayerOfTile(int rowIndex, int colIndex){
        if(!this.loadedMapData){
            return -1;
        }
        // search our layers and find out if it's filled,
        //  starting with last layer
        for(int i = 0; i < this.layers.Length; i++){
            GameObject tileObject = this.layers[i].GetTileObject(rowIndex, colIndex);
            if(tileObject!=null){
                return i;
            }
        }
        // couldnt find a layer it was filled, use background layer
        return -1;
    }


    // ================================================================
    // ================================================================
    // ----------------------------------------- public getter methods

    public int RowCount(){
        return (this.loadedMapData)? this.mapDimensions.y : -1;
    }
    public int ColCount(){
        return (this.loadedMapData)? this.mapDimensions.x : -1;
    }


    public TileData GetTileData( int rowIndex, int colIndex ){
        // did we load it yet?
        if(this.loadedMapData){
            int layerIndex = this.tileLayerIndices[ rowIndex, colIndex ];
            // is there a layer to use?
            if(layerIndex >= 0){
                return this.layers[ layerIndex ].GetTileData(rowIndex, colIndex);
                
            }
        }
        // otherwise
        return null;
    }
    public GameObject GetTileObject( int rowIndex, int colIndex ){
        // did we load it yet?
        if(this.loadedMapData){
            int layerIndex = this.tileLayerIndices[ rowIndex, colIndex ];
            // is there a layer to use?
            if(layerIndex >= 0){
                GameObject tileToUse = this.layers[ layerIndex ].GetTileObject(rowIndex, colIndex);
                return (tileToUse!=null)? tileToUse : this.layerErrorTile;
            }
            else {
                return this.accessErrorTile;
            }
        }
        return this.loadErrorTile;
    }

    // return type???
    public Texture2D GetTileOverlayTexture( int rowIndex, int colIndex ){
        // did we load it yet?
        if(this.loadedMapData){
            int layerIndex = this.tileLayerIndices[ rowIndex, colIndex ];
            // is there a layer to use?
            if(layerIndex >= 0){
                // get the tile sprite/texture
                Texture2D tileToUse = this.layers[ layerIndex ].GetTileOverlay(rowIndex, colIndex);
                if(tileToUse!=null){
                    // have tile
                    return tileToUse;
                }
                else {
                    // layer error tile
                    return layerErrorTileSprite;
                }
            }
            else {
                // access error overlay 
                return accessErrorTileSprite;
            }
        }
        // load error overlay
        return loadErrorTileSprite;
    }

    // ================================================================
    // ================================================================
}
