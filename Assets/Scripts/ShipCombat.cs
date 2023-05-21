
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

    public float startTimer = 5.0f;
    void Start()
    {
        UpdateCannons();
    }

    public void UpdateCannons()
    {
        cannons = cannonParent.GetComponentsInChildren<Cannon>();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Space) && player)
        {
            Shoot();
        }

        timer -= Time.deltaTime;
        startTimer -= Time.deltaTime;

        if(startTimer < 0) 
        {
            cannons[0].enabled = true;
            cannons[1].enabled = true;
        }
    }

    public void Shoot()
    {
        if(timer <= 0)
        {
            foreach(Cannon c in cannons)
                {
                    c.Shoot(player);
                }
                timer = cooldown;
        }
    }

    public Cannon[] GetCannons()
    {
        return cannons;
    }
}
