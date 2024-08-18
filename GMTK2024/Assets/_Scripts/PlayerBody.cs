using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    [SerializeField] private List<Collider2D> _colliders;

    public void ActiveColliders()
    {
        foreach (var collider in _colliders)
        {
            collider.gameObject.SetActive(true);
        }
    }
}
