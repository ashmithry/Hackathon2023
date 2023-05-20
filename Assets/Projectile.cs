
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float duration;
    

    void Start()
    {
        Invoke("DestroySelf", duration);
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
