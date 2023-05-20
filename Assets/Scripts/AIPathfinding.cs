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

    void Start()
    {
        optimumDist = Random.Range(2f, 5f);
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

        //Rotate in the same direction as the ship

        float bestAngle = transform.rotation.eulerAngles.y - goal.rotation.eulerAngles.y;

        //If the angle is in Quadrant 4 or 3, then make it negative

        bool neg = true;
        if(bestAngle > 180f)
        {
            bestAngle -= 360f;
        }

        if(Mathf.Abs(bestAngle) < 5f || Mathf.Abs(bestAngle) > 175f)
        {
            combat.Shoot();
            return;
        }

        if((bestAngle > 5 && !neg) || (bestAngle < -175 && neg))
        {
            transform.Rotate(0f, +0.25f, 0f);
        } 
        else if((bestAngle < -5 && neg) || (bestAngle > 175 && !neg))
        {
            transform.Rotate(0f, -0.25f, 0f);
        }
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
            if(!g.CompareTag("Ship"))
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
        }
    }

    public GameObject GetEnemy()
    {
        return enemy;
    }

}
