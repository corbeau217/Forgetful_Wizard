using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerBehaviour : MonoBehaviour
{
    // using left handed xyz axis
    public Vector3 DirectionVector_UP    = new Vector3( 0.0f, 0.0f,  1.0f);
    public Vector3 DirectionVector_DOWN  = new Vector3( 0.0f, 0.0f, -1.0f);
    public Vector3 DirectionVector_LEFT  = new Vector3(-1.0f, 0.0f,  0.0f);
    public Vector3 DirectionVector_RIGHT = new Vector3( 1.0f, 0.0f,  0.0f);

    // TODO : handle when left and right are applied and need to sqrt(2)/2 for the vector

    public KeyCode moveKeyUp = KeyCode.W;
    public KeyCode moveKeyDown = KeyCode.S;
    public KeyCode moveKeyLeft = KeyCode.A;
    public KeyCode moveKeyRight = KeyCode.D;

    public float currentMovementSpeed = 10.0f;

    public Vector3 currentOrthogonalMovement;

    public void HandleInput(){
        // ========================== othogonal movement
        // ================================================================================

        // prepare movement vector
        Vector3 OrthogonalMovement = new Vector3(0.0f, 0.0f, 0.0f);

        // determine orthogonal movement vector

        if(Input.GetKey(this.moveKeyUp)){
            OrthogonalMovement += DirectionVector_UP;
        }
        if(Input.GetKey(this.moveKeyDown)){
            OrthogonalMovement += DirectionVector_DOWN;
        }
        if(Input.GetKey(this.moveKeyLeft)){
            OrthogonalMovement += DirectionVector_LEFT;
        }
        if(Input.GetKey(this.moveKeyRight)){
            OrthogonalMovement += DirectionVector_RIGHT;
        }

        // remove extra diagonal speed and apply as our movement direction
        this.currentOrthogonalMovement = Vector3.Normalize(OrthogonalMovement);

        // ================================================================================
    }

    public void HandleOrthogonolMovement(){
        // prepare location
        Vector3 desiredLocation = this.gameObject.transform.position + this.currentOrthogonalMovement;

        // apply movement
        this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, desiredLocation, this.currentMovementSpeed * Time.deltaTime);
    }




    // Start is called before the first frame update
    void Start()
    {
        this.currentOrthogonalMovement = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        this.HandleInput();
        this.HandleOrthogonolMovement();
    }
}
