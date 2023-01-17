using UnityEngine;
using System;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class InputManager : MonoBehaviour
{
    private void Awake()
    {
        if (RefManager.inputManager != null)
        {
            Destroy(gameObject);
            return;
        }
        RefManager.inputManager = this;
    }
    private PlayerInput _playerInput;
    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        var playerInputActions = _playerInput.actions;
        SubscribeToActionMap(playerInputActions);
    }

    public Action<CallbackContext> OnLeftRight = delegate { };
    public Action<CallbackContext> OnUpDown = delegate { };
    public Action<CallbackContext> OnPrimary = delegate { };
    public Action<CallbackContext> OnSecondary = delegate { };
    public Action<CallbackContext> OnAim = delegate { };
    public Action<CallbackContext> OnRun = delegate { };
    public Action<CallbackContext> OnJump = delegate { };

    public Action<CallbackContext> OnHelperVision = delegate { };
    public Action<CallbackContext> OnPause = delegate { };
    private void SubscribeToActionMap(InputActionAsset playerInputActions)
    {
        playerInputActions["LeftRight"].started += OnLeftRightInput;
        playerInputActions["LeftRight"].performed += OnLeftRightInput;
        playerInputActions["LeftRight"].canceled += OnLeftRightInput;
        playerInputActions["UpDown"].started += OnUpDownInput;
        playerInputActions["UpDown"].performed += OnUpDownInput;
        playerInputActions["UpDown"].canceled += OnUpDownInput;
        playerInputActions["Primary"].started += OnPrimaryInput;
        playerInputActions["Primary"].performed += OnPrimaryInput;
        playerInputActions["Primary"].canceled += OnPrimaryInput;
        playerInputActions["Secondary"].started += OnSecondaryInput;
        playerInputActions["Secondary"].performed += OnSecondaryInput;
        playerInputActions["Secondary"].canceled += OnSecondaryInput;
        playerInputActions["Aim"].started += OnAimInput;
        playerInputActions["Aim"].performed += OnAimInput;
        playerInputActions["Aim"].canceled += OnAimInput;
        playerInputActions["Run"].started += OnRunInput;
        playerInputActions["Run"].performed += OnRunInput;
        playerInputActions["Run"].canceled += OnRunInput;
        playerInputActions["Jump"].started += OnJumpInput;
        playerInputActions["Jump"].performed += OnJumpInput;
        playerInputActions["Jump"].canceled += OnJumpInput;
        playerInputActions["HelperVision"].started += OnHelperVisionInput;
        playerInputActions["HelperVision"].performed += OnHelperVisionInput;
        playerInputActions["HelperVision"].canceled += OnHelperVisionInput;
        playerInputActions["Pause"].started += OnPauseInput;
        playerInputActions["Pause"].performed += OnPauseInput;
        playerInputActions["Pause"].canceled += OnPauseInput;
    }
    public void OnLeftRightInput(CallbackContext context)
    {
        OnLeftRight(context);
    }

    public void OnUpDownInput(CallbackContext context)
    {
        OnUpDown(context);
    }

    public void OnPrimaryInput(CallbackContext context)
    {
        OnPrimary(context);
    }

    public void OnSecondaryInput(CallbackContext context)
    {
        OnSecondary(context);
    }

    public void OnAimInput(CallbackContext context)
    {
        OnAim(context);
    }

    public void OnRunInput(CallbackContext context)
    {
        OnRun(context);
    }

    public void OnJumpInput(CallbackContext context)
    {
        OnJump(context);
    }
    
    public void OnHelperVisionInput(CallbackContext context)
    {
        OnHelperVision(context);
    }

    public void OnPauseInput(CallbackContext context)
    {
        OnPause(context);
    }



}