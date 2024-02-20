using GameInput;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float rotateSpeed;

    [SerializeField] private float maxHeight;
    [SerializeField] private float minHeight;

    private Vector2 _moveDirection;
    private float _zoomValue;
    private bool _rotateButtonPressed;
    private Vector3 _mouseStartPosition;
    
    private void OnEnable()
    {
        InputReader.CameraMoveEvent += HandleMove;
        InputReader.CameraZoomEvent += HandleZoom;
        InputReader.CameraRotateButtonEvent += HandleRotate;
    }
    
    private void OnDisable()
    {
        InputReader.CameraMoveEvent -= HandleMove;
        InputReader.CameraZoomEvent += HandleZoom;
        InputReader.CameraRotateButtonEvent += HandleRotate;
    }

    private void HandleMove(Vector2 moveDirection)
    {
        _moveDirection = moveDirection;
    }
    
    private void HandleZoom(float zoomValue)
    {
        _zoomValue = zoomValue;
    }
    
    private void HandleRotate(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _rotateButtonPressed = true;
            _mouseStartPosition = Input.mousePosition;
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            _rotateButtonPressed = false;
        }
    }


    private void Update()
    {
        // Left and right
        var lateralMove = _moveDirection.x * moveSpeed * transform.right;
        
        // forward and backward
        var forwardMove = transform.forward;
        forwardMove.y = 0;
        forwardMove.Normalize();
        forwardMove *= _moveDirection.y * moveSpeed;
        
        // up and down
        var verticalValue = Mathf.Log(transform.position.y) * -zoomSpeed * _zoomValue;
        var verticalMove = new Vector3(0, verticalValue, 0);
        
        // check max and min height
        if (transform.position.y >= maxHeight && verticalMove.y > 0)
        {
            verticalMove.y = 0;
        }
        else if (transform.position.y <= minHeight && verticalMove.y < 0)
        {
            verticalMove.y = 0;
        }
        
        // total move amount 
        var move = lateralMove + forwardMove + verticalMove;

        transform.position += move;
        
        Rotate();
    }

    private void Rotate()
    {
        if (!_rotateButtonPressed)
            return;

        var mousePosition = Input.mousePosition;

        var dx = (mousePosition - _mouseStartPosition).x * rotateSpeed;
        var dy = (mousePosition - _mouseStartPosition).y * rotateSpeed;

        transform.rotation *= Quaternion.Euler(new Vector3(0, dx, 0));
        transform.GetChild(0).transform.rotation *= Quaternion.Euler(new Vector3(-dy, 0, 0));

        _mouseStartPosition = mousePosition;
    }
}
