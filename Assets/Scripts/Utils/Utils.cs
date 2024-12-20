using UnityEngine;

public class Utils
{
    static float groundCheckSizeMultiplier = 0.9f;
    static float groundCheckDistance = 0.3f;

    static float maxSlopeAngle = 55f;

    // perf
    // if you do run into perfromance issues, take object number and frame and store information in a dictionary (dictionary allows you to do a fast search)
    // key for dictionary would be object identifier/id value would be what frame it is and whether it's grounded
    public static bool IsGrounded(BoxCollider myBoxCollider, Rigidbody myRigidbody, out RaycastHit groundCheckHit)
    {
        Vector3 myBoxColliderPosition = myRigidbody.position + myBoxCollider.center;

        Vector3 halfBoxCastSize = groundCheckSizeMultiplier * myBoxCollider.size/2;
        float boxCastTravelDistance = myBoxCollider.size.y/2 - halfBoxCastSize.y + groundCheckDistance;

        // ~ will flip all bits so ~layermask gets all layers except the layer you used ~ on.
        LayerMask layerMask = ~LayerMask.GetMask("Projectile");

        bool raycastHitGround = Physics.BoxCast(myBoxColliderPosition, halfBoxCastSize, Vector3.down, out groundCheckHit, myRigidbody.rotation, boxCastTravelDistance, layerMask);

        float slopeAngle = Vector3.Angle(Vector3.up, groundCheckHit.normal);

        return raycastHitGround && slopeAngle <= maxSlopeAngle;
    }
}
