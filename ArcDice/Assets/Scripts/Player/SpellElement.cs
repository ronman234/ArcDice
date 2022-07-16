using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu (fileName = nameof(SpellElement), menuName = "Spells/New Spell Element", order = 10)]
public class SpellElement : ScriptableObject
{
    public string elementName;
}
