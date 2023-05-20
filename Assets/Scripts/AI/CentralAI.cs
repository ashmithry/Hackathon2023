using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralAI : NormalAI
{
    private bool reachedCenter;
    // Update is called once per frame
    protected override void Update()
    {
        if(!reachedCenter && transform.position == center.position)
        {
            reachedCenter = true;
        }

        if(reachedCenter)
        {
            FindEnemy();
            if (foundEnemy)
            {
                GoToEnemy();
            }
            else
            {
                GoToCenter();
            }
        } else
        {
            GoToCenter();
        }
    }
}
