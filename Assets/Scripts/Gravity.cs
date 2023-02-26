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

    float gravity = 0f;
    float gravityIncrement = 100f;
    float gravityMax = 1000f;

    float timeSinceLastGravityIncrease = 0f;
    float gravityIncreaseInteveral = 0.1f; // time in seconds between increasing the gravity

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
        isGrounded = Utils.IsGrounded(myBoxCollider, myRigidbody, ref groundCheckHit);

        if (isGrounded) {
            gravity = 0;
            timeSinceLastGravityIncrease = 0;
        } else {
            timeSinceLastGravityIncrease += Time.fixedDeltaTime;

            while(timeSinceLastGravityIncrease > gravityIncreaseInteveral) {
                gravity += gravityIncrement;
                timeSinceLastGravityIncrease -= gravityIncreaseInteveral;
            }


            if (gravity > gravityMax )
            {
                gravity = gravityMax;
            }
        }

        myRigidbody.AddForce(mass * gravity * Vector3.down, ForceMode.Force);
    }
}
