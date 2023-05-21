using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DestroyIn : MonoBehaviour
{
    public float time;
    public void MakeActive()
    {
        GetComponent<TextMeshProUGUI>().enabled = true;
        Invoke("StopSelf", time);
    }

    public void StopSelf()
    {
        GetComponent<TextMeshProUGUI>().enabled = false;
    }
}
