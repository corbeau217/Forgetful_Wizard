using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectileSpell : BaseSpell
{
    public GameObject projectileObject;

    public float spellProjectileForce = 1000.0f;

    public string[] collisionTags = new string[]{"Walls", "Tiles"};

    protected abstract void OnProjectileSpawn(Vector3 castPoint, Quaternion forwardRotation, Transform spawnParentTransform);
    protected abstract void OnProjectileHit(GameObject projectileObject, Collider hitObject);
   
    protected bool CollidesWithTag(string tagToCheck){
        for (int index = 0; index < this.collisionTags.Length; index++) {
            if(tagToCheck == collisionTags[index]){
                return true;
            }
        }
        return false;
    }
    protected GameObject SpawnProjectile(Vector3 castPoint, Quaternion forwardRotation, Transform spawnParentTransform){
        this.OnProjectileSpawn(castPoint, forwardRotation, spawnParentTransform);
        GameObject projectile = (GameObject)Instantiate(this.projectileObject, castPoint, forwardRotation, spawnParentTransform);
        ProjectileHitTrigger hitTrigger = projectile.GetComponent<ProjectileHitTrigger>();
        hitTrigger.SetSourceSpell(this);
        return projectile;
    }
    protected override void OnSpellCast(GameObject sourceObject, Vector3 castPoint, Quaternion forwardRotation, Transform spawnParentTransform){
        // create one
        GameObject newProjectile = this.SpawnProjectile(castPoint, forwardRotation, spawnParentTransform);

        // make it move
        newProjectile.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * this.spellProjectileForce);
    }

    public void HandleCollision(GameObject projectileObject, Collider hitObject){
        // check for can collide
        if( this.CollidesWithTag(hitObject.gameObject.tag) ){
            // deal with the collision
            this.OnProjectileHit(projectileObject,hitObject);
            // cleanup object
            Destroy(projectileObject, 0.0f);
        }
    }
}
