using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform Player;
    private NavMeshAgent Agent;
    public float UpdateRate = 0.1f;

    private Coroutine FollowCoroutine;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    public void StartChasing()
    {
        if(FollowCoroutine == null)
        {
            //Debug.Log("Move dammit");
            FollowCoroutine = StartCoroutine(FollowTarget());
        }
        else
        {
            Debug.Log("WHY YOU DO THIS");
        }
    }

    private IEnumerator FollowTarget()
    {
        WaitForSeconds Wait = new WaitForSeconds(UpdateRate);
    //    Debug.Log("Move dammit");
        while (gameObject.activeSelf)
        {
            //Agent.SetDestination(Player.transform.position);
            Agent.destination = Player.transform.position;
            yield return Wait;
        }
    }
}
