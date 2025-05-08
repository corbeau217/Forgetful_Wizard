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


    public GameObject spellObjectParent;
    public ProjectileSpellData projectileSpell;

    public bool attemptProjectileSpell = false;

    public float projectileSpellCooldown = 0.0f;

    public KeyCode sprintBurstKey = KeyCode.LeftShift;
    public bool attemptSprintBurst = false;
    public float sprintBurstSpellCooldown = 0.0f;
    public float sprintBurstSpellCooldownOnUsageMinimum = 0.35f;
    public float sprintBurstSpellCooldownOnUsageMaximum = 0.6f;
    public float sprintBurstForce = 20.0f;

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

        if(Input.GetKey(this.sprintBurstKey)){
            this.attemptSprintBurst = true;
        }

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
            this.attemptProjectileSpell = true;
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
            this.playerRigidBody.AddForce(this.currentMovementForce, ForceMode.Impulse);
        }
        // provide move rotation
        this.playerRigidBody.MoveRotation(this.quarternionFacing);
    }

    // ==========================================================================================
    // ==========================================================================================
    // ==========================================================================================

    public void HandleTimeouts(){
        // zzzz our spell firing
        this.projectileSpellCooldown = Mathf.Max(0.0f, this.projectileSpellCooldown - Time.deltaTime);
        this.sprintBurstSpellCooldown = Mathf.Max(0.0f, this.sprintBurstSpellCooldown - Time.deltaTime);
    }
    public void HandleSpellUsage(){
        // have spell use flag
        if(this.attemptProjectileSpell){
            // check for cooldown over
            if(this.projectileSpellCooldown == 0.0f){
                // ...
                // prepare spawn
                Vector3 spawnLocation = this.gameObject.transform.position + this.currentFacingVector;

                this.projectileSpell.ActivateSpell( spawnLocation, this.quarternionFacing, this.spellObjectParent.transform );

                // snooze from spell
                this.projectileSpellCooldown = this.projectileSpell.spellTimeout;
            }
            // remove the spell use flag
            this.attemptProjectileSpell = false;
        }
        if(this.attemptSprintBurst){
            if(this.sprintBurstSpellCooldown == 0.0f){
                // add our sprint burst force in direction of movement
                this.playerRigidBody.AddForce((this.movementForceDirection * this.sprintBurstForce), ForceMode.Impulse);

                // snooze from spell
                this.sprintBurstSpellCooldown = Random.Range(this.sprintBurstSpellCooldownOnUsageMinimum, this.sprintBurstSpellCooldownOnUsageMaximum);
            }

            // remove spell use flag
            this.attemptSprintBurst = false;
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
