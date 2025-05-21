using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CellOptionSet", menuName = "ScriptableObjects/TileMapping/CellOptionSet", order = 1)]
public class CellOptionSet : ScriptableObject
{
    public CellOptionBase[] options;
    public CellOptionBase defaultOption;


    /**

    # #|# .|. #|# #
    . #|# .|. .|. .
    ---+---+---+---
    . #|# .|. .|. .
    # .|. .|. .|. #
    ---+---+---+---
    # .|. .|. .|. #
    # #|# #|# .|. #
    ---+---+---+---
    # #|# #|# .|. #
    # #|# .|. #|# #

    */
    // convert primary grid filling to secondary grid tile option index
    //  indices hard coded to a 16 option count show above with
    //  top left as first, and bottom right as last in row major ordering
    public int GetIndexByMask(bool sameTopLeft_q0, bool sameTopRight_q1, bool sameBottomLeft_q2, bool sameBottomRight_q3){
        // these were retrieved through using a karnaugh map to convert from filling to tile index
        // filling index is in the order as our formal parameters, TL,TR,BL,BR
        // tile index is also in row major order starting from top left on the 'tile sheet'
        //  3D with a list, means we can rearrange the order, but it's best to stick to conventional ordering

        // probably could simplify these more with factorisation

        // (2^0) least significant bit
        int bit0 = (
            ( sameBottomRight_q3 && !sameTopLeft_q0) ||
            (!sameBottomRight_q3 &&  sameTopLeft_q0)
        )? 1 : 0;

        // (2^1)
        int bit1 = (
            (!sameBottomRight_q3 &&                       !sameTopRight_q1 && !sameTopLeft_q0) ||
            ( sameBottomRight_q3 && !sameBottomLeft_q2 && !sameTopRight_q1                   ) ||
            (!sameBottomRight_q3 && !sameBottomLeft_q2 &&  sameTopRight_q1                   ) ||
            ( sameBottomRight_q3 &&                        sameTopRight_q1 && !sameTopLeft_q0)
        )? 2 : 0;

        // (2^2)
        int bit2 = (
            (!sameBottomLeft_q2 && !sameTopRight_q1) ||
            ( sameBottomLeft_q2 &&  sameTopRight_q1)
        )? 4 : 0;
        
        // (2^3) most significant bit
        int bit3 = (
            (                        sameBottomLeft_q2 && !sameTopRight_q1 && !sameTopLeft_q0) ||
            ( sameBottomRight_q3                       && !sameTopRight_q1 &&  sameTopLeft_q0) ||
            (                        sameBottomLeft_q2 &&  sameTopRight_q1 &&  sameTopLeft_q0) ||
            ( sameBottomRight_q3                       &&  sameTopRight_q1 && !sameTopLeft_q0)
        )? 8 : 0;

        // usually bitwise or operation, but that may cause weird compatibility issues between architectures
        return bit3 + bit2 + bit1 + bit0;
    }

    public GameObject GenerateOption(GameObject optionParent, bool tl_filled, bool tr_filled, bool bl_filled, bool br_filled ){
        int optionIndex = this.GetIndexByMask( tl_filled, tr_filled, bl_filled, br_filled );

        if(optionIndex > this.options.Length){
            Debug.Log("CellOptionSet ("+this.name+") doesnt have the required tile!");
            return new GameObject(this.name+" selection error");
        }
        // safe to generate
        else {
            return this.options[optionIndex].Generate(optionParent);
        }
    }
}