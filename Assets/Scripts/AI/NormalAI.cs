
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Academies Hacks 2023
//Normal AI

public class NormalAI : MonoBehaviour
{
    protected NavMeshAgent nav;
    protected GameObject enemy;
    protected ShipCombat combat;
    protected AIDifficulty difficulty;

    protected bool foundEnemy;

    public Transform center;

    public float startTimer = 4.0f;

    void Start()
    {
        if (AIDifficulty.difficulties == null)
        {
            AIDifficulty.Init(); 
        }

        difficulty = AIDifficulty.difficulties[(int)Random.Range(0, 4)];
        nav = GetComponent<NavMeshAgent>();
        nav.acceleration = difficulty.movementVelocity;
        combat = GetComponent<ShipCombat>();
        center = GameObject.Find("Center").transform;
    }

    protected virtual void GoToEnemy()
    {
        Transform goal = enemy.transform;

        //Pathfind towards the closest side of the ship

        Vector3 r = goal.position - goal.right * difficulty.optimalDist;
        Vector3 l = goal.position + goal.right * difficulty.optimalDist;

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
        rotationalSpeed = Mathf.Min(rotationalSpeed, +difficulty.angularVelocity);
        rotationalSpeed = Mathf.Max(rotationalSpeed, -difficulty.angularVelocity);

        bestAngle %= 180;

        if (Mathf.Abs(bestAngle) < difficulty.shootingAngle || Mathf.Abs(bestAngle) > (180 - difficulty.shootingAngle))
        {
            combat.Shoot();
        }

        if (bestAngle > +1f)
        {
            transform.Rotate(0f, rotationalSpeed, 0f);
        }
        else if (bestAngle < -1f)
        {
            transform.Rotate(0f, -rotationalSpeed, 0f);
        }
    }

    protected void GoToCenter()
    {
        nav.destination = center.position;
    }

    protected void FindEnemy()
    {
        Collider[] data = Physics.OverlapSphere(transform.position, difficulty.detectionDistance);

        //Now check if the data is in sight line by doing a raycast
        GameObject closestEnemy = null;
        float enemyDistance = difficulty.detectionDistance;
        foreach (Collider c in data)
        {
            GameObject g = c.gameObject;

            // Only pathfind towards other ships
            if (!g.CompareTag("Ship") || g == gameObject)
            {
                continue;
            }

            Vector3 disp = transform.position - g.transform.position;
            float magn = Mathf.Abs(disp.magnitude);
            if (magn < enemyDistance)
            {
                enemyDistance = magn;
                closestEnemy = g;
            }
        }

        if (closestEnemy != null)
        {
            foundEnemy = true;
            enemy = closestEnemy;
        }
        else
        {
            foundEnemy = false;
            enemy = null;
        }
    }

    protected virtual void Update()
    {
        if(startTimer > 0)
        {
            startTimer -= Time.deltaTime;
            return;
        }

        FindEnemy();

        if(GetComponent<ShipData>().isInStorm)
        {
            GoToCenter();
            return;
        }

        if(foundEnemy)
        {
            GoToEnemy();
        } else
        {
            GoToCenter();
        }
    }
}
