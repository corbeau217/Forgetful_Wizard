using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile", menuName = "ScriptableObjects/TileData", order = 1)]
public class TileData : ScriptableObject
{   
    public GameObject TilePrefab;
    // top row to bottom row, left to right each row
    public bool[] filledRequired;
    public bool[] vacancyRequired;
    public Texture2D fillMap;


    private bool adjacencyHashPrepared;
    private int adjacencyHash;
    private bool vacancyHashPrepared;
    private int vacancyHash;

    public int GetAdjacencyHash(){
        // already have?
        if(this.adjacencyHashPrepared){
            return this.adjacencyHash;
        }
        // calculate it
        else {
            this.adjacencyHash = GetAdjacencyHashFrom(this.filledRequired);
            this.adjacencyHashPrepared = true;
            // then give it
            return this.adjacencyHash;
        }
    }
    public int GetVacancyHash(){
        // already have?
        if(this.vacancyHashPrepared){
            return this.vacancyHash;
        }
        // calculate it
        else {
            this.vacancyHash = GetAdjacencyHashFrom(this.vacancyRequired);
            this.vacancyHashPrepared = true;
            // then give it
            return this.vacancyHash;
        }
    }

    public static int GetAdjacencyHashFrom(bool[] adjacentFilled){
        int result = 0;
        for(int i = 0; i < adjacentFilled.Length; i++){
            // add left shifted bit based on adjacency
            //  index is the power of 2 to add
            if(adjacentFilled[i]){
                result += (0x01 << i);
            }
        }
        // then give it
        return result;
    }
}
