using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLocation : MonoBehaviour
{
    public Camera cameraObject;
    public GameObject playerObject;
    public LayerMask mouseLayerMask;

    public void UpdateMouseLocation(){
        RaycastHit hit;
        Ray ray = this.cameraObject.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, 1000.0f, this.mouseLayerMask)) {
            // Transform objectHit = hit.transform;
            Vector3 mouseLocation = hit.point;

            // move on to the same xz plane as the player
            mouseLocation.y = this.playerObject.transform.position.y;

            // apply to our location
            this.gameObject.transform.position = mouseLocation;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // ...
    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateMouseLocation();
    }
}
