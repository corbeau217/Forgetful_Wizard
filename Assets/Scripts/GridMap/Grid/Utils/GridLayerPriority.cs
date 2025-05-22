
using System.Collections;
using System.Collections.Generic;
using UnityEngine;





// lowest to highest
public enum GridLayerPriority {
    NoPrioritySet, // when not yet using
    // ============================================
    Room_None,
    Room_Fill,
    Room_Movement, // movement should happen first
    Room_Shelves,
    Room_Pillars,
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
    // test if we're using one of the subtractive layer priorities
    public static bool IsSubtractiveLayer(this GridLayerPriority selfReference){
        switch (selfReference){
            // normally fills a cell when the mask is filled
            default:
                return false;
            // these ones are inverted filling
            case GridLayerPriority.Room_Movement:
            case GridLayerPriority.Room_Passage:
            case GridLayerPriority.Level_Passage:
                return true;
        }
    }
}



