
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCombat : MonoBehaviour
{
    
    public GameObject cannonParent;
    public Cannon[] cannons;

    [Header("Combat")]
    public float cooldown;
    private float timer;
    public bool player;
    void Start()
    {
        cannons = cannonParent. GetComponentsInChildren<Cannon>(); 
         
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Space) && player)
        {
            Shoot();
        }

        timer-= Time.deltaTime;
    }

    public void Shoot()
    {
        if(timer <= 0)
        {
            foreach(Cannon c in cannons)
                {
                    c.Shoot();
                }
                timer = cooldown;
        }
    }
}
