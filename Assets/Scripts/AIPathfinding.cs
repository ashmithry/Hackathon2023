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
            agent.destination = r;
        }
        else
        {
            agent.destination = l;
        }

        if (Mathf.Abs((transform.position - agent.destination).magnitude) < 3f)
        {
            canShoot = true;
        }

        //Rotate in the same direction as the ship

        float bestAngle = goal.rotation.eulerAngles.y - transform.rotation.eulerAngles.y;

        float rotationalSpeed = bestAngle / 10;
        rotationalSpeed = Mathf.Min(rotationalSpeed, 1f);

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
