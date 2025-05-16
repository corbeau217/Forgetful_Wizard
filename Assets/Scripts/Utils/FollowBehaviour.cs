using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehaviour : MonoBehaviour
{
    // game object to move towards
    public GameObject objectToFollow;
    // should be less than what you're following
    public float movementSpeed = 8.0f;
    // when you want to start speeding up
    public float preferredMaximumDistance = 2.0f;

    public void MovementUpdate(){
        // positions
        Vector3 currentPosition = this.gameObject.transform.position;
        Vector3 targetPosition = this.objectToFollow.transform.position;

        // to figure out if we speed up
        float distanceToTarget = Vector3.Distance(currentPosition, targetPosition);
        
        // including the calculation for acceleration based on how far we are
        float followSpeed = movementSpeed * (distanceToTarget / preferredMaximumDistance) * Time.deltaTime;
        
        this.gameObject.transform.position = Vector3.MoveTowards(currentPosition, targetPosition, followSpeed);
    }

    // Start is called before the first frame update
    void Start()
    {
        // ...
    }

    // Update is called once per frame
    void Update()
    {
        this.MovementUpdate();
    }
}
