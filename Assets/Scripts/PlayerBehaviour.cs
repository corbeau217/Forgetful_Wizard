using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerBehaviour : MonoBehaviour
{
    public PlayerData playerData;
    public GameObject mousePlaneObj;
    // programatically determined
    public Rigidbody playerRigidBody;

    public float movementForceScalar;
    public float rotationSpeedScalar;

    public bool movementInputNotDetected = true;

    // this is our movement direction based on player movement
    public Vector3 movementForceDirection;
    // this is how much movement force we use in our player's movement direction
    // the calculated movement force for the player's rigid body to use
    public Vector3 currentMovementForce = Vector3.zero;

    public Vector3 currentFacingVector = Vector3.forward;
    public Quaternion quarternionFacing = Quaternion.identity;

    public Vector3 playerToMouse;
    public Vector3 mouseDirection;



    public GameObject spellProjectileParent;
    public GameObject spellProjectileBase;
    public bool attemptSpellUsage = false;
    public float spellProjectileForce = 100.0f;
    public float spellTimeout = 0.5f;
    public float spellGlobalCooldownRemaining = 0.0f;

    // ==========================================================================================
    // ==========================================================================================
    // ==========================================================================================

    public void HandleKeyboardInput(){
        // ========================== othogonal movement
        // ================================================================================

        // prepare movement vector
        Vector3 orthogonalMovement = Vector3.zero;

        // determine orthogonal movement vector

        if(Input.GetKey(this.playerData.moveKey_Up)){
            orthogonalMovement += Vector3.forward;
        }
        if(Input.GetKey(this.playerData.moveKey_Down)){
            orthogonalMovement += Vector3.back;
        }
        if(Input.GetKey(this.playerData.moveKey_Left)){
            orthogonalMovement += Vector3.left;
        }
        if(Input.GetKey(this.playerData.moveKey_Right)){
            orthogonalMovement += Vector3.right;
        }

        // check for no movement
        if( orthogonalMovement == Vector3.zero ){
            this.movementInputNotDetected = true;
        }
        else {
            this.movementInputNotDetected = false;
        }

        // remove extra diagonal speed and apply as our movement direction
        this.movementForceDirection = Vector3.Normalize(orthogonalMovement);

        // ================================================================================
    }
    public void HandleMouseInput(){
        // ---- raw mouse positioning
        // ----------------------------------------------------------
        
        // need to get the vector between us and the mouse location 
        this.playerToMouse = this.mousePlaneObj.transform.position - this.gameObject.transform.position;

        // prepare the normalized vector
        this.mouseDirection = Vector3.Normalize(playerToMouse);

        // ---- mouse input
        // ----------------------------------------------------------

        // detect primary
        if(Input.GetMouseButton(0)){
            this.attemptSpellUsage = true;
        }

        // ----------------------------------------------------------
    }

    // ==========================================================================================
    // ==========================================================================================
    // ==========================================================================================

    public void CalculatePlayerMovement(){
        // base movement with force applied
        this.currentMovementForce = this.movementForceDirection * this.movementForceScalar;

        // change our facing vector
        //      RotateTowards( current, target, radiansForRotation, magnitudeChange )
        this.currentFacingVector = Vector3.RotateTowards(this.currentFacingVector, this.mouseDirection, this.rotationSpeedScalar * Time.deltaTime, 0.0f);

        // rotate our player to face it
        this.quarternionFacing = Quaternion.LookRotation(this.currentFacingVector, Vector3.up);
    }

    public void ApplyMovementUpdate(){
        if(movementInputNotDetected){
            // increase drag to negate drifting
            this.playerRigidBody.drag = this.playerData.stationary_playerDrag;
            this.playerRigidBody.angularDrag = this.playerData.stationary_playerAngularDrag;
        }
        else {
            // set mass to movement mass
            this.playerRigidBody.mass = this.playerData.base_playerMass;
            // reduce drag so we can move
            this.playerRigidBody.drag = this.playerData.base_playerDrag;
            this.playerRigidBody.angularDrag = this.playerData.base_playerAngularDrag;
            // add our movement force
            playerRigidBody.AddForce(this.currentMovementForce, ForceMode.Impulse);
        }
        // provide move rotation
        playerRigidBody.MoveRotation(this.quarternionFacing);
    }


    public void HandleTimeouts(){
        // zzzz our spell firing
        this.spellGlobalCooldownRemaining = Mathf.Max(0.0f, this.spellGlobalCooldownRemaining - Time.deltaTime);
    }
    public void HandleSpellUsage(){
        // have spell use flag
        if(this.attemptSpellUsage){
            // check for cooldown over
            if(this.spellGlobalCooldownRemaining == 0.0f){
                // ...
                // prepare spawn
                Vector3 spawnLocation = this.gameObject.transform.position + this.currentFacingVector;
                // create one
                GameObject newProjectile = (GameObject)Instantiate(this.spellProjectileBase, spawnLocation, this.quarternionFacing, this.spellProjectileParent.transform);
                // make it move??
                newProjectile.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0.0f, 0.0f, this.spellProjectileForce));


                // snooze from spell
                this.spellGlobalCooldownRemaining = this.spellTimeout;
            }
            // remove the spell use flag
            this.attemptSpellUsage = false;
        }
    }

    // ==========================================================================================
    // ==========================================================================================
    // ==========================================================================================

    void FetchReferences(){
        this.playerRigidBody = this.GetComponent<Rigidbody>();
    }
    void PrepareWorkingFields(){
        this.movementForceDirection = Vector3.zero;
        this.playerToMouse = Vector3.zero;
        this.mouseDirection = Vector3.zero;
    }
    void LoadPlayerData(){
        this.movementForceScalar = this.playerData.base_movementForceScalar;
        this.rotationSpeedScalar = this.playerData.base_rotationSpeed;

        this.playerRigidBody.mass = this.playerData.base_playerMass;
        this.playerRigidBody.drag = this.playerData.base_playerDrag;
        this.playerRigidBody.angularDrag = this.playerData.base_playerAngularDrag;
    }

    // ==========================================================================================
    // ==========================================================================================
    // ==========================================================================================

    // Start is called before the first frame update
    void Start()
    {
        this.FetchReferences();
        this.PrepareWorkingFields();
        this.LoadPlayerData();
    }

    // Update is called once per frame
    void Update()
    {
        this.HandleKeyboardInput();
        this.HandleMouseInput();
        
        this.CalculatePlayerMovement();
        this.ApplyMovementUpdate();

        this.HandleTimeouts();
        this.HandleSpellUsage();
    }

    // ==========================================================================================
    // ==========================================================================================
    // ==========================================================================================
}
