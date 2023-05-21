using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillFeed : MonoBehaviour
{
    public List<GameObject> data;
    public List<float> timers;

    public GameObject killFeedEntry;
    public float duration;
    void Start()
    {
        data = new List<GameObject>();
    }

    void Update()
    {
        for (int i = 0; i < timers.Count; i++)
        {
            timers[i] -= Time.deltaTime;

            if (timers[i] < 0)
            {
                Destroy(data[i]);
                timers.RemoveAt(i);
                data.RemoveAt(i);
            }
        }
    }

    void AddKill(string cause, string who)
    {
        timers.Add(duration);

        GameObject kill = Instantiate(killFeedEntry, transform.position, transform.rotation);
        Debug.Log(kill.transform.position);
        kill.GetComponent<TextMeshProUGUI>().text = who + " got one tapped by " + cause;
        data.Add(kill);
    }
}
