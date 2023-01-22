using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    bool isGrounded = true;

    BoxCollider myBoxCollider = null;
    Rigidbody myRigidbody = null;

    float mass;

    RaycastHit groundCheckHit;

    Vector3 gravitationalForce = 9.8f * Vector3.down;

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

        isGrounded = Utils.IsGrounded(myBoxCollider, myRigidbody, ref groundCheckHit);

        if (!isGrounded)
        {
           myRigidbody.AddForce(mass * gravitationalForce, ForceMode.Force);
        }
    }
}
