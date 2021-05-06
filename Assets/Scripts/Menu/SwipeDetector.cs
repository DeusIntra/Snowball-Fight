using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    #region Events
    public delegate void TouchDetectedDelegate(Vector2 direction);
    public event TouchDetectedDelegate onTouchDetected;
    #endregion

    public float minDistance = 50f;
    public float maxTime = 1f;

    private TouchInput _input;
    private Vector2 _startPosition;
    private float _startTime;

    private void Awake()
    {
        _input = TouchInput.Instance;
    }

    private void OnEnable()
    {
        _input.onTouchStarted += TouchStarted;
        _input.onTouchEnded += TouchEnded;
    }

    private void OnDisable()
    {
        _input.onTouchStarted -= TouchStarted;
        _input.onTouchStarted -= TouchEnded;
    }

    private void TouchStarted(Vector2 position, float time)
    {
        _startPosition = position;
        _startTime = time;
    }
    private void TouchEnded(Vector2 position, float time)
    {
        float posDelta = Vector2.Distance(_startPosition, position);
        float timeDelta = time - _startTime;
        if (posDelta <= minDistance || timeDelta >= maxTime)
        {
            onTouchDetected?.Invoke(Vector2.zero);
            return;
        }

        var direction = (position - _startPosition).normalized;
        var axis = CalculateAxis(direction);
        onTouchDetected?.Invoke(axis);
    }

    private Vector2 CalculateAxis(Vector2 direction)
    {
        
        if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
        {
            if (direction.y < 0) return new Vector2(0, -1);
            return new Vector2(0, 1);
        }
        else
        {
            if (direction.x < 0) return new Vector2(-1, 0);
            return new Vector2(1, 0);
        }
    }
}
