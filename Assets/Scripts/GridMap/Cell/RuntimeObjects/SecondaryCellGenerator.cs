using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryCellGenerator
{
    private List<SecondaryCellLayer> cellLayers;
    private CellOptionBase noneLayersOption;
    private GridLayerPriority currentHighestPriority;
    private bool givenLayer = false;

    public SecondaryCellGenerator(CellOptionBase noneLayersOption){
        this.noneLayersOption = noneLayersOption;
        cellLayers = new List<SecondaryCellLayer>();
    }

    // only adds the layer if there was a new higher priority layer
    public void GiveLayer(SecondaryCellLayer newLayer){
        if(!this.currentHighestPriority.TakesPriorityOver(newLayer.priority)){
            this.currentHighestPriority = newLayer.priority;
        }
        this.cellLayers.Add(newLayer);
        this.givenLayer = true;
    }

    public GridLayerPriority GetHighestLayerPriority(){
        return this.currentHighestPriority;
    }

    // goes through each layer and gets the cell for that layer, adding it to the returns parent object
    public GameObject GenerateCellCollection(GameObject parentContainer){
        // prepare the container
        GameObject secondaryCellContainer = (parentContainer == null)? new GameObject("SecondaryCellContainer") : parentContainer;

        if(!givenLayer){
            Debug.Log("didnae give us a layer");
            this.noneLayersOption.Generate(secondaryCellContainer);
            secondaryCellContainer.name = "NoLayersGiven";
        }

        // TODO : sort layers in priority order before generating

        // layer fill tests
        bool tl_fillSoFar = false;
        bool tr_fillSoFar = false;
        bool bl_fillSoFar = false;
        bool br_fillSoFar = false;

        // make all our layers from last to first
        for (int layerIndex =  this.cellLayers.Count-1; layerIndex >= 0; layerIndex--) {
            // prepare fill checks
            bool tl_fillCurrent, tr_fillCurrent, bl_fillCurrent, br_fillCurrent;

            // add but save our fill information
            GameObject currentLayer = this.cellLayers[layerIndex].GenerateCell(secondaryCellContainer, out tl_fillCurrent, out tr_fillCurrent, out bl_fillCurrent, out br_fillCurrent);

            // update our fill tests
            tl_fillSoFar = tl_fillSoFar || tl_fillCurrent;
            tr_fillSoFar = tr_fillSoFar || tr_fillCurrent;
            bl_fillSoFar = bl_fillSoFar || bl_fillCurrent;
            br_fillSoFar = br_fillSoFar || br_fillCurrent;

            // when all were filled, stop adding
            if(tl_fillSoFar && tr_fillSoFar && bl_fillSoFar && br_fillSoFar){
                break;
            }
        }

        // done here
        return secondaryCellContainer;
    }
}