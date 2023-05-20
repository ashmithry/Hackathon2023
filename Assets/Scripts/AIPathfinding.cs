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

    [SerializeField]
    private float maxScanDistance;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    void GoToEnemy()
    {
        Transform goal = enemy.transform;

        //Pathfind towards the closest side of the ship

        Vector3 r = goal.position - goal.right * 2.5f;
        Vector3 l = goal.position + goal.right * 2.5f;

        if (Mathf.Abs((transform.position - r).magnitude) < Mathf.Abs((transform.position - l).magnitude))
        {
            nav.destination = r;
        }
        else
        {
            nav.destination = l;
        }

        if (Mathf.Abs((transform.position - nav.destination).magnitude) < 4f)
        {
            //canShoot = true;
        }

        //Rotate in the same direction as the ship

        float bestAngle = goal.rotation.eulerAngles.y - transform.rotation.eulerAngles.y;

        float rotationalSpeed = bestAngle / 10;
        rotationalSpeed = Mathf.Min(rotationalSpeed, 0.25f);

        if (bestAngle > 2f)
        {
            transform.Rotate(0f, rotationalSpeed, 0f);
        }
        else if (bestAngle < -2f)
        {
            transform.Rotate(0f, -rotationalSpeed, 0f);
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
            
            //Try and raycast the enemy - Deprecated
            /*
             * RaycastHit rcData;
            Vector3 angle = transform.rotation.eulerAngles - c.transform.rotation.eulerAngles;
            Debug.Log(angle + "");

            Debug.DrawRay(transform.position, angle);
            if (Physics.Raycast(transform.position, angle, out rcData, maxScanDistance))
            {
                //Check if the enemy was hit
                if(rcData.transform.gameObject != g)
                {
                    Debug.Log("Enemy Was Blocked By Something");
                    continue;
                }
            }
            else
            {
                Debug.Log("Ray Hit Nothing");
                continue;
            }
            */

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
            Debug.Log("Going to Enemy!");
            GoToEnemy();
        }
    }

    public GameObject GetEnemy()
    {
        return enemy;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(transform.position, maxScanDistance);
    }

}
