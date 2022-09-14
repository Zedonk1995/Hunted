using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    Rigidbody myRigidBody;
    public Vector3 directionOfPropulsion { get; set; }

    public float maxSpeed { get; set; } = 30.0f;
    public float dragCoefficient { get; set; } = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 currentVelocity = myRigidBody.velocity;

        Vector3 resultantForce = dragCoefficient * (maxSpeed * directionOfPropulsion - currentVelocity);
        myRigidBody.AddForce(resultantForce);
    }


}
