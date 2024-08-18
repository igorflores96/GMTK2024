using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
   [SerializeField] private PlayerMovement _player;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.layer == 20)
        {
            other.isTrigger = false;
            _player.UpdateCollider(this.transform, other.transform);
            this.gameObject.SetActive(false);
        }
        
    }
}
