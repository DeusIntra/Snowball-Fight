using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public float speed = 1f;
    public float minSpawnDelaySeconds = 2f;
    public float maxSpawnDelaySeconds = 7f;

    public List<GameObject> carPrefabs;

    public Transform startPosition;
    public Transform endPosition;

    private float _time;


    private void Start()
    {
        startPosition.LookAt(endPosition);
    }


    private void Update()
    {
        if (_time <= 0)
        {
            Spawn();
            _time = Random.Range(minSpawnDelaySeconds, maxSpawnDelaySeconds);
        }

        _time -= Time.deltaTime;
    }


    private void Spawn()
    {
        int randIndex = Random.Range(0, carPrefabs.Count);

        GameObject carGO = Instantiate(carPrefabs[randIndex], startPosition.position, startPosition.rotation);

        Car car = carGO.GetComponent<Car>();

        if (car != null)
        {
            car.SetDestination(endPosition.position, speed);
        }
    }
}
