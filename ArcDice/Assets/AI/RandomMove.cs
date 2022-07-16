using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class RandomMove : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent agent;
    float x, y, z;
    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        x = Random.Range(-25, 26);
        y = 3;
        z = Random.Range(-25, 26);
        agent.destination = new Vector3(x, y, z);
        Debug.Log(agent.destination);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(agent.destination);
        if (agent.remainingDistance < 5.0f)
        {
            //agent.destination = new Vector3(Random.Range(-25, 26), 0, Random.Range(-25, 26));
            x = Random.Range(-25, 26);
            y = 3;
            z = Random.Range(-25, 26);
            agent.destination = new Vector3(x, y, z);
            //Debug.Log(agent.destination);
        }
    }
}
