using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    // using left handed xyz axis
    public Vector3 directionVector_UP    = new Vector3( 0.0f, 0.0f,  1.0f);
    public Vector3 directionVector_DOWN  = new Vector3( 0.0f, 0.0f, -1.0f);
    public Vector3 directionVector_LEFT  = new Vector3(-1.0f, 0.0f,  0.0f);
    public Vector3 directionVector_RIGHT = new Vector3( 1.0f, 0.0f,  0.0f);
    
    // ...
    public KeyCode moveKey_Up = KeyCode.W;
    public KeyCode moveKey_Down = KeyCode.S;
    public KeyCode moveKey_Left = KeyCode.A;
    public KeyCode moveKey_Right = KeyCode.D;

    public float base_movementSpeed = 10.0f;
    public float base_rotationSpeed = 8.0f;
}
