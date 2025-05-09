using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapLayout", menuName = "ScriptableObjects/MapLayoutData", order = 1)]
public class MapLayoutData : ScriptableObject
{

    public int[] rowData;
    private bool[,] cellFilledGrid;
    private int rowCount;
    private int colCount;
    private bool loadedGridData;

    public void LoadFrom(int[] rowData){
        this.cellFilledGrid = new bool[rowData.Length,31];
        // each row
        for(int rowIndex = 0; rowIndex < rowData.Length; rowIndex++){
            // gather that rows bits, int is 32 bits but we ignore signed bit, left to right for columns
            for(int colIndex = 0; colIndex < 31; colIndex++){
                // prepare mask as most significant bit as 1
                //      left to right, as we do with adjacency
                int bitMask = (0x40000000 >> colIndex);
                // test if it's real
                if( (rowData[rowIndex] & bitMask) > 0 ){
                    this.cellFilledGrid[rowIndex,colIndex] = true;
                }
                // redundant, defaults false anyway
                // else {
                //     this.cellFilledGrid[rowIndex,colIndex] = false;
                // }
            }
        }
    }
    public void Load(){
        if(this.rowData!=null && this.rowData.Length > 0){
            this.rowCount = rowData.Length;
            this.colCount = 31;
            this.LoadFrom(this.rowData);
            this.loadedGridData = true;
        }
        else {
            Debug.Log("No row data to load!");
        }
        if(this.rowData.Length==0){
            this.rowData = new int[32];
        }
    }
    public bool[] GetAdjacency(int rowIndex, int colIndex){
        // bool[] result = new bool[9];
        // int resultIndex = 0;
        // // rows
        // for(int rowIndexOffset = -1; rowIndexOffset < 2; rowIndexOffset++){
        //     // columns
        //     for(int colIndexOffset = -1; colIndexOffset < 2; colIndexOffset++){
        //         // get if it's filled
        //         result[resultIndex++] = this.IsLocationFilled( rowIndex + rowIndexOffset, colIndex + colIndexOffset );
        //     }
        // }
        // // give it up
        // return result;
        return new bool[]{
            this.IsLocationFilled( rowIndex-1, colIndex-1 ),
            this.IsLocationFilled( rowIndex-1, colIndex   ),
            this.IsLocationFilled( rowIndex-1, colIndex+1 ),

            this.IsLocationFilled( rowIndex,   colIndex-1 ),
            this.IsLocationFilled( rowIndex,   colIndex   ),
            this.IsLocationFilled( rowIndex,   colIndex+1 ),

            this.IsLocationFilled( rowIndex+1, colIndex-1 ),
            this.IsLocationFilled( rowIndex+1, colIndex   ),
            this.IsLocationFilled( rowIndex+1, colIndex+1 )
        };
    }
    public bool IsLocationFilled(int rowIndex, int colIndex){
        // not loaded or out of bounds
        if((!this.loadedGridData) || (rowIndex < 0) || (colIndex < 0) || (rowIndex >= this.rowCount) || (colIndex >= this.colCount)){
            return true;
        }
        // safe indices, give data
        else {
            return this.cellFilledGrid[rowIndex,colIndex];
        }
    }

    public int RowCount(){
        return (this.loadedGridData)?this.rowCount:-1;
    }
    public int ColCount(){
        return (this.loadedGridData)?this.colCount:-1;
    }
}
