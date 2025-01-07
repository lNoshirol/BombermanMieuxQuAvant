using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private PlayerActions _playerInput;
    [SerializeField] Vector3 _direction;

    public float _moveSpeed;
    private Vector3 _moveInput;



    private void Awake()
    {
        _playerInput = new PlayerActions();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        var _valueRead = callbackContext.ReadValue<Vector2>();
        _direction = new Vector3(_valueRead.x, 0, _valueRead.y);
    }

    public void Update()
    {
        transform.position += Time.deltaTime * _moveSpeed * _direction;
    }
}
