using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FALL : MonoBehaviour
{
    public float fallSpeed;
    public float fallHeight;
    public GameObject die;
    public List<Material> dieFaces;
    public float rollTime;
    float targetY;
    public UnityEvent afterFall;

    // Start is called before the first frame update
    public void StartFall(Vector3 doorPosition)
    {
        targetY = transform.position.y;
        transform.position = new Vector3(transform.position.x, fallHeight, transform.position.z);
        foreach (MeshRenderer meshRenderer in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            meshRenderer.enabled = false;
        }
        StartCoroutine(StartRoll(doorPosition));
    }

    IEnumerator StartRoll(Vector3 doorPosition)
    {
        GameObject dieSpawned = Instantiate(die, doorPosition + Vector3.up * 4, Quaternion.identity);
        foreach (Transform child in dieSpawned.transform)
        {
            if (child.GetComponent<MeshRenderer>() == null)
            {
                continue;
            }
            int face = Random.Range(0, dieFaces.Count);
            child.GetComponent<MeshRenderer>().materials = new Material[1] { dieFaces[face] };
            dieFaces.RemoveAt(face);
        }
        yield return new WaitForSeconds(rollTime);
        afterFall.AddListener(delegate { Destroy(dieSpawned); });
        StartCoroutine(Fall());
    }

    IEnumerator Fall()
    {
        foreach (MeshRenderer meshRenderer in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            meshRenderer.enabled = true;
        }

        while (transform.position.y > targetY)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - fallSpeed * Time.deltaTime, transform.position.z);
            yield return null;
        }

        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);

        afterFall.Invoke();
    }
}
