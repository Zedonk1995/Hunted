using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltMovementScript : MonoBehaviour
{

    private CharacterController controller;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float speed = 12.0f;

    Vector3 nonWasdVelocity; // velocity minus the velocity due WASD movement.

    public float gravity = 9.8f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        bool isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // resets acceleration due to gravity when on the ground
        if (isGrounded && nonWasdVelocity.y < 0)
        {
            nonWasdVelocity.y = -2f;    // negative to stop players floating slightly above the floor
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.fixedDeltaTime);

        // Enforce gravity on the player
        nonWasdVelocity.y += -gravity * Time.deltaTime;
        controller.Move(nonWasdVelocity * Time.deltaTime);
    }
}
