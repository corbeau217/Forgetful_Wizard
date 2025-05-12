using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OverlayMode {
    Fill,
    Adjacency,
    Vacancy
}
static class OverlayModeMethods {
    public static OverlayMode Prev(this OverlayMode mode){
        switch (mode) {
            default:
            case OverlayMode.Fill:
                return OverlayMode.Vacancy;
            case OverlayMode.Adjacency:
                return OverlayMode.Fill;
            case OverlayMode.Vacancy:
                return OverlayMode.Adjacency;
        }
    }
    public static OverlayMode Next(this OverlayMode mode){
        switch (mode) {
            default:
            case OverlayMode.Fill:
                return OverlayMode.Adjacency;
            case OverlayMode.Adjacency:
                return OverlayMode.Vacancy;
            case OverlayMode.Vacancy:
                return OverlayMode.Fill;
        }
    }
}


public class TileOverlayObject : MonoBehaviour
{
    private const float OVERLAY_INPUT_COOLDOWN_TIME = 0.2f;

    public SpriteRenderer fillOverlayRenderer;
    public SpriteRenderer adjacencyOverlayRenderer;
    public SpriteRenderer vacancyOverlayRenderer;

    public KeyCode cyclePrevMode = KeyCode.LeftBracket;
    public KeyCode cycleNextMode = KeyCode.RightBracket;
    public KeyCode overlayToggle = KeyCode.Backslash;

    public float textureWidth = 3.0f;
    public float textureHeight = 3.0f;

    public bool showOverlay = false;

    private Texture2D fillTexture;
    private Texture2D adjacencyTexture;
    private Texture2D vacancyTexture;

    private Sprite fillSprite;
    private Sprite adjacencySprite;
    private Sprite vacancySprite;

    private Color fillColour;
    private Color adjacencyColour;
    private Color vacancyColour;

    private OverlayMode currentOverlayMode = OverlayMode.Fill;

    private float overlayInputCooldownRemaining = 0.0f;

    public void Initialise(Texture2D fillTextureToUse, Texture2D adjacencyTextureToUse, Texture2D vacancyTextureToUse, float overlayAlpha){
        this.fillColour = new Color(1.0f, 1.0f, 1.0f, overlayAlpha);
        this.adjacencyColour = new Color(0.4f, 0.4f, 1.0f, overlayAlpha);
        this.vacancyColour = new Color(0.0f, 1.0f, 0.0f, overlayAlpha);

        // ---- save textures

        this.fillTexture = fillTextureToUse;
        this.adjacencyTexture = adjacencyTextureToUse;
        this.vacancyTexture = vacancyTextureToUse;

        // ---- make sprites

        this.fillSprite = Sprite.Create(
            fillTexture, new Rect(0, 0, fillTexture.width, fillTexture.height), new Vector2(0.0f, 0.0f)
        );
        this.adjacencySprite = Sprite.Create(
            adjacencyTexture, new Rect(0, 0, adjacencyTexture.width, adjacencyTexture.height), new Vector2(0.0f, 0.0f)
        );
        this.vacancySprite = Sprite.Create(
            vacancyTexture, new Rect(0, 0, vacancyTexture.width, vacancyTexture.height), new Vector2(0.0f, 0.0f)
        );

        // ---- give sprites to renderers

        this.fillOverlayRenderer.sprite = fillSprite;
        this.adjacencyOverlayRenderer.sprite = adjacencySprite;
        this.vacancyOverlayRenderer.sprite = vacancySprite;

        // ---- assign the colour of overlays

        this.fillOverlayRenderer.color = this.fillColour;
        this.adjacencyOverlayRenderer.color = this.adjacencyColour;
        this.vacancyOverlayRenderer.color = this.vacancyColour;

    }

    private void HandleModeSpriteVisibility(){
        switch (currentOverlayMode) {
            default:
            case OverlayMode.Fill:
                fillOverlayRenderer.enabled = this.showOverlay;
                adjacencyOverlayRenderer.enabled = false;
                vacancyOverlayRenderer.enabled = false;
                break;
            case OverlayMode.Adjacency:
                fillOverlayRenderer.enabled = false;
                adjacencyOverlayRenderer.enabled = this.showOverlay;
                vacancyOverlayRenderer.enabled = false;
                break;
            case OverlayMode.Vacancy:
                fillOverlayRenderer.enabled = false;
                adjacencyOverlayRenderer.enabled = false;
                vacancyOverlayRenderer.enabled = this.showOverlay;
                break;
        }
    }
    private void Snore(){
        this.overlayInputCooldownRemaining = Mathf.Max(0.0f, this.overlayInputCooldownRemaining - Time.deltaTime);
    }
    private void HandleInput(){
        // no cooldown remaining
        if( this.overlayInputCooldownRemaining == 0.0f){
            if(  Input.GetKey(this.cyclePrevMode)){
                this.currentOverlayMode = this.currentOverlayMode.Prev();
                this.overlayInputCooldownRemaining = OVERLAY_INPUT_COOLDOWN_TIME;
            }
            if(  Input.GetKey(this.cycleNextMode)){
                this.currentOverlayMode = this.currentOverlayMode.Next();
                this.overlayInputCooldownRemaining = OVERLAY_INPUT_COOLDOWN_TIME;
            }
            if(  Input.GetKey(this.overlayToggle)){
                this.showOverlay = !this.showOverlay;
                this.overlayInputCooldownRemaining = OVERLAY_INPUT_COOLDOWN_TIME;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // ...
    }

    // Update is called once per frame
    void Update()
    {
        this.HandleModeSpriteVisibility();
        this.Snore();
        this.HandleInput();
    }
}
