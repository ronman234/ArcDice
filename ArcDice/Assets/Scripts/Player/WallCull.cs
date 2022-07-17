using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCull : MonoBehaviour
{
    public Material xray;
    private GameObject cameraObj;
    private GameObject creature;
    private Vector3 direction;
    private float distance;
    private bool isActive = false;
    private static Dictionary<MeshRenderer, Material[]> affectedMeshes;
    private static int activeCreatures = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (affectedMeshes is null)
        {
            affectedMeshes = new Dictionary<MeshRenderer, Material[]>();
        }

        cameraObj = Camera.main.gameObject;
        creature = gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direction =  creature.transform.position - cameraObj.transform.position;
        distance = direction.magnitude;
        direction = direction.normalized;

        int layer = 8;
        int layerMask = 1 << layer;

        RaycastHit hit;
        Ray ray = new Ray(cameraObj.transform.position, direction);
        if (Physics.Raycast(ray, out hit, distance, layerMask, QueryTriggerInteraction.Ignore))
        {
            if (!isActive)
            {
                isActive = true;
                activeCreatures += 1;
            }

            Debug.DrawRay(cameraObj.transform.position, direction * distance);

            MeshRenderer mesh = hit.collider.GetComponent<MeshRenderer>();
            if (!affectedMeshes.ContainsKey(mesh))
            {
                affectedMeshes.Add(mesh, mesh.materials);
            }
            mesh.materials = new Material[1] { xray };

            Ray rayTwo = new Ray(hit.point, direction);
            if (Physics.Raycast(rayTwo, out hit, distance - hit.distance, layerMask, QueryTriggerInteraction.Ignore))
            {
                Debug.DrawRay(cameraObj.transform.position, direction * distance);

                mesh = hit.collider.GetComponent<MeshRenderer>();
                if (!affectedMeshes.ContainsKey(mesh))
                {
                    affectedMeshes.Add(mesh, mesh.materials);
                }
                mesh.materials = new Material[1] { xray };
            }
        }
        else
        {
            if (isActive)
            {
                isActive = false;
                activeCreatures -= 1;
            }

            if (activeCreatures <= 0)
            {
                foreach (KeyValuePair<MeshRenderer, Material[]> keyValuePair in affectedMeshes)
                {
                    if (keyValuePair.Key == null)
                    {
                        continue;
                    }

                    keyValuePair.Key.materials = keyValuePair.Value;
                }

                affectedMeshes.Clear();
            }
        }
    }
}
