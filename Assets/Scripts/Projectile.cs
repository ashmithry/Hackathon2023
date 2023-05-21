
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log(col.gameObject);
        if (col.gameObject == shooter) return;

        if(col.transform.GetComponent<ShipData>() != null)
        {
            col.transform.GetComponent<ShipData>().Damage(damage);

            if(col.transform.GetComponent<ShipData>().health <= 0)
            {
                shooter.GetComponent<ShipData>().kills++;
                Debug.Log("Kills: " + shooter.GetComponent<ShipData>().kills);
            }

            DestroySelf();
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
