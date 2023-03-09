using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Jump : MonoBehaviour
{
    BoxCollider myBoxCollider = null;
    Rigidbody myRigidbody = null;
    ILandInput input = null;
    Gravity gravityScript = null;

    float mass;

    bool isGrounded = true;
    RaycastHit groundCheckHit;

    bool JumpIsPressed = false;

    float jumpHeight = 10f;
    float GravityStrength;
    float initialJumpSpeed;
    Vector3 jumpForce;

    // number of seconds before jump can be pressed again
    float jumpCooldown = 0.5f;
    bool isJumpEnabled = true;

    // records whether the player has pressed jump recently.
    // Needed to disable drag in the ground movement script  
    public bool JumpRecentlyPressed { get; private set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        myBoxCollider = GetComponent<BoxCollider>();
        myRigidbody = GetComponent<Rigidbody>();
        input = GetComponent<ILandInput>();

        mass = myRigidbody.mass;

        gravityScript = GetComponent<Gravity>();
        GravityStrength = gravityScript.GravityStrength;

        // initial jump speed is the square root of 2 times gravity times height.
        initialJumpSpeed = Mathf.Sqrt(2 * GravityStrength * jumpHeight);

        // F = ma = m*(speed/time) since acceleration due to gravity is constant.
        jumpForce = Vector3.zero;
        jumpForce.y = ( mass * initialJumpSpeed ) / Time.fixedDeltaTime;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private bool GetJumpInput()
    {
        return input.JumpIsPressed;
    }


    void FixedUpdate()
    {
        isGrounded = Utils.IsGrounded(myBoxCollider, myRigidbody, ref groundCheckHit);

        JumpIsPressed = GetJumpInput();

        if (JumpIsPressed == true && isJumpEnabled && isGrounded )
        {
            myRigidbody.AddForce(jumpForce, ForceMode.Force);
            isJumpEnabled = false;
            JumpRecentlyPressed = true;
        }

    }
}
