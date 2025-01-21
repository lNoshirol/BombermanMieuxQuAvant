using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private PlayerActions _playerInput;
    [SerializeField] private Vector3 _direction;

    public float _moveSpeed;

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
        _direction = new Vector3(_valueRead.x, 0, _valueRead.y).normalized;
    }

    private void Update()
    {
        transform.position += Time.deltaTime * _moveSpeed * _direction;

        if (_direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * Time.deltaTime);
        }
    }
}