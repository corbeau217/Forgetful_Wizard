
using System.Collections;
using System.Collections.Generic;
using UnityEngine;





// lowest to highest
public enum GridLayerPriority {
    NoPrioritySet, // when not yet using
    // ============================================
    Room_None,
    Room_Fill,
    Room_Shelves,
    Room_Pillars,
    Room_Movement,
    Room_Passage,
    // ============================================
    Level_None,
    Level_Passage,
    // ============================================
}
public static class GridLayerPriorityUtils
{
    public static bool TakesPriorityOver(this GridLayerPriority selfReference, GridLayerPriority other){
        // if((int)selfReference >= (int)other){
        //     Debug.Log("greater happened");
        // }
        return (int)selfReference >= (int)other;
    }
}



