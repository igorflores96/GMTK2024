using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBody : MonoBehaviour, IPlatformerVictim
{
    [SerializeField] private List<Collider2D> _colliders;
    [SerializeField] private GearType _type;

    private LayerMask _cellingLayer;
    private LayerMask _floorLayer;
    private LayerMask _leftLayer;
    private LayerMask _rightLayer;
    private float _rayCastDistance;

    public GearType CurrentType => _type;

    public void UpdateInfo(LayerMask celling, LayerMask floor, LayerMask left, LayerMask right, float distance)
    {
        _cellingLayer = celling;
        _floorLayer = floor;
        _leftLayer = left;
        _rightLayer = right;
        _rayCastDistance = distance;
    }

    public void ActiveColliders()
    {
        foreach (var collider in _colliders)
        {
            collider.gameObject.SetActive(true);
        }
    }

    public void DesactiveColliders()
    {
        foreach (var collider in _colliders)
        {
            collider.gameObject.SetActive(false);
        }
    }

    public void HandlePlatformCollision(Collision2D collisor)
    {
        Debug.Log(collisor.otherCollider.gameObject.name);
        bool collideDown = collisor.GetContact(0).normal.y > 0 && Physics2D.Raycast(transform.position, Vector2.down, _rayCastDistance, _floorLayer);
        bool collideUp = collisor.GetContact(0).normal.y < 0 && Physics2D.Raycast(transform.position, Vector2.up, _rayCastDistance, _cellingLayer);
        bool collideLeft = collisor.GetContact(0).normal.x > 0 && Physics2D.Raycast(transform.position, Vector2.left, _rayCastDistance, _leftLayer);
        bool collideRight = collisor.GetContact(0).normal.x < 0 && Physics2D.Raycast(transform.position, Vector2.right, _rayCastDistance, _rightLayer);

        if(collideDown || collideUp || collideLeft || collideRight)
        {
            FindObjectOfType<AudioManager>().Play("Die");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * _rayCastDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position - Vector3.right * _rayCastDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * _rayCastDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position - Vector3.up * _rayCastDistance);
    }

    public void HandleAreaCollision(GearType typeArea)
    {
        if (typeArea != _type)
        {
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("Disconnect");
        }


    }
}


public enum GearType
{
    Normal, Light, Hot, DeadZone
}