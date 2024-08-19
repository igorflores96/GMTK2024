using UnityEngine;
using UnityEngine.InputSystem;

public class WallRightMovement : PlayerBaseState
{
    private System.Action<InputAction.CallbackContext> _jumpHandler;

    public override void EnterState(PlayerMovement playerContext)
    {
        Debug.Log("Wall State");

        _jumpHandler = (context) => Jump(context, playerContext);
        playerContext.PlayerActions.Movement.Jump.performed += _jumpHandler;
        playerContext.Rb.velocity = Vector2.zero;
        playerContext.Rb.gravityScale = 0.0f;
    }

    public override void ExitState(PlayerMovement playerContext)
    {
        playerContext.Rb.gravityScale = 1.0f;
        playerContext.PlayerActions.Movement.Jump.performed -= _jumpHandler;
    }

    public override void UpdateState(PlayerMovement playerContext)
    {
        playerContext.UpdateRayCast();
        float y = playerContext.PlayerActions.Movement.Move.ReadValue<Vector2>().y;
        float x = playerContext.PlayerActions.Movement.Move.ReadValue<Vector2>().x;
    
        float boostX = playerContext.PlayerActions.Movement.Boost.ReadValue<Vector2>().x;
        float boostY = playerContext.PlayerActions.Movement.Boost.ReadValue<Vector2>().y;

        float rotation = boostX + boostY * playerContext.RotationSpeed;

        if(x < 0)
            playerContext.TransitionState(playerContext.JumpingState);
        
        playerContext.Rb.velocity = new Vector2(0.0f, (y + x) * playerContext.Speed);
        //playerContext.transform.Translate(new Vector2(0.0f, y + x) * playerContext.Speed, Space.World);
        //playerContext.transform.Rotate(new Vector3(0.0f, 0.0f, rotation), Space.Self);
        playerContext.Rb.MoveRotation(playerContext.Rb.rotation + rotation * Time.fixedDeltaTime);

    }

    private void Jump(InputAction.CallbackContext context, PlayerMovement playerContext)
    {
        if(context.performed)
        {
            playerContext.Rb.AddForce(Vector2.left * playerContext.JumpForce, ForceMode2D.Impulse);
            playerContext.TransitionState(playerContext.JumpingState);
        }
    }
}
