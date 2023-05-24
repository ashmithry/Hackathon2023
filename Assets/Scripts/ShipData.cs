
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

    public float health = 75f;
    public int upgradePoints = 0;

    public int coins;
    public float coinsToUpgrade = 10;

    public float speed = 100f;

    public float regen = 1f;
    public float timeToRegen = 3.5f;
    private float regenTimer;
    

    public float damage = 7f;
    public float fireRate = 1.5f;

    public GameObject explodeEffect;

    public bool isInStorm;
    private float stormTimer;

    public Transform healthBar;
    public Transform healthBarBG;
    public Vector3 barScale;

    public Transform username;

    private float alpha;
    private float palpha;

    public GameManager gameManager;

    [Range(0f,1f)]
    public float shiftRate = 0.1f;
    public int speedLevel, regenLevel, cannonLevel, damageLevel, fireRateLevel;

    private TextMeshProUGUI upgradePTSText;
    public TextMeshProUGUI killsText;
    private Slider progbar;

    public int kills;

    public static string playerUsername;

    public GameObject stormVolume;

    void Start()
    {
        speedLevel = 0;
        regenLevel = 0;
        cannonLevel = 0;
        damageLevel = 0;
        fireRateLevel = 0;
        kills = 0;
        palpha = 0;

        combat = GetComponent<ShipCombat>();
        stormTimer = StormSystem.damageTickSpeed;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        controller = GetComponent<ShipController>();
        if(combat.player)
        {
            controller.movementSpeed = speed;
            controller.turnSpeed = speed;
            username.GetChild(0).GetComponent<TextMeshPro>().text = PlayerPrefs.GetString("username");
        }

        combat.cooldown = fireRate;
        upgradePTSText = GameObject.Find("UpgradePTSText").GetComponent<TextMeshProUGUI>();
        progbar = GameObject.Find("LevelProgressBar").GetComponent<Slider>();

        regenTimer = timeToRegen;
    }

    // Update is called once per frame
    void Update()
    {
        alpha = Mathf.Lerp(alpha, health/50f, shiftRate);
        alpha = Mathf.Clamp(alpha, 0f, 1f);
        healthBar.localScale = new Vector3(barScale.x * alpha,1,1);

        if(combat.player && isInStorm)
        {
            stormVolume.SetActive(true);
        }
        else if(combat.player)
        {
            stormVolume.SetActive(false);
        }

        healthBarBG.LookAt(Camera.main.transform);
        username.LookAt(Camera.main.transform);

        if(coins >= coinsToUpgrade)
        {
            upgradePoints += 1;
            coinsToUpgrade =  Mathf.Floor(coinsToUpgrade *= 1.1f);
            coins = 0;
        }

        if(isInStorm && stormTimer <= 0)
        {
            Damage(StormSystem.stormDamage);
            stormTimer = StormSystem.damageTickSpeed;
        }

        stormTimer -= Time.deltaTime;

        //Update Upgrade Points text

        upgradePTSText.text = "" + upgradePoints;

        palpha = Mathf.Lerp(palpha, coins / coinsToUpgrade, 0.1f);
        progbar.value = palpha;

        regenTimer -= Time.deltaTime;
        if(regenTimer <= 0)
        {
            health += regen;
            regenTimer = timeToRegen;
        }

        if (GetComponent<ShipCombat>().player)
        {
            killsText.text = "" + kills;
        }
    }

    public void Damage(float dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            //player dead
            GameObject.Find("GameManager").GetComponent<GameManager>().OnKilled(gameObject);
            Dead();
        }
    }

    public void Dead()
    {
        if(combat.player)
            gameManager.GameOver();

        Instantiate(explodeEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
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

                    damage += 4f;
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
