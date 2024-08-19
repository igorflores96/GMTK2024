using UnityEngine;
using UnityEngine.UIElements;

public class MovingPlatforms : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _points;
    private int _index;


    private void Update() 
    {
        if(Vector2.Distance(transform.position, _points[_index].position) < 0.02f)
        {
            _index++;
            if(_index == _points.Length)
                _index = 0;
        }

        transform.position = Vector2.MoveTowards(transform.position, _points[_index].position, _speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {

        foreach (ContactPoint2D contact in other.contacts)
        {
            if (contact.collider.gameObject.TryGetComponent(out IPlatformerVictim component))
            {
                Debug.Log(contact.collider.gameObject.name);
                component.HandlePlatformCollision(other);
                break;
            }
        }
        

    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Transform parentTransform = gameObject.transform;

        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(parentTransform);
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(other.transform.parent != null)
                other.transform.SetParent(null);
        }
    }

}