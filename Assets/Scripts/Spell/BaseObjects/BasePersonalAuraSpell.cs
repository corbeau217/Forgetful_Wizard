using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePersonalAuraSpell : BaseAuraSpell
{
    protected override List<GameObject> GetEffectedObjects(GameObject sourceObject, Vector3 castPoint, Quaternion forwardRotation){
        // normally use AuraEffectsObject, but we know it's a personal spell so just give back the source object
        return new List<GameObject> { sourceObject };
    }
}
