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
            this.cellLayers.Add(newLayer);
            this.givenLayer = true;
        }
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

        // make all our layers generate their object and include it
        for (int layerIndex = 0; layerIndex < this.cellLayers.Count; layerIndex++) {
            GameObject currentLayer = this.cellLayers[layerIndex].GenerateCell(secondaryCellContainer);
        }

        // done here
        return secondaryCellContainer;
    }
}