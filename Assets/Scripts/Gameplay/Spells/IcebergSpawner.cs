using UnityEngine;

public class IcebergSpawner : MonoBehaviour
{
    public float speed = 1f;

    public Transform endPosition;
    public GameObject icebergPrefab;


    public void Cast()
    {
        GameObject iceberg = Instantiate(icebergPrefab, transform.position, icebergPrefab.transform.rotation);
        Car car = iceberg.GetComponent<Car>();

        if (car != null)
        {
            car.SetDestination(endPosition.position, speed);
        }
    }
}
