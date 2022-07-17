using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpellCreator
{
    public static List<string> defaultElements = new List<string> { "fire", "ice", "crystal", "wind", "holy", "unholy" };
    public static SpellShape RollAttackType(List<SpellShape> spellShapes)
    {
        return spellShapes[Random.Range(0, spellShapes.Count)];
    }
    public static string RollElementType()
    {
        return defaultElements[Random.Range(0, defaultElements.Count)];
    }
    public static string RollElementType(List<string> elements)
    {
        return elements[Random.Range(0, elements.Count)];
    }
    
}