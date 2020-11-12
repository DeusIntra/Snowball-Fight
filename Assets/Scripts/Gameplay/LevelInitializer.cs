using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    public LevelDataObject levelData;
    public Transform enemyLines;

    private int _enemyCount = 0;

    private void Start()
    {
        SpawnEnemy(levelData.minion1Prefab, levelData.minion1Count);
        SpawnEnemy(levelData.minion2Prefab, levelData.minion2Count);
        if (levelData.hasBoss)
        {
            // TODO: spawn boss
        }
    }

    // REDO:
    private void SpawnEnemy(GameObject prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            int spawnLineIndex = Random.Range(0, enemyLines.childCount);
            Transform spawnLine = enemyLines.GetChild(spawnLineIndex);

            int spawnPointIndex = Random.Range(0, spawnLine.childCount);
            Transform spawnPoint = spawnLine.GetChild(spawnPointIndex);

            Vector3 offsetZ = new Vector3(0, 0, _enemyCount * 0.05f);
            Instantiate(prefab, spawnPoint.position + offsetZ, Quaternion.identity);

            _enemyCount++;
        }
    }

}
