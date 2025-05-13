using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlinkSpell", menuName = "ScriptableObjects/BlinkSpell", order = 1)]
public class BlinkSpell : BasePersonalAuraSpell
{
    public float blinkDistance = 10.0f;
    public float blinkObstacleBufferDistance = 0.5f;
    public LayerMask collidesWithMask;

    protected override void ApplyAuraTo(GameObject effectedObject, Vector3 castPoint, Quaternion forwardRotation){
        // LayerMask layerMask = LayerMask.GetMask("Walls", "Tiles");
        Rigidbody effectedRigidBody = effectedObject.GetComponent<Rigidbody>();
        RaycastHit hit;

        // where are we blinking from
        Vector3 blinkOrigin = effectedObject.transform.position;
        // so the ray goes from the torso kinda
        blinkOrigin.y += 0.5f;

        // prepare our direction and temp distance value
        Vector3 blinkDirection = forwardRotation * Vector3.forward;
        float currentBlinkDistance = this.blinkDistance;

        // Does the ray intersect any objects excluding the player layer
        if (SpellRay(blinkOrigin, blinkDirection, out hit, this.blinkDistance, collidesWithMask)) {
            currentBlinkDistance = hit.distance - this.blinkObstacleBufferDistance;
        }

        // vector used for blinking
        Vector3 blinkVector = blinkDirection * currentBlinkDistance;
        
        // apply to position
        effectedRigidBody.position = effectedRigidBody.position + blinkVector;
    }
}
