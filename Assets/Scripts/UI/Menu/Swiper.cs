using System.Collections;
using System.Collections.Generic;
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
        swipeDetector.onDetectSwipe += Swipe;
    }

    private void OnDisable()
    {
        swipeDetector.onDetectSwipe -= Swipe;
    }

    private void Swipe(Vector2 direction)
    {
        if (direction.x == 0) return;
        if (direction.x > 0) _locationsRoll.Previous();
        else _locationsRoll.Next();
    }

}
