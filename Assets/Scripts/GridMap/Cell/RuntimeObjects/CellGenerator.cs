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
            // Debug.Log("switcheroo happened");
            this.currentPriority = newPriority;
            this.currentSet = optionSet;
        }
        // else {
        //     Debug.Log("im better than u");
        // }
    }

    public CellOptionSet GetOptionSet(){
        return this.currentSet;
    }
    public GridLayerPriority GetLayerPriority(){
        return this.currentPriority;
    }
    public bool MatchesFillWith(CellGenerator other){
        if(other == null){
            return false;
        }
        CellOptionSet otherSet = other.GetOptionSet();
        GridLayerPriority otherPriority = other.GetLayerPriority();
        return (
            (this.currentSet != null && other.GetOptionSet() != null) &&
            (this.currentSet == otherSet && this.currentPriority == otherPriority)
        );
    }
}