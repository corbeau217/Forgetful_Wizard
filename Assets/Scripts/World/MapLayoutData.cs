using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapLayout", menuName = "ScriptableObjects/MapLayoutData", order = 1)]
public class MapLayoutData : ScriptableObject
{

    public uint[] rowBits;
    private bool[,] cellFilledGrid;
    private int rowCount;
    private int colCount;
    private bool loadedGridData;

    public void LoadFrom(uint[] rowBits){
        this.cellFilledGrid = new bool[rowBits.Length,32];
        // each row
        for(int rowIndex = 0; rowIndex < rowBits.Length; rowIndex++){
            // gather that rows bits, int is 32 bits, left to right for columns
            for(int colIndex = 0; colIndex < 32; colIndex++){
                // prepare mask as most significant bit as 1
                //      left to right, as we do with adjacency
                uint bitMask = (0x80000000 >> colIndex);
                // test if it's real
                if( (rowBits[rowIndex] & bitMask) > 0 ){
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
        if(this.rowBits!=null && this.rowBits.Length > 0){
            this.rowCount = rowBits.Length;
            this.colCount = 32;
            this.LoadFrom(this.rowBits);
            this.loadedGridData = true;
        }
        else {
            Debug.Log("No row data to load!");
        }
    }
    public bool[] GetAdjacency(int rowIndex, int colIndex){
        bool[] result = new bool[9];
        int resultIndex = 0;
        // rows
        for(int rowIndexOffset = 0; rowIndexOffset < 3; rowIndexOffset++){
            // columns
            for(int colIndexOffset = 0; colIndexOffset < 3; colIndexOffset++){
                // get if it's filled
                result[resultIndex++] = this.IsLocationFilled( rowIndex+rowIndexOffset, colIndex+colIndexOffset );
            }
        }
        // give it up
        return result;
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
