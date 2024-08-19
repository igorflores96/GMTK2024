using UnityEngine;
using UnityEngine.InputSystem;

public class GroundMovementState : PlayerBaseState
{
    private System.Action<InputAction.CallbackContext> _jumpHandler;
    public override void EnterState(PlayerMovement playerContext)
    {
        Debug.Log("Ground State");

        _jumpHandler = (context) => Jump(context, playerContext);
        playerContext.PlayerActions.Movement.Jump.performed += _jumpHandler;
    }

    public override void ExitState(PlayerMovement playerContext)
    {
        playerContext.PlayerActions.Movement.Jump.performed -= _jumpHandler;
    }

    public override void UpdateState(PlayerMovement playerContext)
    {
        playerContext.UpdateRayCast();
        float x = playerContext.PlayerActions.Movement.Move.ReadValue<Vector2>().x;

        float boostX = playerContext.PlayerActions.Movement.Boost.ReadValue<Vector2>().x;
        float boostY = playerContext.PlayerActions.Movement.Boost.ReadValue<Vector2>().y;

        float rotation = boostX + boostY * playerContext.RotationSpeed;

        //playerContext.transform.Translate(new Vector2(x, 0.0f) * playerContext.Speed, Space.World);
        //playerContext.transform.Rotate(new Vector3(0.0f, 0.0f, rotation), Space.Self);
        playerContext.Rb.velocity = new Vector2(x * playerContext.Speed, playerContext.Rb.velocity.y);
        playerContext.Rb.MoveRotation(playerContext.Rb.rotation + rotation * Time.fixedDeltaTime);

    }

    private void Jump(InputAction.CallbackContext context, PlayerMovement playerContext)
    {
        if(context.performed)
        {
            playerContext.Rb.AddForce(Vector2.up * playerContext.JumpForce, ForceMode2D.Impulse);
            playerContext.TransitionState(playerContext.JumpingState);
        }
    }

}
