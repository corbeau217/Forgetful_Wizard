using System.Collections;
using System.Collections.Generic;
using UnityEngine;






public class CellGenerator
{
    private GridLayerPriority currentPriority;
    private CellSetData currentSet;

    public CellGenerator(){
        // .. cry in to the void, nothing to do 
    }

    public void GiveOption(GridLayerPriority newPriority, CellSetData setData){
        if(!this.currentPriority.TakesPriorityOver(newPriority)){
            this.currentPriority = newPriority;
            this.currentSet = setData;
        }
    }

    public CellSetData GetSetData(){
        return this.currentSet;
    }
}