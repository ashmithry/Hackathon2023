
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Academies Hacks 2023

public class AIPathfinding : MonoBehaviour
{
    private NavMeshAgent nav;

    [SerializeField]
    private bool foundEnemy;
    private GameObject enemy;

    private float optimumDist;

    [SerializeField]
    private float maxScanDistance;

    private ShipCombat combat;

    [SerializeField]
    public Transform center;

    void Start()
    {
        optimumDist = Random.Range(3f, 5f);
        nav = GetComponent<NavMeshAgent>();
        combat = GetComponent<ShipCombat>();
    }

    void GoToEnemy()
    {
        Transform goal = enemy.transform;


        //Pathfind towards the closest side of the ship

        Vector3 r = goal.position - goal.right * optimumDist;
        Vector3 l = goal.position + goal.right * optimumDist;

        if (Mathf.Abs((transform.position - r).magnitude) < Mathf.Abs((transform.position - l).magnitude))
        {
            nav.destination = r;
        }
        else
        {
            nav.destination = l;
        }

        float bestAngle = goal.rotation.eulerAngles.y - transform.rotation.eulerAngles.y;

        float rotationalSpeed = bestAngle / 10;
        rotationalSpeed = Mathf.Min(rotationalSpeed, +1f);
        rotationalSpeed = Mathf.Max(rotationalSpeed, -1f);

        if (Mathf.Abs(bestAngle) < 2f || Mathf.Abs(bestAngle) > 178f)
        {
            combat.Shoot();
            return;
        }

        if (bestAngle > 2f)
        {
            transform.Rotate(0f, rotationalSpeed, 0f);
        }
        else if (bestAngle < -2f)
        {
            transform.Rotate(0f, -rotationalSpeed, 0f);
        }
    }

    public void GoToCenter()
    {
        nav.destination = center.position;
    }

    void Update()
    {
        Collider[] data = Physics.OverlapSphere(transform.position, maxScanDistance);

        //Now check if the data is in sight line by doing a raycast
        GameObject closestEnemy = null;
        float enemyDistance = maxScanDistance;
        foreach(Collider c in data)
        {
            GameObject g = c.gameObject;

            // Only pathfind towards other ships
            if(!g.CompareTag("Ship") || g == gameObject)
            {
                continue;
            }

            Vector3 disp = transform.position - g.transform.position;
            float magn = Mathf.Abs(disp.magnitude);
            if(magn < enemyDistance)
            {
                enemyDistance = magn;
                closestEnemy = g;
            }
        }

        if(closestEnemy != null)
        {
            foundEnemy = true;
            enemy = closestEnemy;
        } else
        {
            foundEnemy = false;
            enemy = null;
        }

        if(foundEnemy)
        {
            GoToEnemy();
        } else
        {
            GoToCenter();
        }
    }

    public GameObject GetEnemy()
    {
        return enemy;
    }

}
