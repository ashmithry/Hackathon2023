
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipData : MonoBehaviour
{
    [Header("Data")]
    public ShipController controller;
    public ShipCombat combat;

    public float health = 50f;
    public int upgradePoints = 0;

    public int coins;
    public float coinsToUpgrade = 10;


    public float speed = 100f;

    public float regen = 0.25f;
    public float timeToRegen = 5f;
    private float regenTimer;
    

    public float damage = 7f;
    public int cannons = 1;
    public float fireRate = 1.5f;


    public bool isInStorm;
    private float stormTimer;


    void Start()
    {
        combat = GetComponent<ShipCombat>();
        stormTimer = StormSystem.damageTickSpeed;

        controller = GetComponent<ShipController>();
        if(combat.player)
        {
            controller.movementSpeed = speed;
            controller.turnSpeed = speed;
        }

        
        combat.cooldown = fireRate;


    }

    // Update is called once per frame
    void Update()
    {
        if(coins >= coinsToUpgrade)
        {
            upgradePoints += 1;
            coinsToUpgrade =  Mathf.Floor(coinsToUpgrade *= 1.1f);
            coins = 0;
        }

        if(isInStorm)
        {
            if(stormTimer <= 0)
            {
                Damage(StormSystem.stormDamage);
                stormTimer = StormSystem.damageTickSpeed;
            }
        }

        stormTimer -= Time.deltaTime;
    }

    public void Damage(float dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            //player dead
            if(!combat.player)
                Destroy(gameObject);
        }
    }

    public void Dead()
    {
        //delete player
        Destroy(gameObject);
    }

    public void Upgrade(string stat)
    {
        if(upgradePoints > 0)
        {
            switch(stat)
            {
                case "cannons":
                    cannons += 1;
                    break;
                case "damage":
                    damage += 1.5f;
                    break;
                case "fire rate":
                    fireRate -= 0.25f;
                    combat.cooldown = fireRate;
                    break;
                case "regen":
                    regen += 0.25f;
                    break;
                case "speed":
                    speed += 10;
                    controller.movementSpeed = speed;
                    controller.turnSpeed = speed;
                    break;
                default:
                    break;

            }

            upgradePoints -= 1;
        }
    }

}
