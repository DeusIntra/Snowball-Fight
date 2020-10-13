using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 0.05f;
    public GameObject snowballPrefab;

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0);
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0);
        }
        */

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(speed, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Instantiate(snowballPrefab, transform.position, Quaternion.identity);
        }
    }
}
