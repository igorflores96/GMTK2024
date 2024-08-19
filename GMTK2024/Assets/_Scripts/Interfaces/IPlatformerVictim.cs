using UnityEngine;

public interface IPlatformerVictim
{
    void HandlePlatformCollision(Collision2D collisor);
    void HandleAreaCollision(GearType typeArea);

}
