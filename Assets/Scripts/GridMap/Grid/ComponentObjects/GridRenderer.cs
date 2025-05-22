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
    
    private SecondaryCellGenerator[,] secondaryCellGenerators;
    private CellRenderer[,] cellRenderers;
    private bool[,] primaryCellVacancies;
    private List<Vector2Int> availableInteractionSpawnLocations;

    private Vector2Int primaryDimensions;
    private Vector2Int secondaryDimensions;

    // ================================================================
    // ================================================================
    // ------------------------------------------------- event methods

    public void Generate(){
        // make grid
        this.StartGridGenerator();

        // fetches our cell data
        this.PrepareGeneratedCellData();

        // create render objects
        this.BuildSecondaryCellRenderers();

        // make our secondary cell meshes
        this.SpawnSecondaryCellMeshes();

        this.PrepareInteractableLocations();
    }

    // ================================================================
    // ================================================================
    // ----------------------------------------------- private methods

    private void StartGridGenerator(){
        this.availableInteractionSpawnLocations = new List<Vector2Int>();
        this.primaryDimensions = this.gridSettings.GetPrimaryDimensions();
        this.secondaryDimensions = this.gridSettings.GetSecondaryDimensions();
        // while constructing it processes the grid data
        this.gridGenerator = new GridGenerator(this.gridSettings);
    }
    private void PrepareGeneratedCellData(){
        this.secondaryCellGenerators = this.gridGenerator.GetSecondaryCellGenerators();
        this.primaryCellVacancies = this.gridGenerator.GetEmptyPrimaryCells();
    }
    private void BuildSecondaryCellRenderers(){
        this.cellRenderers = new CellRenderer[this.secondaryDimensions.y, this.secondaryDimensions.x];
        for (int rowIndex = 0; rowIndex < this.secondaryDimensions.y; rowIndex++)
        {
            for (int colIndex = 0; colIndex < this.secondaryDimensions.x; colIndex++)
            {
                this.BuildSecondaryCellRendererForLocation(rowIndex, colIndex);
            }
        }
    }
    private void BuildSecondaryCellRendererForLocation(int rowIndex, int colIndex){
        GameObject cellObject = (GameObject)Instantiate(
            this.cellRendererPrefab,
            this.cellContainer.transform
        );
        // stash it and dont use so it waits for the adding
        RectTransform rt = cellObject.AddComponent(typeof(RectTransform)) as RectTransform;
        // yep grab
        this.cellRenderers[rowIndex,colIndex] = cellObject.GetComponent<CellRenderer>();
        this.cellRenderers[rowIndex,colIndex].cellGenerator = this.secondaryCellGenerators[rowIndex,colIndex];
    }
    private void SpawnSecondaryCellMeshes(){
        for (int rowIndex = 0; rowIndex < this.secondaryDimensions.y; rowIndex++)
        {
            for (int colIndex = 0; colIndex < this.secondaryDimensions.x; colIndex++)
            {
                // tell them all to spawn their data
                this.cellRenderers[rowIndex,colIndex].Generate();
            }
        }
    }





    private void PrepareInteractableLocations(){
        // that's O(rows * cols * sizeof(Vector2Int)) worst case memory usage oof
        List<Vector2Int> availableLocations = new List<Vector2Int>(); 

        for (int rowIndex = 0; rowIndex < this.primaryDimensions.y; rowIndex++) {
            for (int colIndex = 0; colIndex < this.primaryDimensions.x; colIndex++) {
                // is vacant?
                if(this.primaryCellVacancies[rowIndex,colIndex]){
                    // stash it
                    availableLocations.Add(new Vector2Int(rowIndex, colIndex));
                }
            }
        }
        // find how many spawn locations we need to roll
        int remainingLocationsToRoll = Mathf.Min(
            this.gridSettings.GetMaximumRandomInteractablesSpawned(),
            availableLocations.Count
        );
        // select random locations from our list
        while (remainingLocationsToRoll > 0){
            int randomIndex = Random.Range(0,availableLocations.Count);
            // stash the location in our list
            this.availableInteractionSpawnLocations.Add( availableLocations[randomIndex] );
            // remove it from the available locations
            availableLocations.RemoveAt(randomIndex);

            // tick off one of our locations to roll
            remainingLocationsToRoll--;
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

    public Vector2Int GetPrimaryDimensions(){
        return this.primaryDimensions;
    }
    public Vector2Int GetSecondaryDimensions(){
        return this.secondaryDimensions;
    }

    // ================================================================
    // ================================================================
}
