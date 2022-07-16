using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(SpellElement), menuName = "Spells/New Spell Element", order = 10)]
public class SpellAttack : ScriptableObject
{
    public string attackName;
    public float damageModifier;
}
