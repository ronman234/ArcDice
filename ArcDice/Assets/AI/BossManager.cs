using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    float randomAttackInterval;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        randomAttackInterval = Random.Range(0, 5);
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(randomAttackInterval);

        DoAttack();

        randomAttackInterval = Random.Range(0, 5);
        StartCoroutine(Attack());
    }

    private void DoAttack()
    {
        animator.SetTrigger("Attack");
    }
}
