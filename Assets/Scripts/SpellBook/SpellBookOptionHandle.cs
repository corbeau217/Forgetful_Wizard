using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBookOptionHandle : MonoBehaviour
{
    public SpellBookToggle SpellBookToggle;
    public PlayerBehaviour playerInstance;
    public Camera cameraObject;
    public LayerMask rayLayerMask;

    private SpellBookOption selectedOption;

    private float clickTimeout = 0.4f;
    private float clickSnooze = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        this.selectedOption = null;
    }

    // Update is called once per frame
    void Update()
    {
        this.clickSnooze = Mathf.Max(0.0f, this.clickSnooze-Time.deltaTime);
        this.HandleInput();
        this.HandleSelectedOption();
    }
    private void HandleSelectedOption(){
        // when found something
        if(selectedOption!=null){
            this.playerInstance.boltSpell = this.selectedOption.spellOption;
            // clear the selection
            this.selectedOption = null;
            this.SpellBookToggle.ToggleMenu();
        }
    }

    private void HandleInput(){
        // not snoozing and have clicked?
        if (this.clickSnooze == 0 && Input.GetMouseButton(0)) {
            
            this.selectedOption = this.GetSelectedOption();

            this.clickSnooze = this.clickTimeout;
        }
    }


    private SpellBookOption GetSelectedOption(){
        RaycastHit hit;
        Ray ray = this.cameraObject.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, 50.0f, this.rayLayerMask)) {
            if(hit.collider!=null){
                return hit.collider.gameObject.GetComponent<SpellBookOption>();
            }
        }
        return null;
    }
}
