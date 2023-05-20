using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormSystem : MonoBehaviour
{
    public Vector3 startSize, endSize;
    public float shrinkSpeed;
    
    private void Awake()
    {
        transform.localScale = new Vector3(startSize.x, 100, startSize.z);
        
    }
    private void Update()
    {
        if(transform.localScale.z > endSize.z)
        {
            transform.localScale = new Vector3(transform.localScale.x - shrinkSpeed*Time.deltaTime, 100, transform.localScale.z - shrinkSpeed*Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //stop dealing damage to any ship in zone
        if(other.GetComponent<ShipController>() != null)
        {
            other.GetComponent<ShipController>().isInStorm = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //deal damage to any ship in zone
        if(other.GetComponent<ShipController>() != null)
        {
            other.GetComponent<ShipController>().isInStorm = true;
        }
    }
}
