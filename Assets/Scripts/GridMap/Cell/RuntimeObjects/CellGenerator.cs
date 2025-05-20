using System.Collections;
using System.Collections.Generic;
using UnityEngine;






public class CellGenerator
{
    private GridLayerPriority currentPriority;
    private CellOptionSet currentSet;

    public CellGenerator(){
        // .. cry in to the void, nothing to do 
    }

    public void GiveOption(GridLayerPriority newPriority, CellOptionSet optionSet){
        if(!this.currentPriority.TakesPriorityOver(newPriority)){
            this.currentPriority = newPriority;
            this.currentSet = optionSet;
        }
    }

    public CellOptionSet GetSetData(){
        return this.currentSet;
    }
}