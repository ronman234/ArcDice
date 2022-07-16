using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public PlayerController followTarget;
    public float followDistanceZ = 3.5f;
    public float followDistanceY = 15f;
    public float yaw = 70;

    private Camera cam;

    private void Awake()
    {
        cam = gameObject.GetComponent<Camera>();
    }

    private void Update()
    {
        Vector3 currentPosition = transform.position;
        Vector3 newPosition = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y + followDistanceY, followTarget.transform.position.z - followDistanceZ);
        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(yaw,0,0);
    }


}
