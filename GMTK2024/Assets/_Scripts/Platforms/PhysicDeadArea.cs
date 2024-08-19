//using System.Diagnostics;
using UnityEngine;

public class PhysicDeadArea : MonoBehaviour
{
    [SerializeField] private GearType _type;

    private void OnCollisionEnter2D(Collision2D other) 
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.TryGetComponent(out IPlatformerVictim component))
            component.HandleAreaCollision(_type);
    }
}
