using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CellOptionBase : ScriptableObject
{
    public CellType cellType;

    public abstract GameObject Generate(GameObject parent);
}