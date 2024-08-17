using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private Rigidbody2D _rb;
    private PlayerInputActions _playerActions;

    private void OnEnable() 
    {
        _playerActions = new PlayerInputActions();

        _playerActions.Enable();

    }

    private void OnDisable() 
    {
        _playerActions.Disable();
    }

    private void FixedUpdate()
    {
        float x = _playerActions.Movement.Move.ReadValue<Vector2>().x;
        float y = _playerActions.Movement.Move.ReadValue<Vector2>().y;

        float boostX = _playerActions.Movement.Boost.ReadValue<Vector2>().x;
        float boostY = _playerActions.Movement.Boost.ReadValue<Vector2>().y;

        transform.Translate(new Vector2(x, y) * 0.1f, Space.World);
        transform.Rotate(new Vector3(0.0f, 0.0f, boostX + boostY * 5.0f));
    }


}
