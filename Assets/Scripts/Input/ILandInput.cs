using UnityEngine;

public interface ILandInput
{
    public Vector2 MoveInput { get; }
    public bool JumpIsPressed { get; }
}
