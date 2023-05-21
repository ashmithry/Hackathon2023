
using System.Timers;
using System.Threading;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShipController : MonoBehaviour
{
    [Header("Values")]
     float xInput, yInput;
    public float movementSpeed;
    public float turnSpeed;

    [Header("References")]
    public Rigidbody rb;
    public Transform shipTransform;
    CinemachineVirtualCamera vcam;

    public float startTimer = 4.0f;

    public ShipData data;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        data = GetComponent<ShipData>();
        GetComponent<ShipCombat>().player = true;
        GetComponent<ShipCombat>().player = false;

        vcam = GameObject.Find("ShipFollowCamera").GetComponent<CinemachineVirtualCamera>();
        vcam.LookAt = transform;
        vcam.Follow = transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer < 0)
        {
            Destroy(GameObject.Find("CountdownCanvas"));
            GetComponent<ShipCombat>().player = true;
            xInput = Input.GetAxisRaw("Horizontal");
            yInput = Input.GetAxisRaw("Vertical");

            shipTransform.Rotate(new Vector3(0f, xInput * turnSpeed * Time.deltaTime, 0f));

            if(xInput > 0f || yInput > 0f) 
            {
                Vector2 Input = new Vector2(xInput, yInput);
                Input.Normalize();
                GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("movementSound", Mathf.Abs(Input.magnitude));
            }
        } else
        {
            startTimer -= Time.deltaTime;
        }
        
    }

    void FixedUpdate()
    {
        rb.AddForce(yInput * shipTransform.forward * movementSpeed * Time.fixedDeltaTime);
    }
}
