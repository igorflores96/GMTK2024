using UnityEngine;

public class DeadArea : MonoBehaviour
{
    [SerializeField] private GearType _type;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.TryGetComponent(out IPlatformerVictim component))
            component.HandleAreaCollision(_type);
    }
}
