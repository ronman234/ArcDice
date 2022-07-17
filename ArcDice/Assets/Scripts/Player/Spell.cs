using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    //public SpellElement element;
    public string spellElement;
    public SpellShape shape;
    public PlayerController playerController;
    public PlayerManager playerManager;


    public List<SpellShape> spellShapes;
    //private List<string> spellElements = new List<string> { "fire", "ice", "crystal", "wind", "holy", "unholy" };

    private float baseDamage;

    public void CastSpell(string element, SpellShape shape)
    {

        baseDamage = shape.damgeModifier * playerManager.playerLevel * 2;

        playerController.UpdateAttack(shape.animationTriggerName);
        foreach (Enemy enemy in shape.GetEnemiesHit(playerController.gameObject))
        {
            enemy.TakeDamage(baseDamage);
            switch (element)
            {
                case "fire":
                    StartCoroutine(damageOverTime(enemy, baseDamage, 5));
                    break;
                case "ice":
                    StartCoroutine(SlowMovement(enemy));
                    break;
                case "crystal":
                    enemy.TakeDamage(baseDamage * 0.5f);
                    break;
                case "wind":
                    enemy.GetComponentInChildren<Rigidbody>().AddForce(((enemy.transform.position - playerController.transform.position).normalized * playerManager.playerLevel + Vector3.up) * 100, ForceMode.Impulse);
                    break;
                case "holy":
                    
                    break;
                case "unholy":
                    if (enemy.Health <= 0)
                    {
                        playerManager.TakeDamage(-baseDamage);
                    }

                    break;

            }
        }
    }

    public IEnumerator damageOverTime(Enemy enemy, float damage, float timer)
    {
        float currentTimer = timer;
        while (currentTimer > 0)
        {
            currentTimer -= Time.deltaTime;
            enemy.TakeDamage(damage * Time.deltaTime / timer);
            yield return null;
        }
    }public IEnumerator healOverTime(PlayerManager player, float heal, float timer)
    {
        float currentTimer = timer;
        while (currentTimer > 0)
        {
            currentTimer -= Time.deltaTime;
            player.TakeDamage(-heal * Time.deltaTime / timer);
            yield return null;
        }
    }
    public IEnumerator SlowMovement(Enemy enemy)
    {
        enemy.Agent.speed /= 2;
        yield return new WaitForSeconds(5);
        enemy.Agent.speed *= 2;
    }
}
