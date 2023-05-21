using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StormSystem : MonoBehaviour
{
    public Vector3 startSize, endSize;
    public float shrinkSpeed;

    public static float damageTickSpeed = 1;
    public static float stormDamage = 2.5f;
    
    ShipController player;
    private void Awake()
    {
        transform.localScale = new Vector3(startSize.x, 100, startSize.z);
        player = GameObject.Find("Player").GetComponent<ShipController>();
        
    }
    private void Update()
    {
        if(transform.localScale.z > endSize.z && player.startTimer < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x - shrinkSpeed*Time.deltaTime, 100, transform.localScale.z - shrinkSpeed*Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //stop dealing damage to any ship in zone
        if(other.transform.GetComponent<ShipData>() != null)
        {
            other.transform.GetComponent<ShipData>().isInStorm = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //deal damage to any ship in zone
        if(other.transform.GetComponent<ShipData>() != null)
        {
            other.transform.GetComponent<ShipData>().isInStorm = true;
            if(GameObject.Find("Player") == other.transform.gameObject)
            {
                GameObject.Find("StormStatusText").GetComponent<DestroyIn>().MakeActive();
            }
        }
    }
}
