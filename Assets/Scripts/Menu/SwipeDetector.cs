using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    #region Events
    public delegate void DetectSwipeDelegate(Vector2 direction);
    public event DetectSwipeDelegate onDetectSwipe;
    #endregion

    public float minDistance;
    public float maxTime;

    private Input _input;

    private Vector2 _startPosition;
    private float _startTime;

    private Vector2 _endPosition;
    private float _endTime;

    private void Awake()
    {
        _input = Input.Instance;
    }

    private void OnEnable()
    {
        _input.onStartTouch += StartSwipe;
        _input.onEndTouch += EndSwipe;
    }

    private void OnDisable()
    {
        _input.onStartTouch -= StartSwipe;
        _input.onEndTouch -= EndSwipe;
    }

    private void EndSwipe(Vector2 position, float time)
    {
        _startPosition = position;
        _startTime = time;
    }

    private void StartSwipe(Vector2 position, float time)
    {
        _endPosition = position;
        _endTime = time;
        Vector2 direction = DetectSwipe();
        onDetectSwipe(direction);
        Debug.Log(direction);
    }

    private Vector2 DetectSwipe()
    {
        Debug.Log("distance " + Vector2.Distance(_startPosition, _endPosition));
        bool tooShort = Vector2.Distance(_startPosition, _endPosition) <= minDistance;
        bool tooFast = (_endTime - _startTime) >= maxTime;
        if (tooShort || tooFast) return Vector2.zero;

        Vector2 direction = (_endPosition - _startPosition).normalized;

        // horizontal swipe
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0) return new Vector2(1, 0);
            else return new Vector2(-1, 0);
        }
        // vertical swipe
        else
        {
            if (direction.y > 0) return new Vector2(0, 1);
            else return new Vector2(0, -1);
        }
    }
}
