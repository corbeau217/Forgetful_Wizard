using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{   
    // ...
    public KeyCode moveKey_Up = KeyCode.W;
    public KeyCode moveKey_Down = KeyCode.S;
    public KeyCode moveKey_Left = KeyCode.A;
    public KeyCode moveKey_Right = KeyCode.D;

    public float base_movementForceScalar = 1.11f;
    public float base_rotationSpeed = 20.0f;

    public float base_playerMass = 2.0f;
    public float base_playerDrag = 2.0f;
    public float base_playerAngularDrag = 1.0f;
    public float stationary_playerDrag = 15.0f;
    public float stationary_playerAngularDrag = 2.0f;
}
