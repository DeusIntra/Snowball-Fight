using UnityEngine;

[System.Serializable]
public class UIAnimationData
{
    public float speed = 1f;
    public Vector2 startPosition;
    public Vector2 endPosition;
    public AnimationCurve positionOverTime;

    [Space]
    public bool setAnchorsAfterAnimation = false;
    public Vector2 min;
    public Vector2 max;
}
