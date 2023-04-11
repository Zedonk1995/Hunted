using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    BoxCollider myBoxCollider = null;
    Rigidbody myRigidbody = null;

    float mass;

    public float GravityStrength { get; private set; } = 20f;


    // Start is called before the first frame update
    void Start()
    {
        myBoxCollider = GetComponent<BoxCollider>();
        myRigidbody = GetComponent<Rigidbody>();

        mass = myRigidbody.mass;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        bool isGrounded = Utils.IsGrounded(myBoxCollider, myRigidbody, out _);

        if (!isGrounded)
        {
            myRigidbody.AddForce(mass * GravityStrength * Vector3.down, ForceMode.Force);
        }
    }
}
