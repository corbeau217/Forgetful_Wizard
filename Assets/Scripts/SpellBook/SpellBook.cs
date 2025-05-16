using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellBook", menuName = "ScriptableObjects/SpellBook", order = 1)]
public class SpellBook : ScriptableObject
{   
    public List<BaseSpell> spellOptions;
}
