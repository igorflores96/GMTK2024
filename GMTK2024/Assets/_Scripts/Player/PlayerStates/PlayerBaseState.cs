public abstract class PlayerBaseState
{
    public abstract void EnterState(PlayerMovement playerContext);
    public abstract void UpdateState(PlayerMovement playerContext);
    public abstract void ExitState(PlayerMovement playerContext);

}
