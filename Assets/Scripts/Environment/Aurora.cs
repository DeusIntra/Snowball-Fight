using UnityEngine;

public class Aurora : MonoBehaviour
{
    public float speed;
    public float range;

    private float _time = 0;
    private float _startY;


    private void Start()
    {
        _time = Random.Range(-10000, 10000);
        _startY = transform.position.y;
    }


    private void Update()
    {
        _time += Time.deltaTime;
        float offsetY = Mathf.PerlinNoise(transform.position.x, _time * speed) * range;

        transform.position = new Vector3(transform.position.x, _startY + offsetY, transform.position.z);
    }
}
