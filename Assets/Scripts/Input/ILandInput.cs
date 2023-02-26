using UnityEngine;

public interface ILandInput
{
    public Vector2 moveInput { get; }
    public bool jumpIsPressed { get; }
}
