using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Parameters")]
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Rigidbody2D _rb;
    
    private PlayerInputActions _playerActions;
    private GroundMovementState _groundState = new GroundMovementState();
    private JumpingMovementState _jumpingState = new JumpingMovementState();
    private PlayerBaseState _currentState;
    private List<Vector2> _occupiedPositions = new List<Vector2>();

    public PlayerInputActions PlayerActions => _playerActions;
    public Rigidbody2D Rb => _rb;
    public float Speed => _speed;
    public float RotationSpeed => _rotationSpeed;
    public float JumpForce => _jumpForce;
    public GroundMovementState GroundState => _groundState;
    public JumpingMovementState JumpingState => _jumpingState; 

    private void OnEnable() 
    {
        _playerActions = new PlayerInputActions();
        _occupiedPositions.Add(Vector2.zero); // Adiciona a posição inicial do jogador

        _currentState = _groundState;
        _currentState.EnterState(this);
        _playerActions.Enable();
    }

    private void OnDisable() 
    {
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

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.layer == 30)
            TransitionState(GroundState);
    } 

    public void UpdateCollider(Transform colliderPosition, Transform ballTransform) 
    {
        if(ballTransform.TryGetComponent(out PlayerBody body))
            body.ActiveColliders();

        
        ballTransform.rotation = Quaternion.identity;
        ballTransform.position = colliderPosition.position;
        ballTransform.SetParent(this.transform, true);

    }

}
