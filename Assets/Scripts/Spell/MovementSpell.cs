using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementSpell", menuName = "ScriptableObjects/MovementSpell", order = 1)]
public class MovementSpell : BasePersonalAuraSpell
{
    public float movementForce = 100.0f;

    protected override void ApplyAuraTo(GameObject effectedObject, Vector3 castPoint, Quaternion forwardRotation){
        // gather our relative direction
        Vector3 forceDirection = forwardRotation * Vector3.forward;
        // do it
        effectedObject.GetComponent<Rigidbody>().AddForce((forceDirection * this.movementForce), ForceMode.Impulse);
    }
}
