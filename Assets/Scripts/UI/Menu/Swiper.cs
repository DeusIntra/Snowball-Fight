using UnityEngine;

public class Swiper : MonoBehaviour
{
    public SwipeDetector swipeDetector;

    private LocationsRoll _locationsRoll;

    private void Awake()
    {
        _locationsRoll = GetComponent<LocationsRoll>();
    }

    private void OnEnable()
    {
        swipeDetector.onTouchDetected += direction => Swipe(direction);
        swipeDetector.enabled = true;
    }

    private void OnDisable()
    {
        swipeDetector.onTouchDetected -= direction => Swipe(direction);
        swipeDetector.enabled = false;
    }

    private void Swipe(Vector2 direction)
    {
        if (direction.x > 0) _locationsRoll.Previous();
        else if (direction.x < 0) _locationsRoll.Next();        
    }
}
