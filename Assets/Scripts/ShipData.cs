using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipData : MonoBehaviour
{
    [Header("Data")]
    public float health = 50f;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(float dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            //player dead
            Debug.Log("dead");
        }
    }

    public void Dead()
    {
        //delete player
        Destroy(gameObject);
    }
}
