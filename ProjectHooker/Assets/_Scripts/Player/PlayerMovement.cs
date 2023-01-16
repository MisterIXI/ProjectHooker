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
    private bool _isJumpPressed;
    private float _jumpPressTime;
    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void OnLeftRightInput(InputAction.CallbackContext context) {
        _moveInput = context.ReadValue<float>();
    }

    public void OnJumpInput(InputAction.CallbackContext context) {
        if (context.started) {
            _isJumpPressed = true;
            _jumpPressTime = Time.time;
        }
        if (context.canceled) {
            _isJumpPressed = false;
        }
    }
}