using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;
public class Grapple : MonoBehaviour
{
    [SerializeField] private RopeVerlet _ropeVerlet;
    private float _updownInput;
    private void Start()
    {
        InputManager input = RefManager.inputManager;
        input.OnUpDown += OnUpDownInput;
    }

    private void FixedUpdate()
    {
        _ropeVerlet.RopeSegmentLength = Mathf.Clamp(_ropeVerlet.RopeSegmentLength + _updownInput * Time.deltaTime * 2f, 0.1f, 10f);
    }
    public void OnUpDownInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("UpDown" + context.ReadValue<float>());
            _updownInput = context.ReadValue<float>();
        }
        if(context.canceled)
        {
            _updownInput = 0;
        }
    }
}