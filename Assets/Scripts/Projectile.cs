
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float duration;
    public float damage;
    public GameObject shooter;
    

    void Start()
    {
        Invoke("DestroySelf", duration);
        shooter = transform.parent.parent.parent.parent.parent.gameObject;
        damage = shooter.GetComponent<ShipData>().damage;
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == shooter) return;

        if(col.transform.GetComponent<ShipData>() != null)
        {
            col.transform.GetComponent<ShipData>().Damage(damage);

            if(col.transform.GetComponent<ShipData>().health <= 0)
            {
                shooter.GetComponent<ShipData>().kills++;
                shooter.GetComponent<ShipData>().upgradePoints++;

                if (shooter == GameObject.Find("Player"))
                {
                    string enemy = col.gameObject.GetComponent<ShipData>().username.GetChild(0).GetComponent<TextMeshPro>().text;

                    GameObject.Find("ElimStatusText").GetComponent<DestroyIn>().MakeActive();
                    GameObject.Find("ElimStatusText").GetComponent<TextMeshProUGUI>().text = "Eliminated " + enemy;
                }
            }


            DestroySelf();
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
