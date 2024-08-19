using UnityEngine;
using UnityEngine.InputSystem;

public class CellingMovementState : PlayerBaseState
{


    public override void EnterState(PlayerMovement playerContext)
    {
        playerContext.Rb.gravityScale = 0.0f;
        playerContext.Rb.velocity = Vector2.zero;
    }

    public override void ExitState(PlayerMovement playerContext)
    {
        playerContext.Rb.gravityScale = 1.0f;
    }

    public override void UpdateState(PlayerMovement playerContext)
    {
        playerContext.UpdateRayCast();
        float y = playerContext.PlayerActions.Movement.Move.ReadValue<Vector2>().y;
        float x = playerContext.PlayerActions.Movement.Move.ReadValue<Vector2>().x;
    
        float boostX = playerContext.PlayerActions.Movement.Boost.ReadValue<Vector2>().x;
        float boostY = playerContext.PlayerActions.Movement.Boost.ReadValue<Vector2>().y;

        float rotation = boostX + boostY * playerContext.RotationSpeed;

        if(y < 0)
            playerContext.TransitionState(playerContext.JumpingState);
        
        //playerContext.transform.Translate(new Vector2(x, 0.0f) * playerContext.Speed, Space.World);
        playerContext.Rb.velocity = new Vector2(x * playerContext.Speed, 0.0f);
        playerContext.Rb.MoveRotation(playerContext.Rb.rotation + rotation * Time.fixedDeltaTime);
        //playerContext.transform.Rotate(new Vector3(0.0f, 0.0f, rotation), Space.Self);
    }
}
