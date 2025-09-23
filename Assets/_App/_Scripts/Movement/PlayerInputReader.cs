using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour, IPlayerInput
{
    public Vector2 Move { get; set; }
    public bool SprintHeld { get; private set; }

    private bool _jumpQueued;
    
    public void OnMove(InputAction.CallbackContext context) => Move = context.ReadValue<Vector2>();
    public void OnSprint(InputAction.CallbackContext context) => SprintHeld = context.ReadValueAsButton();
    public void OnJump(InputAction.CallbackContext context) { if(context.performed) _jumpQueued = true; }
    
    public bool ConsumeJump()
    {
        if(!_jumpQueued) return false;
        _jumpQueued = false;
        return true;
    }
}
