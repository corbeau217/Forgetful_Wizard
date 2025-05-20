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
    private Vector2Int dimensions;

    // ================================================================
    // ================================================================
    // ------------------------------------------------- event methods

    // ================================================================
    // ================================================================
    // ----------------------------------------------- private methods

    private void StartGridGenerator(){
        this.gridGenerator = new GridGenerator(this.gridSettings);
        this.dimensions = this.gridGenerator.dimensions;
    }
    private void BakeCells(){
        this.bakedCells = this.gridGenerator.BakeCells();
    }
    private void SpawnCellRenderers(){
        this.cellRenderers = new CellRenderer[this.dimensions.y, this.dimensions.x];
        for (int rowIndex = 0; rowIndex < this.dimensions.y; rowIndex++)
        {
            for (int colIndex = 0; colIndex < this.dimensions.x; colIndex++)
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
        for (int rowIndex = 0; rowIndex < this.dimensions.y; rowIndex++)
        {
            for (int colIndex = 0; colIndex < this.dimensions.x; colIndex++)
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
    public Vector2Int GetDimensions(){
        return this.dimensions;
    }

    // ================================================================
    // ================================================================
}
