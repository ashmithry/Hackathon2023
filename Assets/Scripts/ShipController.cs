using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [Header("Values")]
    public int xInput, yInput;
    public float movementSpeed;
    public float turnSpeed;

    [Header("References")]
    public Rigidbody rb;
    public Transform shipTransform;

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
    }

    void FixedUpdate()
    {
        rb.AddForce(shipTransform.forward * moveSpeed * Time.fixedDeltaTime);
    }
}
