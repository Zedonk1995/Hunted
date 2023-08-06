using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLandInput : MonoBehaviour, ILandMovementInput, IJumptInput, ILookHorizontalInput, ILookVerticalInput
{
    public Vector2 MoveInput { get; private set; } = Vector2.zero;
    public Vector2 LookInput { get; private set; } = Vector2.zero;

    public bool JumpIsPressed { get; private set; } = false;

    InputActions input = null;

    private void OnEnable()
    {
        input = new InputActions();

        // you have to enable action maps before you use it.
        input.PlayerLand.Enable();

        // we subscribe to the event on LHS which triggers the function on RHS.
        input.PlayerLand.Move.performed += SetMove;
        input.PlayerLand.Move.canceled += SetMove;

        input.PlayerLand.Look.performed += SetLook;
        input.PlayerLand.Look.canceled += SetLook;

        input.PlayerLand.Jump.started += SetJump;
        input.PlayerLand.Jump.canceled+= SetJump;
    }

    private void OnDisable()
    {
        // unsubscribes from the events
        input.PlayerLand.Move.performed -= SetMove;
        input.PlayerLand.Move.canceled -= SetMove;

        input.PlayerLand.Look.performed -= SetLook;
        input.PlayerLand.Look.canceled -= SetLook;

        input.PlayerLand.Jump.started -= SetJump;
        input.PlayerLand.Jump.canceled -= SetJump;

        input.PlayerLand.Disable();
    }

    private void SetMove(InputAction.CallbackContext ctx)
    {
        MoveInput = ctx.ReadValue<Vector2>();
    }

    private void SetLook(InputAction.CallbackContext ctx)
    {
        LookInput = ctx.ReadValue<Vector2>();
    }

    private void SetJump(InputAction.CallbackContext ctx)
    {
        JumpIsPressed = ctx.started;
    }
}
