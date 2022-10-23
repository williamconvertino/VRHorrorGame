
using UnityEngine;

public class PlayerDesktopMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterController _characterControllerRef;
    [SerializeField] private Camera _playerCamera;

    #region Core

    private void Start()
    {
        LockCursor();
    }

    private void Update()
    {
        UpdateDirection();
        UpdateVelocity();
        UpdateGravity();
        Move();
    }

    #endregion

    #region Cursor

    private bool _isCursorLocked = false;
    private void UpdateCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isCursorLocked)
            {
                UnlockCursor();
            }
            else
            {
                LockCursor();
            }
        }
    }
    
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _isCursorLocked = true;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _isCursorLocked = false;
    }

    #endregion
    
    #region Movement

    [Header("Moving")]
    [SerializeField] private float _playerWalkingSpeed = 7.5f;
    [SerializeField] private float _playerRunningSpeed = 11.5f;
    [SerializeField] private float _lookSpeed = 2.0f;
    [SerializeField] private float _lookLimitX = 45.0f;
    [SerializeField] private float _jumpForce = 8.0f;

    private Vector3 _velocity = Vector3.zero;
    private Vector3 _lookDirection = Vector3.zero;
    private void UpdateDirection()
    {
        
        _lookDirection.x += -Input.GetAxis("Mouse Y") * _lookSpeed;
        _lookDirection.x = Mathf.Clamp(_lookDirection.x, -_lookLimitX, _lookLimitX);
        _lookDirection.y += Input.GetAxis("Mouse X") * _lookSpeed;
        _playerCamera.transform.eulerAngles = _lookDirection;
        _characterControllerRef.transform.eulerAngles = new Vector3(0, _lookDirection.y, 0);
    }

    private void UpdateVelocity()
    {
        float currentMoveSpeed = Input.GetKeyDown(KeyCode.LeftShift) ? _playerRunningSpeed : _playerWalkingSpeed;
        Vector3 direction = Input.GetAxis("Vertical") * transform.TransformDirection(Vector3.forward) +
                            Input.GetAxis("Horizontal") * transform.TransformDirection(Vector3.right);
        direction.Normalize(); 
        _velocity = currentMoveSpeed * direction;

        if (_characterControllerRef.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _velocity.y = _jumpForce;
        }
    }
    private void Move()
    {
        _characterControllerRef.Move(_velocity * Time.deltaTime);
    }

    #endregion

    #region Gravity

    [Header("Gravity")]
    [SerializeField] private float _gravityScale = 20.0f;
    [SerializeField] private float _maxFallSpeed = 20.0f;
    private void UpdateGravity()
    {
        _velocity.y -= _gravityScale * Time.deltaTime;
        if (_characterControllerRef.isGrounded)
        {
            _velocity.y = 0;
        }
        _velocity.y = Mathf.Max(_velocity.y, -_maxFallSpeed);
    }
    
    #endregion
    
}