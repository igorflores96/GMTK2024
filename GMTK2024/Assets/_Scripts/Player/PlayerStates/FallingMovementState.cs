using UnityEngine;

public class FallingMovementState : PlayerBaseState
{
    public override void EnterState(PlayerMovement playerContext)
    {

    }

    public override void ExitState(PlayerMovement playerContext)
    {

    }

    public override void UpdateState(PlayerMovement playerContext)
    {
        playerContext.UpdateRayCast();
        playerContext.Rb.velocity = new Vector2(playerContext.Rb.velocity.x, -0.1f * playerContext.Speed);

        float boostX = playerContext.PlayerActions.Movement.Boost.ReadValue<Vector2>().x;
        float boostY = playerContext.PlayerActions.Movement.Boost.ReadValue<Vector2>().y;

        float rotation = boostX + boostY * playerContext.RotationSpeed;
        playerContext.Rb.MoveRotation(playerContext.Rb.rotation + rotation * Time.fixedDeltaTime);

    }

}
