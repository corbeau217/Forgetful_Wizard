using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpell : ScriptableObject {
    public float baseSpellCooldown;
    protected float remainingSpellCooldown;

    // ----------- methods to override
    
    // activation
    protected abstract void OnSpellCast(GameObject sourceObject, Vector3 castPoint, Quaternion forwardRotation, Transform spawnParentTransform);


    // ----------- pre build methods

    public float GetRemainingCooldown(){ return this.remainingSpellCooldown; }

    public void CastSpell(GameObject sourceObject, Vector3 castPoint, Quaternion forwardRotation, Transform spawnParentTransform){
        this.remainingSpellCooldown = baseSpellCooldown;
        this.OnSpellCast(sourceObject, castPoint, forwardRotation, spawnParentTransform);
    }
    public void UpdateSpellCooldown(){
        this.remainingSpellCooldown = Mathf.Max(0.0f, this.remainingSpellCooldown-Time.deltaTime );
    }
}