using System.Collections.Generic;
using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    public LevelDataObject levelData;
    public Transform enemyLines;

    private List<Transform> _spawnPoints;
    private int _bossSpawnPointIndex = -1;
    private int _enemyCount = 0;


    private void Awake()
    {
        _spawnPoints = new List<Transform>();

        foreach (Transform enemyLine in enemyLines)
        {
            foreach (Transform spawnPoint in enemyLine)
            {
                _spawnPoints.Add(spawnPoint);
                if (spawnPoint.CompareTag("Boss Spawn Point"))
                {
                    _bossSpawnPointIndex = _spawnPoints.Count - 1;
                }
            }
        }
    }


    private void Start()
    {
        if (levelData.hasBoss)
        {
            if (_bossSpawnPointIndex == -1)
            {
                Debug.LogError("Boss spawn point not found");
                return;
            }

            Transform spawnPoint = _spawnPoints[_bossSpawnPointIndex];
            _spawnPoints.RemoveAt(_bossSpawnPointIndex);

            Instantiate(levelData.bossPrefab, spawnPoint.position, Quaternion.identity);

            _enemyCount++;
        }

        if (levelData.minion1Count > 0)
            SpawnEnemy(levelData.minion1Prefab, levelData.minion1Count);
        if (levelData.minion2Count > 0)
            SpawnEnemy(levelData.minion2Prefab, levelData.minion2Count);
    }


    private void SpawnEnemy(GameObject prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (_spawnPoints.Count == 0)
            {
                Debug.LogError("No spawn points");
                return;
            }

            int spawnPointIndex = Random.Range(0, _spawnPoints.Count);
            Transform spawnPoint = _spawnPoints[spawnPointIndex];
            _spawnPoints.RemoveAt(spawnPointIndex);

            Vector3 offsetZ = new Vector3(0, 0, _enemyCount * 0.05f);
            Instantiate(prefab, spawnPoint.position + offsetZ, Quaternion.identity);

            _enemyCount++;
        }
    }
}
