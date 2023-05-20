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

    [HideInInspector]public bool isInStorm;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        shipTransform.Rotate(new Vector3(0f, xInput * turnSpeed * Time.deltaTime, 0f));

        if(isInStorm)
        {
            
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(yInput * shipTransform.forward * movementSpeed * Time.fixedDeltaTime);
    }
}
