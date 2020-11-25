using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public float speed = 1f;
    public float minSpawnDelaySeconds = 2f;
    public float maxSpawnDelaySeconds = 7f;

    public bool randomAtStart = false;

    public List<GameObject> carPrefabs;

    public Transform startPosition;
    public Transform endPosition;

    private float _time;


    private void Start()
    {
        startPosition.LookAt(endPosition);

        if (randomAtStart)
        {
            int count = Random.Range(2, 5);

            for (int i = 0; i < count; i++)
            {
                float t = Random.Range(0f, 1f);

                Spawn(startPosition.position, endPosition.position, t);
            }
        }
    }


    private void Update()
    {
        if (_time <= 0)
        {
            Spawn(startPosition.position, endPosition.position);
            _time = Random.Range(minSpawnDelaySeconds, maxSpawnDelaySeconds);
        }

        _time -= Time.deltaTime;
    }


    private void Spawn(Vector3 startPos, Vector3 endPos, float t = 0)
    {
        int randIndex = Random.Range(0, carPrefabs.Count);

        Vector3 direction = (startPos - endPos).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        GameObject carGO = Instantiate(carPrefabs[randIndex], startPos, lookRotation);

        Car car = carGO.GetComponent<Car>();

        if (car != null)
        {
            car.SetDestination(endPos, speed, t);
        }
    }
}
