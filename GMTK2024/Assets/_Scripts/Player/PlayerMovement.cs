using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Parameters")]
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _rayCastDistance;
    [SerializeField] private List<Collider2D> _colliders;

    [Header("Layers Parameters")]
    [SerializeField] private LayerMask _cellingLayer;
    [SerializeField] private LayerMask _floorLayer;
    [SerializeField] private LayerMask _leftLayer;
    [SerializeField] private LayerMask _rightLayer;
    
    private PlayerInputActions _playerActions;
    private GroundMovementState _groundState = new GroundMovementState();
    private JumpingMovementState _jumpingState = new JumpingMovementState();
    private WallLeftMovement _wallLeftState = new WallLeftMovement();
    private WallRightMovement _wallRightState = new WallRightMovement();
    private FallingMovementState _fallingState = new FallingMovementState();
    private CellingMovementState _cellingState = new CellingMovementState();


    private PlayerBaseState _currentState;

    public PlayerInputActions PlayerActions => _playerActions;
    public Rigidbody2D Rb => _rb;
    public float Speed => _speed;
    public float RotationSpeed => _rotationSpeed;
    public float JumpForce => _jumpForce;
    public GroundMovementState GroundState => _groundState;
    public JumpingMovementState JumpingState => _jumpingState; 
    public WallLeftMovement WallLeftState => _wallLeftState;
    public WallRightMovement WallRightState => _wallRightState;
    public FallingMovementState FallingState => _fallingState; 
    public CellingMovementState CellingState => _cellingState; 




    private void OnEnable() 
    {
        _playerActions = new PlayerInputActions();
        _currentState = _groundState;
        _currentState.EnterState(this);

        _playerActions.Movement.Binding.performed += UnbindingBody;
        _playerActions.Enable();
    }

    private void OnDisable() 
    {
        _playerActions.Movement.Binding.performed -= UnbindingBody;

        _playerActions.Disable();
    }

    private void FixedUpdate()
    {
        _currentState.UpdateState(this);
    }

    public void TransitionState(PlayerBaseState newState)
    {
        _currentState.ExitState(this);
        _currentState = newState;
        _currentState.EnterState(this);
    }

    public void UpdateRayCast()
    {
        if (Physics2D.Raycast(transform.position, Vector2.left, _rayCastDistance, _leftLayer))
        {
            TransitionState(WallLeftState);
        }
        else if (Physics2D.Raycast(transform.position, Vector2.right, _rayCastDistance, _rightLayer))
        {
            TransitionState(WallRightState);
        }
        else if (Physics2D.Raycast(transform.position, Vector2.up, _rayCastDistance, _cellingLayer))
        {
            TransitionState(CellingState);
        }
        else if (Physics2D.Raycast(transform.position, Vector2.down, _rayCastDistance, _floorLayer))
        {
            TransitionState(GroundState);
        }
        else
            TransitionState(GroundState);

    }
    
    public void UpdateCollider(Transform colliderPosition, Transform ballTransform) 
    {
        if(ballTransform.TryGetComponent(out PlayerBody body))
            body.ActiveColliders();

        ballTransform.rotation = Quaternion.identity;
        ballTransform.position = colliderPosition.position;
        ballTransform.SetParent(this.transform, true);
        ballTransform.localRotation = Quaternion.identity;
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

    private void UnbindingBody(InputAction.CallbackContext context)
    {
        List<Transform> childrenToUnbind = new List<Transform>();

        foreach (Transform child in transform)
        {
            childrenToUnbind.Add(child);
        }

        foreach (Transform child in childrenToUnbind)
        {
            if (child.TryGetComponent(out PlayerBody body))
            {
                body.DesactiveColliders();
                child.SetParent(null);
            }
        }

        StartCoroutine(ActiveCollider());
    }

    IEnumerator ActiveCollider()
    {
        yield return new WaitForSeconds(0.5f);

        foreach(Collider2D collider in _colliders)
            collider.gameObject.SetActive(true);

    }

}
