
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

    public Transform healthBar;
    public Transform healthBarBG;
    public Vector3 barScale;

    private float alpha;


    [Range(0f,1f)]
    public float shiftRate = 0.1f;
    public int speedLevel, regenLevel, cannonLevel, damageLevel, fireRateLevel;

    void Start()
    {

        speedLevel = 0;
        regenLevel = 0;
        cannonLevel = 0;
        damageLevel = 0;
        fireRateLevel = 0;

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
        alpha = Mathf.Lerp(alpha, health/50f, shiftRate);
        alpha = Mathf.Clamp(alpha, 0f, 1f);
        healthBar.localScale = new Vector3(barScale.x * alpha,1,1);

        healthBarBG.LookAt(Camera.main.transform);

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
        Debug.Log("Updating!");

        if (upgradePoints > 0)
        {
            switch(stat)
            {
                case "cannons":

                    if (cannonLevel >= 3) return;

                    cannons += 1;
                    cannonLevel++;
                    break;
                case "damage":

                    if (damageLevel >= 3) return;

                    damage += 1.5f;
                    damageLevel++;
                    break;
                case "fire rate":

                    if (fireRateLevel >= 3) return;

                    fireRate -= 0.25f;
                    combat.cooldown = fireRate;
                    fireRateLevel++;
                    break;
                case "regen":

                    if (regenLevel >= 3) return;

                    regen += 0.25f;
                    regenLevel++;
                    break;
                case "speed":

                    if (speedLevel >= 3) return;

                    speed += 10;
                    controller.movementSpeed = speed;
                    controller.turnSpeed = speed;
                    speedLevel++;
                    break;
                default:
                    break;

            }

            upgradePoints -= 1;
        }
    }

}
