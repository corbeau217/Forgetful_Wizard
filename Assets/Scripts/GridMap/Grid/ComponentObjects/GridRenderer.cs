using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// for grid manipulation
//  shhhh it's silly but duct tape
//  makes the projects stop floating
using UnityEngine.UI;

// this is for generating the tile room in the world, and should just be runtime logic
public class GridRenderer : MonoBehaviour
{
    // ================================================================
    // ================================================================
    // ------------------------------------------ private const fields

    // ================================================================
    // ================================================================
    // -------------------------------------------- public data fields

    public GridGeneratorSettings gridSettings;
    public GameObject cellRendererPrefab;
    public GameObject cellContainer;

    // ================================================================
    // ================================================================
    // ------------------------------------------- private data fields

    private GridGenerator gridGenerator;
    private CellOptionBase[,] bakedCells;
    private CellRenderer[,] cellRenderers;
    private Vector2Int primaryDimensions;
    private Vector2Int secondaryDimensions;

    // ================================================================
    // ================================================================
    // ------------------------------------------------- event methods

    // ================================================================
    // ================================================================
    // ----------------------------------------------- private methods

    private void StartGridGenerator(){
        this.gridGenerator = new GridGenerator(this.gridSettings);
        this.primaryDimensions = this.gridSettings.GetPrimaryDimensions();
        this.secondaryDimensions = this.gridSettings.GetSecondaryDimensions();
    }
    private void BakeCells(){
        this.bakedCells = this.gridGenerator.BakeCells();
    }
    private void SpawnCellRenderers(){
        this.cellRenderers = new CellRenderer[this.secondaryDimensions.y, this.secondaryDimensions.x];
        for (int rowIndex = 0; rowIndex < this.secondaryDimensions.y; rowIndex++)
        {
            for (int colIndex = 0; colIndex < this.secondaryDimensions.x; colIndex++)
            {
                this.SpawnCellRendererForLocation(rowIndex, colIndex);
            }
        }
    }
    private void SpawnCellRendererForLocation(int rowIndex, int colIndex){
        // this.cellRenderers[rowIndex,colIndex] = new  uhhhhhhh .....(this.bakedCells[rowIndex,colIndex])
        GameObject cellObject = (GameObject)Instantiate(
            this.cellRendererPrefab,
            this.cellContainer.transform
        );
        // stash it and dont use so it waits for the adding
        RectTransform rt = cellObject.AddComponent(typeof(RectTransform)) as RectTransform;
        // yep grab
        this.cellRenderers[rowIndex,colIndex] = cellObject.GetComponent<CellRenderer>();
        this.cellRenderers[rowIndex,colIndex].cellOption = bakedCells[rowIndex,colIndex];
    }
    private void StartCellGeneration(){
        for (int rowIndex = 0; rowIndex < this.secondaryDimensions.y; rowIndex++)
        {
            for (int colIndex = 0; colIndex < this.secondaryDimensions.x; colIndex++)
            {
                // tell them all to spawn their data
                this.cellRenderers[rowIndex,colIndex].Generate();
            }
        }
    }

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
    // ------------------------------------------------ public methods

    public void Generate(){
        this.StartGridGenerator();
        this.BakeCells();
        this.SpawnCellRenderers();
        this.StartCellGeneration();
    }
    public Vector2Int GetPrimaryDimensions(){
        return this.primaryDimensions;
    }
    public Vector2Int GetSecondaryDimensions(){
        return this.secondaryDimensions;
    }

    // ================================================================
    // ================================================================
}
