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

        GameObject minimapCam = GameObject.Find("MinimapCam");

        Player.transform.position = playerSpawnPoint.position;
        minimapCam.transform.parent = Player.transform;
        minimapCam.transform.position = new Vector3(0, 85, 0);
        Vector3 playerLookPos = Vector3.zero - Player.transform.position;
        playerLookPos.y = 0;
        Player.transform.rotation = Quaternion.LookRotation(playerLookPos);

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
