using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public GameObject[] AI;

    public Transform PlayerSpawnPoint;
    public GameObject Player;

    void Awake()
    {
        //Spawn AI around the map
        Transform[] spawnPoints = GetComponentsInChildren<Transform>();

        Transform playerSpawnPoint = spawnPoints[(int)Random.Range(0, spawnPoints.Length)];

        GameObject p = Instantiate(Player, playerSpawnPoint.position, playerSpawnPoint.rotation);
        Vector3 playerLookPos = Vector3.zero - p.transform.position;
        playerLookPos.y = 0;
        p.transform.rotation = Quaternion.LookRotation(playerLookPos);

        foreach (Transform sp in spawnPoints)
        {
            if(sp == transform || sp == playerSpawnPoint)
            {
                continue;
            }

            GameObject cur = AI[(int)Random.Range(0, AI.Length)];
            GameObject o = Instantiate(cur, sp.position, cur.transform.rotation);

            Vector3 lookPos = Vector3.zero - o.transform.position;
            lookPos.y = 0;
            o.transform.rotation = Quaternion.LookRotation(lookPos);

        }


    }
}
