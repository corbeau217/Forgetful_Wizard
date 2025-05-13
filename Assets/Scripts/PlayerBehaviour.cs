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

    public GameObject movementDirectionObject;


    public GameObject spellObjectParent;

    public BoltSpell boltSpell;
    public bool attemptProjectileSpell = false;

    public BaseSpell sprintSpell;
    public KeyCode sprintBurstKey = KeyCode.LeftShift;
    public bool attemptSprintBurst = false;


    public BaseSpell blinkSpell;
    public KeyCode blinkKey = KeyCode.Space;
    public bool attemptBlink = false;


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
        if(Input.GetKey(this.blinkKey)){
            this.attemptBlink = true;
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

        if(this.movementForceDirection.magnitude > 0.0f){
            this.movementDirectionObject.SetActive(true);
            Quaternion moveDirectionQuaternion = Quaternion.LookRotation(this.movementForceDirection);
            this.movementDirectionObject.transform.rotation = moveDirectionQuaternion;
        }
        else {
            this.movementDirectionObject.SetActive(false);
        }
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
        
        this.boltSpell.UpdateSpellCooldown();
        this.sprintSpell.UpdateSpellCooldown();
        this.blinkSpell.UpdateSpellCooldown();
    }
    public void HandleSpellUsage(){
        Quaternion movementQuaternion = (!this.movementInputNotDetected)? Quaternion.LookRotation(this.movementForceDirection, Vector3.up) : Quaternion.identity;
        // have spell use flag
        if(this.attemptProjectileSpell){
            if(this.boltSpell.GetRemainingCooldown() == 0.0f){
                // prepare spawn
                Vector3 spawnLocation = this.gameObject.transform.position + this.currentFacingVector;
                
                this.boltSpell.CastSpell(this.gameObject, spawnLocation, this.quarternionFacing, this.spellObjectParent.transform);
            }
            // remove the spell use flag
            this.attemptProjectileSpell = false;
        }
        if(this.attemptSprintBurst){
            if(this.sprintSpell.GetRemainingCooldown() == 0.0f){
                if(!this.movementInputNotDetected){
                    this.sprintSpell.CastSpell(this.gameObject, this.gameObject.transform.position, movementQuaternion, this.spellObjectParent.transform );
                }
            }

            // remove spell use flag
            this.attemptSprintBurst = false;
        }
        if(this.attemptBlink){
            if(this.blinkSpell.GetRemainingCooldown() == 0.0f){
                if(!this.movementInputNotDetected){
                    this.blinkSpell.CastSpell(this.gameObject, this.gameObject.transform.position, movementQuaternion, this.spellObjectParent.transform);
                }
                else {
                    this.blinkSpell.CastSpell(this.gameObject, this.gameObject.transform.position, this.quarternionFacing, this.spellObjectParent.transform);
                }
            }

            // remove spell use flag
            this.attemptBlink = false;
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
