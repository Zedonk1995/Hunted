using UnityEngine;

public static class Vector3Extensions
{
    public static float GetComponentInDirectionOf(this Vector3 v1, Vector3 v2) => v1.Dot(v2.normalized);

    public static float Dot(this Vector3 v1, Vector3 v2) => Vector3.Dot(v1, v2);
}