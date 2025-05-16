using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBookOption : MonoBehaviour
{
    public BaseSpell spellOption;
    // public Sprite spellIcon;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        // this.spriteRenderer.sprite = spellIcon;
    }

    // Update is called once per frame
    void Update()
    {
        // ....
    }

    public BaseSpell GetSpellOption(){
        return this.spellOption;
    }
}
