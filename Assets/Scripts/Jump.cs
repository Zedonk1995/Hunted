using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Jump : MonoBehaviour
{
    Rigidbody myRigidbody = null;
    ILandInput input = null;

    Boolean JumpIsPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        input = GetComponent<ILandInput>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Boolean GetJumpInput()
    {
        return input.JumpIsPressed;
    }


    void FixedUpdate()
    {
        JumpIsPressed = GetJumpInput();

        if (JumpIsPressed == true )
        {
            //Debug.Log("yarg");
            myRigidbody.AddForce(new Vector3(0f, 100000f, 0f), ForceMode.Force);
        }

    }
}
