using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject ClosedState;
    public GameObject OpenState;
    public GameObject SealedState;

    [HideInInspector]
    public GameObject OwningRoom;
    [HideInInspector]
    public Door pairedDoor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetClosed()
    {
        ClosedState.SetActive(true);
        OpenState.SetActive(false);
        SealedState.SetActive(false);
    }

    public void SetOpen()
    {
        ClosedState.SetActive(false);
        OpenState.SetActive(true);
        SealedState.SetActive(false);
    }

    public void SetSealed()
    {
        ClosedState.SetActive(false);
        OpenState.SetActive(false);
        SealedState.SetActive(true);
    }
}
