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
       // transform.position = new Vector3(followTarget.transform.position.x - currentPosition, 0, followTarget.transform.position.z);
    }


}
