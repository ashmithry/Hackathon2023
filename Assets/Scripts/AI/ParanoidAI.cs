using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParanoidAI : NormalAI
{
    protected override void GoToEnemy()
    {
        //Go away from the enemy!
        nav.destination = enemy.transform.position + Vector3.right * 10;
    }
}
