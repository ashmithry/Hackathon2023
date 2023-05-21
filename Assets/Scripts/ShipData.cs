
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public float regen = 1f;
    public float timeToRegen = 3.5f;
    private float regenTimer;
    

    public float damage = 7f;
    public float fireRate = 1.5f;


    public bool isInStorm;
    private float stormTimer;

    public Transform healthBar;
    public Transform healthBarBG;
    public Vector3 barScale;

    private float alpha;

    public GameManager gameManager;


    [Range(0f,1f)]
    public float shiftRate = 0.1f;
    public int speedLevel, regenLevel, cannonLevel, damageLevel, fireRateLevel;

    private TextMeshProUGUI tmp;
    private Slider progbar;

    void Start()
    {

        speedLevel = 0;
        regenLevel = 0;
        cannonLevel = 0;
        damageLevel = 0;
        fireRateLevel = 0;

        combat = GetComponent<ShipCombat>();
        stormTimer = StormSystem.damageTickSpeed;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        controller = GetComponent<ShipController>();
        if(combat.player)
        {
            controller.movementSpeed = speed;
            controller.turnSpeed = speed;
        }

        
        combat.cooldown = fireRate;
        tmp = GameObject.Find("UpgradePTSText").GetComponent<TextMeshProUGUI>();
        progbar = GameObject.Find("LevelProgressBar").GetComponent<Slider>();

        regenTimer = timeToRegen;
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

        //Update Upgrade Points text

        tmp.text = "" + upgradePoints;
        progbar.value = coins / coinsToUpgrade;

        regenTimer -= Time.deltaTime;
        if(regenTimer <= 0)
        {
            health += regen;
            regenTimer = timeToRegen;
        }
    }

    public void Damage(float dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            //player dead
            Dead();

            regenTimer = timeToRegen;
        }

        
    }

    public void Dead()
    {
        if(combat.player)
            gameManager.GameOver();
        gameObject.SetActive(false);
    }

    public Color col;

    void UpdateUI(GameObject ui, int index)
    {
        Debug.Log(ui.transform.GetChild(1).GetChild(index));
        ui.transform.GetChild(1).GetChild(index).GetComponent<Image>().color = col;
    }

    public void Upgrade(string stat)
    {
        if (upgradePoints > 0)
        {
            switch (stat)
            {
                case "cannons":

                    if (cannonLevel >= 3) return;
                    cannonLevel++;

                    UpdateUI(GameObject.Find("CannonUpgrade"), cannonLevel - 1);
                    transform.GetComponentsInChildren<Cannon>()[cannonLevel*2].enabled = true;
                    transform.GetComponentsInChildren<Cannon>()[cannonLevel*2+1].enabled = true;
                    break;
                case "damage":

                    if (damageLevel >= 3) return;

                    damage += 1.5f;
                    damageLevel++;
                    UpdateUI(GameObject.Find("DamageUpgrade"), damageLevel - 1);
                    break;
                case "fire rate":

                    if (fireRateLevel >= 3) return;

                    fireRate -= 0.25f;
                    combat.cooldown = fireRate;
                    fireRateLevel++;
                    UpdateUI(GameObject.Find("FireRateUpgrade"), fireRateLevel - 1);
                    break;
                case "regen":

                    if (regenLevel >= 3) return;

                    regen += 2f;
                    regenLevel++;
                    UpdateUI(GameObject.Find("RegenUpgrade"), regenLevel - 1);
                    break;
                case "speed":

                    if (speedLevel >= 3) return;

                    Debug.Log("Updating Speed!");

                    speed += 10;
                    controller.movementSpeed = speed;
                    controller.turnSpeed = speed;
                    speedLevel++;
                    UpdateUI(GameObject.Find("SpeedUpgrade"), speedLevel - 1);
                    break;
                default:
                    return;

            }

            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("upgradeSound");
            upgradePoints -= 1;
        }
    }

}
