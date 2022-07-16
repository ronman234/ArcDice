using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellShape : ScriptableObject
{
    public float range;
    public float spreadAngle;
    public float angleBetweenRays;
    public float heightAbovePlayerPivot;

    public List<Enemy> GetEnemiesHit(GameObject player)
    {
        List<Enemy> hits = new List<Enemy>();

        return hits;
    }

    public IEnumerable<Vector3> RaysToCast(GameObject player)
    {
        Vector3 center = player.transform.right;

        for (float angle = 0; angle <= spreadAngle / 2; angle += spreadAngle)
        {
            Quaternion rot = Quaternion.AngleAxis(angle, Vector3.up);
            yield return rot * center;
            yield return Quaternion.Inverse(rot) * center;
        }
    }

}
