using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    
    public GameObject[] AI;
    public Transform PlayerSpawnPoint;
    public GameObject Player;

    public ShipData[] data = new ShipData[20];

    void Awake()
    {
        //Spawn AI around the map
        Transform[] spawnPoints = GetComponentsInChildren<Transform>();

        Transform playerSpawnPoint = spawnPoints[(int)Random.Range(0, spawnPoints.Length)];
        int index = 0;
        GameObject p = GameObject.Find("Player");
        GameObject minimapCam = GameObject.Find("MinimapCam");
        data[index] = p.GetComponent<ShipData>();
        minimapCam.transform.parent = p.transform;
        minimapCam.transform.localPosition = new Vector3(0,85,0);

        Vector3 playerLookPos = Vector3.zero - p.transform.position;
        playerLookPos.y = 0;
        p.transform.rotation = Quaternion.LookRotation(playerLookPos);

        index = 1;
        foreach (Transform sp in spawnPoints)
        {
            if(sp == transform || sp == playerSpawnPoint)
            {
                continue;
            }

            GameObject cur = AI[(int)Random.Range(0, AI.Length)];
            GameObject o = Instantiate(cur, sp.position, cur.transform.rotation);

            data[index] = o.GetComponent<ShipData>();

            Vector3 lookPos = Vector3.zero - o.transform.position;
            lookPos.y = 0;
            o.transform.rotation = Quaternion.LookRotation(lookPos);
            index++;
        }
    }
}
