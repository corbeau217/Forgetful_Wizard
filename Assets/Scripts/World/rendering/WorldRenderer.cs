using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// for grid manipulation
//  shhhh it's silly but duct tape
//  makes the projects stop floating
using UnityEngine.UI;

public class WorldRenderer : MonoBehaviour
{
    // ================================================================
    // ================================================================
    // ------------------------------------------ private const fields

    // ...

    // ================================================================
    // ================================================================
    // -------------------------------------------- public data fields

    public WorldGenData worldGenData;
    public LevelRenderer levelRenderer;

    // ================================================================
    // ================================================================
    // ------------------------------------------- private data fields

    // ================================================================
    // ================================================================
    // ------------------------------------------------- event methods

    public void Generate(){
        Debug.Log("WorldRenderer.Generate() called");
        this.worldGenData.Initialise();
        levelRenderer.Generate(this.worldGenData);
    }

    // ================================================================
    // ================================================================
    // ----------------------------------------------- private methods

    // ================================================================
    // ================================================================
    // ------------------------------------------------- unity methods

    // Start is called before the first frame update
    void Start()
    {
        this.Generate();
    }

    // Update is called once per frame
    void Update()
    {
        // ...
    }

    // ================================================================
    // ================================================================
}
