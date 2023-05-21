using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.transform.GetComponent<ShipData>() == null) return;

        col.transform.GetComponent<ShipData>().coins++;
        Destroy(gameObject);

        if(col.transform.GetComponent<ShipCombat>().player)

            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("coinPick");
    }
}
