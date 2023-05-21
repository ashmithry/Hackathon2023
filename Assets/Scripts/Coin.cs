using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        col.transform.GetComponent<ShipData>().coins++;
        Destroy(gameObject);
    }
}
