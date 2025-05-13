using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoltSpell", menuName = "ScriptableObjects/BoltSpell", order = 1)]
public class BoltSpell : BaseProjectileSpell
{
    protected override void OnProjectileSpawn(Vector3 castPoint, Quaternion forwardRotation, Transform parent){
        // zzz nothing to do for now
    }
    protected override void OnProjectileHit(GameObject projectileObject, Collider hitObject){
        // zzz nothing to do for now
    }
}
