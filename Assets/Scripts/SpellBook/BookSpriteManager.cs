using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSpriteManager : MonoBehaviour
{
    public BookSpriteMaskManager maskManager;
    public SpriteRenderer coverShape;
    public SpriteRenderer coverShading;
    public SpriteRenderer pages;
    public Color baseCoverColour = Color.white;
    public Color basePagesColour = Color.white;

    public float lightLevelCurrent = 1.0f;
    public Color currentCoverColor = Color.white;
    public Color currentPageShade = Color.white;

    private float lightLevelMaximum = 0.9f;
    private float lightLevelMinimum = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        // ...
    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateColor();
        // provide colour to sprite renderers
        this.coverShape.color = this.currentCoverColor;
        this.coverShading.color = this.currentCoverColor;
        this.pages.color = this.currentPageShade;
    }
    public void SetCoverColour(Color inputColour){
        this.baseCoverColour = inputColour;
    }
    public void SetLightLevel(float inputLightLevel){
        this.lightLevelCurrent = Mathf.Min( this.lightLevelMaximum, Mathf.Max(this.lightLevelMinimum, inputLightLevel));
    }
    private void UpdateColor(){
        this.currentCoverColor = this.GetLightAdjustedColour(this.baseCoverColour);
        this.currentPageShade = this.GetLightAdjustedColour(this.basePagesColour);
    }
    private Color GetLightAdjustedColour(Color inputColour){
        return new Color(
            (inputColour.r * this.lightLevelCurrent),
            (inputColour.g * this.lightLevelCurrent),
            (inputColour.b * this.lightLevelCurrent),
            inputColour.a
        );
    }
}
