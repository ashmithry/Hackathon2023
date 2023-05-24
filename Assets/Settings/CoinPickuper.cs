

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickuper : MonoBehaviour
{

    public GameObject coinPrefab;

    public int coinsToSpawn;

    public Vector3 minSpawn, maxSpawn;

    public void Awake()
    {
        for(int i = 0; i < coinsToSpawn; i++)
        {
            Transform t = Instantiate(coinPrefab, new Vector3(Random.Range(minSpawn.x, maxSpawn.x), 0.689f, Random.Range(minSpawn.z, maxSpawn.z)), coinPrefab.transform.rotation).transform;
            t.parent = transform;
        }
    }
}
