
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject projectile;
    public Transform shootPoint;
    public bool enabled;

    public void Shoot()
    {
        if (!enabled) return;
        Instantiate(projectile, shootPoint.position, shootPoint.rotation, transform);
    }

    public Transform GetShootPoint()
    {
        return shootPoint;
    }
}
