using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input : Singleton<Input>
{
    #region Events
    public delegate void StartTouch(Vector2 position, float time);
    public delegate void EndTouch(Vector2 position, float time);

    public event StartTouch onStartTouch;
    public event EndTouch onEndTouch;
    #endregion

    private GameControls _controls;

    private void Awake()
    {
        _controls = new GameControls();
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    private void Start()
    {
        _controls.Menu.Touch.started += ctx => StartTouchPrimary(ctx);
        _controls.Menu.Touch.canceled += ctx => EndTouchPrimary(ctx);
    }

    private void StartTouchPrimary(InputAction.CallbackContext ctx)
    {
        if (onStartTouch == null) return;

        Vector2 position = _controls.Menu.TouchPosition.ReadValue<Vector2>();
        onStartTouch(position, (float)ctx.startTime);
    }

    private void EndTouchPrimary(InputAction.CallbackContext ctx)
    {
        if (onEndTouch == null) return;

        Vector2 position = _controls.Menu.TouchPosition.ReadValue<Vector2>();
        onEndTouch(position, (float)ctx.time);
    }
}
