using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SealDoor()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(!child.gameObject.activeSelf);
        }
    }
}
