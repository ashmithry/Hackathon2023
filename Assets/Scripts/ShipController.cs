
using System.Timers;
using System.Threading;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [Header("Values")]
     float xInput, yInput;
    public float movementSpeed;
    public float turnSpeed;

    [Header("References")]
    public Rigidbody rb;
    public Transform shipTransform;



    public bool isInStorm;
    private float stormTimer;

    private ShipData data;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        data = GetComponent<ShipData>();
        stormTimer = StormSystem.damageTickSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        shipTransform.Rotate(new Vector3(0f, xInput * turnSpeed * Time.deltaTime, 0f));

        if(isInStorm)
        {
            if(stormTimer <= 0)
            {
                data.Damage(StormSystem.stormDamage);
                Debug.Log("storm Damage");
                stormTimer = StormSystem.damageTickSpeed;
            }
        }

        stormTimer -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        rb.AddForce(yInput * shipTransform.forward * movementSpeed * Time.fixedDeltaTime);
    }
}
