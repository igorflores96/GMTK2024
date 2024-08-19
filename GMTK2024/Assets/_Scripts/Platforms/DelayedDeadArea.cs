using UnityEngine;

public class DelayedDeadArea : MonoBehaviour
{
    [SerializeField] private GearType _type;

    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.gameObject.TryGetComponent(out IPlatformerVictim component))
            component.HandleAreaCollision(_type);
    }
}
