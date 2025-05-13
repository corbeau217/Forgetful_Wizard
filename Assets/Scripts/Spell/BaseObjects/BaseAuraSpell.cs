using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAuraSpell : BaseSpell
{
    public string[] effectedTagList = new string[]{"Player"};


    protected abstract List<GameObject> GetEffectedObjects(GameObject sourceObject, Vector3 castPoint, Quaternion forwardRotation);
    protected abstract void ApplyAuraTo(GameObject effectedObject, Vector3 castPoint, Quaternion forwardRotation);
    

    protected override void OnSpellCast(GameObject sourceObject, Vector3 castPoint, Quaternion forwardRotation, Transform spawnParentTransform){
        List<GameObject> effectedObjects = GetEffectedObjects(sourceObject, castPoint, forwardRotation);
        foreach (GameObject effected in effectedObjects)
        {
            this.ApplyAuraTo(effected, castPoint, forwardRotation);
        }
    }


    public bool AuraEffectsObject(GameObject objectToCheck){
        for (int index = 0; index < this.effectedTagList.Length; index++)
        {
            if(objectToCheck.tag == effectedTagList[index]){
                return true;
            }
        }
        return false;
    }
}
