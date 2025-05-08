using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerBehaviour : MonoBehaviour
{
    public PlayerData playerData;
    public GameObject mousePlaneObj;


    public Vector3 currentOrthogonalMovement;

    public Vector3 playerToMouse;
    public Vector3 mouseDirection;

    public Vector3 currentFacingVector = new Vector3(0.0f, 0.0f, 1.0f);
    public Quaternion quarternionFacing = Quaternion.identity;

    public float currentMovementSpeed;
    public float currentRotationSpeed;



    public GameObject spellProjectileParent;
    public GameObject spellProjectileBase;
    public float spellProjectileForce = 100.0f;
    public float spellTimeout = 0.5f;
    public float spellGlobalCooldownRemaining = 0.0f;

    public void HandleKeyboardInput(){
        // ========================== othogonal movement
        // ================================================================================

        // prepare movement vector
        Vector3 orthogonalMovement = new Vector3(0.0f, 0.0f, 0.0f);

        // determine orthogonal movement vector

        if(Input.GetKey(this.playerData.moveKey_Up)){
            orthogonalMovement += this.playerData.directionVector_UP;
        }
        if(Input.GetKey(this.playerData.moveKey_Down)){
            orthogonalMovement += this.playerData.directionVector_DOWN;
        }
        if(Input.GetKey(this.playerData.moveKey_Left)){
            orthogonalMovement += this.playerData.directionVector_LEFT;
        }
        if(Input.GetKey(this.playerData.moveKey_Right)){
            orthogonalMovement += this.playerData.directionVector_RIGHT;
        }

        // remove extra diagonal speed and apply as our movement direction
        this.currentOrthogonalMovement = Vector3.Normalize(orthogonalMovement);

        // ================================================================================
    }
    public void HandleMouseInput(){
        // need to get the vector between us and the mouse location 
        this.playerToMouse = this.mousePlaneObj.transform.position - this.gameObject.transform.position;

        // prepare the normalized vector
        this.mouseDirection = Vector3.Normalize(playerToMouse);
    }

    public void HandleOrthogonolMovement(){
        // prepare location
        Vector3 desiredLocation = this.gameObject.transform.position + this.currentOrthogonalMovement;

        // apply movement
        this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, desiredLocation, this.currentMovementSpeed * Time.deltaTime);
    }

    public void HandlePlayerRotation(){
        // change our facing vector
        //      RotateTowards( current, target, radiansForRotation, magnitudeChange )
        this.currentFacingVector = Vector3.RotateTowards(this.currentFacingVector, this.mouseDirection, this.currentRotationSpeed * Time.deltaTime, 0.0f);

        // rotate our player to face it
        this.quarternionFacing = Quaternion.LookRotation(this.currentFacingVector, Vector3.up);
        this.gameObject.transform.rotation = this.quarternionFacing;
    }

    public void HandleSpellTimeouts(){
        // zzzz our spell firing
        this.spellGlobalCooldownRemaining = Mathf.Max(0.0f, this.spellGlobalCooldownRemaining - Time.deltaTime);
    }
    public void PerformSpellAttack(){
        // prepare spawn
        Vector3 spawnLocation = this.gameObject.transform.position + this.currentFacingVector;
        // create one
        GameObject newProjectile = (GameObject)Instantiate(this.spellProjectileBase, spawnLocation, this.quarternionFacing, this.spellProjectileParent.transform);
        // make it move??
        newProjectile.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0.0f, 0.0f, this.spellProjectileForce));
    }
    public void HandleAttack(){
        // detect primary
        if(Input.GetMouseButton(0) && this.spellGlobalCooldownRemaining == 0.0f){
            this.PerformSpellAttack();

            // snooze from spell
            this.spellGlobalCooldownRemaining = this.spellTimeout;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        this.currentOrthogonalMovement = new Vector3(0.0f, 0.0f, 0.0f);
        this.playerToMouse = new Vector3(0.0f, 0.0f, 0.0f);
        this.mouseDirection = new Vector3(0.0f, 0.0f, 0.0f);

        this.currentMovementSpeed = this.playerData.base_movementSpeed;
        this.currentRotationSpeed = this.playerData.base_rotationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        this.HandleKeyboardInput();
        this.HandleMouseInput();
        this.HandleOrthogonolMovement();
        this.HandlePlayerRotation();
        this.HandleSpellTimeouts();
        this.HandleAttack();
    }
}
