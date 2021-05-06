using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class TouchInput : Singleton<TouchInput>
{
    #region Events
    public delegate void TouchStartedDelegate(Vector2 position, float time);
    public delegate void TouchEndedDelegate(Vector2 position, float time);

    public event TouchStartedDelegate onTouchStarted;
    public event TouchEndedDelegate onTouchEnded;
    #endregion
    
    private GameControls _controls;

    private void Awake()
    {
        _controls = new GameControls();

        _controls.Menu.Press.started += ctx => TouchStarted(ctx);
        _controls.Menu.Press.canceled += ctx => TouchCancelled(ctx);
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    private void TouchStarted(InputAction.CallbackContext ctx)
    {
        var touchPosition = _controls.Menu.Point.ReadValue<Vector2>();

        onTouchStarted?.Invoke(touchPosition, (float)ctx.startTime);
    }

    private void TouchCancelled(InputAction.CallbackContext ctx)
    {
        var touchPosition = _controls.Menu.Point.ReadValue<Vector2>();

        onTouchEnded?.Invoke(touchPosition, (float)ctx.time);
    }
}
