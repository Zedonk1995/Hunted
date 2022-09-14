using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputConstants
{
    public const string InputAxisMouseX = "Mouse X";
    public const string InputAxisMouseY = "Mouse Y";
    public const string Horizontal = "Horizontal";
    public const string Vertical = "Vertical";
}

public class PlayerController : MonoBehaviour
{
    private const float mouseSensitivity = 3.5f;
    public Rigidbody myRigidbody { get; private set; }
    public float MaxSpeed => 50f;


    public static PlayerController player { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        player = this;

        Cursor.lockState = CursorLockMode.Locked;

        // I'm not sure this is needed.
        Cursor.visible = false;

        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MouseLookUpdate();
    }

    private void FixedUpdate()
    {
        var aOrD = Input.GetAxisRaw(InputConstants.Horizontal);
        var wOrS = Input.GetAxisRaw(InputConstants.Vertical);


        var a = wOrS * transform.forward;
        a.y = 0;

        var b = aOrD * transform.right;
        b.y = 0;

        var direction = (a.normalized + b.normalized).normalized;



        var accelration = 200f;

        var dragFactor = accelration / MaxSpeed;


        /* 
            This assumes that the force due to air resistance is equal to drag factor * velocity.
            maxSpeed is the max speed of the player
            dragFactor is the drag coffecient of the air resistance
            accelration is the inital accelration of the ball assuming it starts at rest
            Don't try to work these out again - I already did it once!  
        */
        myRigidbody.AddForce(dragFactor * (MaxSpeed * direction - myRigidbody.velocity) );


    }

    private void MouseLookUpdate()
    {
        var mouseDelta = mouseSensitivity * new Vector2(Input.GetAxis(InputConstants.InputAxisMouseX), Input.GetAxis(InputConstants.InputAxisMouseY));


        //Pitch
        transform.Rotate(Vector3.right, -mouseDelta.y, Space.Self);

        //Angle around up.
        transform.Rotate(Vector3.up, mouseDelta.x, Space.World);
    }

    private void MoveForward()
    {


    }
}
