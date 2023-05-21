
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float duration;
    public float damage;
    

    void Start()
    {
        Invoke("DestroySelf", duration);
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.transform.GetComponent<ShipData>() != null)
        {
            col.transform.GetComponent<ShipData>().Damage(damage);
            DestroySelf();
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
