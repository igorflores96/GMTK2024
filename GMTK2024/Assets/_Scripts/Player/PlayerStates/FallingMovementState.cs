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
        playerContext.Rb.velocity = new Vector2(playerContext.Rb.velocity.x, -0.5f * playerContext.Speed);
    }

}
