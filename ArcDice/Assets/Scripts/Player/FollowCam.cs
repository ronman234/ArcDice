using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public PlayerController followTarget;
    public float followDistance;


    private Camera cam;

    private void Awake()
    {
        cam = gameObject.GetComponent<Camera>();
    }

    private void Update()
    {
        Vector3 currentPosition = transform.position;
        Vector3 newPosition = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y + 10, followTarget.transform.position.z - followDistance);
        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(45,0,0);
    }


}
