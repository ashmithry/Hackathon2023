
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject projectile;
    public Transform shootPoint;

    public void Shoot()
    {
        Instantiate(projectile, shootPoint.position, shootPoint.rotation);
    }
}
