using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new - Spell Shape", menuName = "Spells/Spell Shape", order = 10)]
public class SpellShape : ScriptableObject
{
    public string animationTriggerName;

    public float range;
    public float spreadAngle;
    public float angleBetweenRays;
    public float heightAbovePlayerPivot = 0.1f;

    public IEnumerable<Enemy> GetEnemiesHit(GameObject player)
    {
        Vector3 origin = player.transform.position + (Vector3.up * heightAbovePlayerPivot);

        foreach (Vector3 direction in RaysToCast(player))
        {
            Physics.Raycast(origin, direction, out RaycastHit hitInfo, range, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore);
            if (hitInfo.transform.TryGetComponent<Enemy>(out Enemy enemy))
            {
                yield return enemy;
            }
        }
    }

    public IEnumerable<Vector3> RaysToCast(GameObject player)
    {
        Vector3 center = player.transform.right;
        yield return center;

        for (float angle = 0; angle < spreadAngle / 2; angle += angleBetweenRays)
        {
            Quaternion rot = Quaternion.AngleAxis(angle, Vector3.up);
            yield return rot * center;
            yield return Quaternion.Inverse(rot) * center;
        }
    }

}
