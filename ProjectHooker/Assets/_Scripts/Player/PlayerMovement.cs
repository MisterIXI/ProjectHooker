using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerController))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float _moveInput;
    private bool _isJumpInputDown;
    private bool _isJumpInputBuffered;
    private float _jumpPressTime;
    private bool _coyoteFlag;
    private bool _isJumping;
    private bool _isSprintPressed;
    private PlayerSettings _playerSettings;
    private PlayerController _playerController;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerSettings = RefManager.gameManager.PlayerSettings;
        _playerController = GetComponent<PlayerController>();
        InputManager input = RefManager.inputManager;
        input.OnLeftRight += OnLeftRightInput;
        input.OnJump += OnJumpInput;
        input.OnRun += OnRunInput;
    }

    private void Update()
    {
        BufferedJumpCheck();
        CoyoteResetCheck();
    }
    private void FixedUpdate()
    {
        MoveSideways();
    }

    private void MoveSideways()
    {
        float currentVelocity = _rb.velocity.x;
        float targetVelocity = _moveInput * _playerSettings.MaxMoveSpeed;

        float acceleration = 0;

        // if we are accelerating
        if ((currentVelocity <= 0 && targetVelocity < currentVelocity) || (currentVelocity >= 0 && targetVelocity > currentVelocity))
        {
            if (_playerController.IsGrounded)
                acceleration = _playerSettings.Acceleration;
            else
                acceleration = _playerSettings.AirAcceleration;
        }
        else
        {
            if (_playerController.IsGrounded)
                acceleration = _playerSettings.Deceleration;
            else
                acceleration = _playerSettings.AirDeceleration;
        }

        _rb.velocity = new Vector2(Mathf.MoveTowards(currentVelocity, targetVelocity, acceleration * 50 * Time.fixedDeltaTime), _rb.velocity.y);
    }

    private void BufferedJumpCheck()
    {
        if (_isJumpInputBuffered)
        {
            if (Time.time - _jumpPressTime < _playerSettings.JumpBufferTime)
            {
                if (_playerController.IsGrounded)
                {
                    Jump();
                }
            }
            else
            { // if we are not in the buffer window
                _isJumpInputBuffered = false;
            }
        }
    }

    private void CoyoteResetCheck()
    {
        if (_coyoteFlag)
        {
            if (_playerController.IsGrounded && _rb.velocity.y < 0.1f)
            {
                _coyoteFlag = false;
            }
        }
    }
    private void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, _playerSettings.JumpVelocity);
        if (_isJumpInputDown)
            _isJumping = true;
        _coyoteFlag = true;
    }
    public void OnLeftRightInput(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<float>();
    }
    private bool CheckForGroundedAndCoyote()
    {
        if (_playerController.IsGrounded)
            return true;
        if (!_coyoteFlag && Time.time - _playerController.LastGroundedTime < _playerSettings.CoyoteTime)
            return true;
        return false;
    }
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _isJumpInputDown = true;
            if (CheckForGroundedAndCoyote())
            {
                Jump();
            }
            else
            {
                _jumpPressTime = Time.time;
                _isJumpInputBuffered = true;
            }
        }
        if (context.canceled)
        {
            if (_isJumping)
                _rb.velocity = new(_rb.velocity.x, Mathf.Min(_rb.velocity.y, _playerSettings.JumpVelocity * _playerSettings.VarHeightModifier));
            _isJumping = false;
            _isJumpInputDown = false;
        }
    }

    public void OnRunInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _isSprintPressed = true;
        }
        if (context.canceled)
        {
            _isSprintPressed = false;
        }
    }
}