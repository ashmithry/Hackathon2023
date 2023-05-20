using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileAI : NormalAI
{
    protected override void Update()
    {
        FindEnemy();

        if(foundEnemy)
        {
            GoToEnemy();
        } else
        {
            GoToCenter();
        }

        combat.Shoot();
    }
}
