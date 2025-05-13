using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBookToggle : MonoBehaviour
{
    public Camera cameraObject;
    public GameObject spellBookMenu;
    public bool showSpellBookMenu;

    public LayerMask rayLayerMask;


    private float clickTimeout = 0.4f;
    private float clickSnooze = 0.0f;

    private float rayMaxDistance = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        // ...
    }

    // Update is called once per frame
    void Update()
    {
        this.spellBookMenu.SetActive(this.showSpellBookMenu);
        this.HandleInput();
        this.clickSnooze = Mathf.Max(0.0f, this.clickSnooze-Time.deltaTime);
    }


    private void HandleInput(){
        // not snoozing and have clicked?
        if (this.clickSnooze == 0 && Input.GetMouseButton(0)) {
            this.HandleIfClickingSelf();
            this.clickSnooze = this.clickTimeout;
        }
    }
    private void HandleIfClickingSelf(){
        
        RaycastHit hit;
        Ray ray = this.cameraObject.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, this.rayMaxDistance, this.rayLayerMask)) {
            if(hit.collider!=null){
                if(hit.collider.gameObject == this.gameObject){
                    this.ToggleMenu();
                }
            }
        }
    }


    public void ToggleMenu(){
        this.showSpellBookMenu = !this.showSpellBookMenu;
    }

}
