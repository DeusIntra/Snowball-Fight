using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float multiplierX = 2f;
    public float multiplierY = 0.5f;
    public float speed;

    private float _random1;
    private float _random2;
    private Vector3 _startPosition;
    private float time;

    private void Start()
    {
        _random1 = Random.Range(0f, 100f);
        _random2 = Random.Range(0f, 100f);

        _startPosition = transform.position;
    }

    private void Update()
    {
        time += Time.deltaTime * speed;
        float offsetX = (Mathf.PerlinNoise(_random1, time) - 0.5f) * 2f;
        float offsetY = (Mathf.PerlinNoise(_random2, time) - 0.5f) * 2f;

        transform.position = _startPosition + new Vector3(offsetX * multiplierX, offsetY * multiplierY);
    }
}
