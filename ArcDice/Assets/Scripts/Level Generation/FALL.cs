using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FALL : MonoBehaviour
{
    public float fallSpeed;
    public float fallHeight;
    float targetY;
    public UnityEvent afterFall;

    // Start is called before the first frame update
    public void StartFall()
    {
        targetY = transform.position.y;
        transform.position = new Vector3(transform.position.x, fallHeight, transform.position.z);
        StartCoroutine(Fall());
    }

    IEnumerator Fall()
    {
        while (transform.position.y > targetY)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - fallSpeed * Time.deltaTime, transform.position.z);
            yield return null;
        }

        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);

        afterFall.Invoke();
    }
}
