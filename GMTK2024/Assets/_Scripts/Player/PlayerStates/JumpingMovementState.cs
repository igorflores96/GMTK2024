using UnityEngine;

public class JumpingMovementState : PlayerBaseState
{
    public override void EnterState(PlayerMovement playerContext)
    {
        Debug.Log("Jumping State");
    }

    public override void ExitState(PlayerMovement playerContext)
    {
        
    }

    public override void UpdateState(PlayerMovement playerContext)
    {
        float x = playerContext.PlayerActions.Movement.Move.ReadValue<Vector2>().x;

        float boostX = playerContext.PlayerActions.Movement.Boost.ReadValue<Vector2>().x;
        float boostY = playerContext.PlayerActions.Movement.Boost.ReadValue<Vector2>().y;

        float rotation = boostX + boostY * playerContext.RotationSpeed;

        playerContext.transform.Translate(new Vector2(x, 0.0f) * playerContext.Speed, Space.World);
        playerContext.transform.Rotate(new Vector3(0.0f, 0.0f, rotation), Space.Self);
    }

}
