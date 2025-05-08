using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // ...
    }

    // Update is called once per frame
    void Update()
    {
        // ...
    }

    void OnTriggerEnter (Collider other)
    {
        // see who we collided with
        if(other.gameObject.tag != "Walls"){
            Debug.Log ("projectile collided with " + other.gameObject.tag);
        }
        
        // otherwise cleanup projectile
        Destroy(this.gameObject, 0.0f);
    }
}
