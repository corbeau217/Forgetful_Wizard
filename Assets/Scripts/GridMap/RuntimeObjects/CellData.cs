using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CellData {
    public CellOptionBase cellOption;
    public CellPlacementRules cellPlacementRules;

    public CellData(CellOptionBase cellOption, CellPlacementRules cellPlacementRules){
        this.cellOption = cellOption;
        this.cellPlacementRules = cellPlacementRules;
    }
}