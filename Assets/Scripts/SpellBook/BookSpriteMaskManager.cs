using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BookSpriteMaskMode {
    None,
    Full,
    FullMargined,
    Left,
    LeftMargined,
    Right,
    RightMargined
}
public static class BookSpriteMaskModeUtils {
    public static BookSpriteMaskMode ToggleMargin(this BookSpriteMaskMode selfReference){
        switch (selfReference) {
            default:
            case BookSpriteMaskMode.Full:
                return BookSpriteMaskMode.FullMargined;
            case BookSpriteMaskMode.FullMargined:
                return BookSpriteMaskMode.Full;
            case BookSpriteMaskMode.Left:
                return BookSpriteMaskMode.LeftMargined;
            case BookSpriteMaskMode.LeftMargined:
                return BookSpriteMaskMode.Left;
            case BookSpriteMaskMode.Right:
                return BookSpriteMaskMode.RightMargined;
            case BookSpriteMaskMode.RightMargined:
                return BookSpriteMaskMode.Right;
        }
    }
}


public class BookSpriteMaskManager : MonoBehaviour
{
    public SpriteMask full;
    public SpriteMask fullMargined;
    public SpriteMask left;
    public SpriteMask leftMargined;
    public SpriteMask right;
    public SpriteMask rightMargined;

    public BookSpriteMaskMode mode = BookSpriteMaskMode.Full;

    // Start is called before the first frame update
    void Start()
    {
        // ...
    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateActiveMasks();
    }

    public void UpdateActiveMasks(){
        // have them determine their enabled status when their mode is the current mode 

        if(           full != null ){           full.enabled = ( this.mode == BookSpriteMaskMode.Full          ); }
        if(   fullMargined != null ){   fullMargined.enabled = ( this.mode == BookSpriteMaskMode.FullMargined  ); }
        if(           left != null ){           left.enabled = ( this.mode == BookSpriteMaskMode.Left          ); }
        if(   leftMargined != null ){   leftMargined.enabled = ( this.mode == BookSpriteMaskMode.LeftMargined  ); }
        if(          right != null ){          right.enabled = ( this.mode == BookSpriteMaskMode.Right         ); }
        if(  rightMargined != null ){  rightMargined.enabled = ( this.mode == BookSpriteMaskMode.RightMargined ); }
    }
}
