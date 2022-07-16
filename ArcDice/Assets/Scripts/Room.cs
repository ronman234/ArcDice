using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [HideInInspector]
    public List<Door> doors;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCreation()
    {
        doors = new List<Door>(GetComponentsInChildren<Door>());
        foreach(Door d in doors)
        {
            d.OwningRoom = gameObject;
        }
    }
}
