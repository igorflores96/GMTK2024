using UnityEngine;

public class JumpingMovementState : PlayerBaseState
{
    float time = 0.0f;
    public override void EnterState(PlayerMovement playerContext)
    {
        time = 0.0f;
    }

    public override void ExitState(PlayerMovement playerContext)
    {
        time = 0.0f;
    }

    public override void UpdateState(PlayerMovement playerContext)
    {
        time += Time.deltaTime;

        if(time > 0.1f)
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

}
