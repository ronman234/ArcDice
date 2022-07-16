using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavMeshCreator : MonoBehaviour
{
    [SerializeField]
    GameObject navAgent;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(navAgent);
        }
    }
}
