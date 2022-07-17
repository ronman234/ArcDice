using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public SpellElement element;
    public SpellShape shape;
    public PlayerController playerController;

    public void CastSpell()
    {
        Debug.Log("Spell cast");

        playerController.UpdateAttack(shape.animationTriggerName);
        foreach (Enemy enemy in shape.GetEnemiesHit(playerController.gameObject))
        {
            Debug.Log("Hit: " + enemy);
        }
    }
}
