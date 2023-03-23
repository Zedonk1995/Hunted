using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    BoxCollider myBoxCollider = null;
    Rigidbody myRigidbody = null;
    ILandInput input = null;

    bool isGrounded = true;
    RaycastHit groundCheckHit;

    Vector3 moveInput = Vector3.zero;

    private Vector3 playerMoveInputDirection = Vector3.zero;
    private Vector3 localCurrentVelocity = Vector3.zero;

    public float maxSpeed { get; set; } = 10.0f;
    public float dragCoefficient { get; set; } = 10.0f;

    bool jumpRecentlyPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        myBoxCollider = GetComponent<BoxCollider>();
        myRigidbody = GetComponent<Rigidbody>();
        input = GetComponent<ILandInput>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 GetMoveInput()
    {
        return new Vector3(input.MoveInput.x, 0.0f, input.MoveInput.y);
    }

    // this function works in local coordinates.
    private Vector3 getPlayerMove(Vector3 moveInput)
    {
        playerMoveInputDirection = new Vector3(moveInput.x, moveInput.y, moveInput.z).normalized;

        if (TryGetComponent(out Jump jumpScript))
        {
            jumpRecentlyPressed = jumpScript.JumpRecentlyPressed;
        }

        if (isGrounded && !jumpRecentlyPressed)
        {
            Vector3 localGroundCheckHitNormal = transform.InverseTransformDirection(groundCheckHit.normal);
            
            float groundSlopeAngle = Vector3.Angle(Vector3.up, localGroundCheckHitNormal);

            Quaternion slopeAngleRotation = Quaternion.FromToRotation(Vector3.up, localGroundCheckHitNormal);

            Vector3 directionOfPropulsion = slopeAngleRotation * playerMoveInputDirection;

            Debug.Log(directionOfPropulsion);

            // Unity cannot handle large numbers so drag is set to be proportional to velocity even though that's not actually how drag works
            Vector3 propulsion = dragCoefficient * maxSpeed * directionOfPropulsion;
            Vector3 drag = dragCoefficient * localCurrentVelocity;

            return propulsion - drag;
        }

        return Vector3.zero;
    }

    private void FixedUpdate()
    {
        isGrounded = Utils.IsGrounded(myBoxCollider, myRigidbody, ref groundCheckHit);

        Vector3 currentVelocity = myRigidbody.velocity;
        localCurrentVelocity = transform.InverseTransformDirection(currentVelocity);
        float speedSquared = Vector3.SqrMagnitude(localCurrentVelocity);

        moveInput = GetMoveInput();
        moveInput = getPlayerMove(moveInput);

        myRigidbody.AddRelativeForce(moveInput * myRigidbody.mass, ForceMode.Force); // ForceMode.Force is the default value but I put in there for clarity
    }


}
