using UnityEditor.PackageManager;
using UnityEngine;

public static class Utils
{
    static float groundCheckSizeMultiplier = 0.9f;
    static float groundCheckDistance = 0.05f;

    // perf
    // if you do run into perfromance issues, take object number and frame and store information in a dictionary (dictionary allows you to do a fast search)
    // key for dictionary would be object identifier/id value would be what frame it is and whether it's grounded
    public static bool IsGrounded(BoxCollider myBoxCollider, Rigidbody myRigidbody, ref RaycastHit groundCheckHit)
    {
        Vector3 boxCastSize = groundCheckSizeMultiplier * myBoxCollider.size/2;
        float boxCastTravelDistance = myBoxCollider.size.y - boxCastSize.y + groundCheckDistance;
        return Physics.BoxCast(myRigidbody.position, boxCastSize, Vector3.down, out groundCheckHit, Quaternion.identity, boxCastTravelDistance ); 
    }
}
