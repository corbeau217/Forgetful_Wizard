using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Map", menuName = "ScriptableObjects/MapData", order = 1)]
public class MapData : ScriptableObject
{

    // ================================================================
    // ================================================================
    // -------------------------------------------- public data fields

    MapLayerData[] layers;

    // ================================================================
    // ================================================================
    // ------------------------------------------- private data fields

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



    // ================================================================
    // ================================================================
    // ----------------------------------------- public getter methods

    public int RowCount(){
        return (this.loadedMapData)?this.mapDimensions.y:-1;
    }
    public int ColCount(){
        return (this.loadedMapData)?this.mapDimensions.x:-1;
    }

    public bool IsLocationFilled(int layerIndex, int rowIndex, int colIndex){
        if(!this.loadedMapData){
            return false;
        }
        if(layerIndex < 0 || layerIndex >= this.layers.Length){
            return false;
        }
        return this.layers[layerIndex].IsLocationFilled(rowIndex, colIndex);
    }

    // fetching the adjacency information for a given
    //  location within our grid
    // 
    // where CC is the current cell we want the following:
    // 
    //   [ TL ][ TC ][ TR ]
    //   [ CL ][ CC ][ CR ]
    //   [ BL ][ BC ][ BR ]
    // 
    // this is fetched where the top left is the first, and
    //  bottom right is the last in row major order
    public bool[] GetAdjacency(int layerIndex, int rowIndex, int colIndex){
        return new bool[]{
            this.IsLocationFilled( layerIndex, rowIndex-1, colIndex-1 ),
            this.IsLocationFilled( layerIndex, rowIndex-1, colIndex   ),
            this.IsLocationFilled( layerIndex, rowIndex-1, colIndex+1 ),

            this.IsLocationFilled( layerIndex, rowIndex,   colIndex-1 ),
            this.IsLocationFilled( layerIndex, rowIndex,   colIndex   ),
            this.IsLocationFilled( layerIndex, rowIndex,   colIndex+1 ),

            this.IsLocationFilled( layerIndex, rowIndex+1, colIndex-1 ),
            this.IsLocationFilled( layerIndex, rowIndex+1, colIndex   ),
            this.IsLocationFilled( layerIndex, rowIndex+1, colIndex+1 )
        };
    }

    // ================================================================
    // ================================================================
}
