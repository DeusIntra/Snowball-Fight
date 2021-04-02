using UnityEngine;

public class TestCube : MonoBehaviour
{
    public float length = 5f;
    public float speed = 1f;

    private float t = 0;

    void Update()
    {
        float x = Mathf.PingPong(t, length * 2) - length;
        transform.position = new Vector3(x, 0, 0);
        t += Time.deltaTime * speed;
    }
}
