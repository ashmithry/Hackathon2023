using System.Reflection.Metadata;
using Microsoft.CSharp.RuntimeBinder;
using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [Header("Values")]
    public int xInput, yInput;
    public float movementSpeed;

    [Header("References")]
    public Rigidbody rb;

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
    }

    void void FixedUpdate()
    {
        rb
    }
}
