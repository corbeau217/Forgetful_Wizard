using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ProjectileSpellData", order = 1)]
public class ProjectileSpellData : ScriptableObject
{
    public GameObject projectileObject;
    public float spellProjectileForce = 1000.0f;
    public float spellTimeout = 0.5f;

    public void ActivateSpell(Vector3 spawnLocation, Quaternion forwardRotation, Transform parentTransform){
        // create one
        GameObject newProjectile = (GameObject)Instantiate(this.projectileObject, spawnLocation, forwardRotation, parentTransform);

        // make it move
        newProjectile.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * this.spellProjectileForce);
    }
}
