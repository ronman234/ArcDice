using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Door : MonoBehaviour
{
    public GameObject ClosedState;
    public GameObject OpenState;
    public GameObject SealedState;

    [HideInInspector]
    public GameObject OwningRoom;
    [HideInInspector]
    public Door pairedDoor;
    [HideInInspector]
    public GameObject player;

    private Collider[] playerColliders;
    private Collider[] selfColliders;

    private bool wasColliding = false;

    // Start is called before the first frame update
    void Start()
    {
        playerColliders = player.GetComponentsInChildren<Collider>();
        selfColliders = GetComponentsInChildren<Collider>();
        
    }

    // Update is called once per frame
    void Update()
    {
        bool isColliding = CollidesWithPlayer();
        if (!wasColliding && isColliding)
        {
            HandleCollision();
        }
        wasColliding = isColliding;
    }

    void HandleCollision()
    {
        if (ClosedState.activeInHierarchy)
        {
            SetOpen();
            pairedDoor.gameObject.SetActive(false);
            pairedDoor.OwningRoom.SetActive(true);
        }
    }

    private bool CollidesWithPlayer()
    {
        foreach (Collider pc in playerColliders)
        {
            foreach (Collider sc in selfColliders)
            {
                if (pc.bounds.Intersects(sc.bounds))
                {
                    return true;
                }
            }
        }
        return false;
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
