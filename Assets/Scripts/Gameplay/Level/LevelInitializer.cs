using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    public LevelDataHolder levelDataHolder;
    public Transform enemyLines;
    public Inventory inventory;
    public ProgressBarTimed shotProgressBar;

    private List<Transform> _spawnPoints;
    private int _bossSpawnPointIndex = -1;
    private int _enemyCount = 0;
    private LevelDataObject _levelData;

    private GameObject _player;
    private EnablerDisabler _enablerDisabler;

    private void Awake()
    {
        _levelData = levelDataHolder.levelData;

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

        _player = FindObjectOfType<Player>().gameObject;

        _enablerDisabler = GetComponent<EnablerDisabler>();
    }


    private void Start()
    {
        #region Enemy spawning
        if (_levelData.hasBoss)
        {
            if (_bossSpawnPointIndex == -1)
            {
                Debug.LogError("Boss spawn point not found");
                return;
            }

            Transform spawnPoint = _spawnPoints[_bossSpawnPointIndex];
            _spawnPoints.RemoveAt(_bossSpawnPointIndex);

            Instantiate(_levelData.bossPrefab, spawnPoint.position, Quaternion.identity);

            _enemyCount++;
        }

        if (_levelData.minion1Count > 0)
            SpawnEnemy(_levelData.minion1Prefab, _levelData.minion1Count);
        if (_levelData.minion2Count > 0)
            SpawnEnemy(_levelData.minion2Prefab, _levelData.minion2Count);
        #endregion

        #region Passive items buffs
        foreach (PassiveItem passiveItem in inventory.passiveItems)
        {
            if (passiveItem.effects.Count == 0)
            {
                Debug.LogError("Item " + passiveItem.name + " has 0 effects");
                continue;
            }

            foreach (ItemEffect effect in passiveItem.effects)
            {
                switch (effect.name)
                {
                    case "Speed Multiplier":
                        MultiplyPlayerSpeed(effect.value);
                        break;
                    case "Energy Up":
                        shotProgressBar.timeToFillSeconds /= effect.value;
                        break;
                    default:
                        Debug.LogError("Effect name " + effect.name + " is not used");
                        break;
                }
            }

        }

        // inventory.ClearPassiveItems();
        #endregion

        StartCoroutine(CountdownCoroutine());
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

    #region Passive item effects
    private void MultiplyPlayerSpeed(float value)
    {
        PlayerMover playerMover = _player.GetComponent<PlayerMover>();
        playerMover.speedMultiplier = value;
    }
    #endregion

    private IEnumerator CountdownCoroutine(int seconds = 3)
    {
        Time.timeScale = 0;
        _enablerDisabler.DisableObjects();
        for (int i = seconds; i > 0; i--)
        {
            Debug.Log(i);
            yield return new WaitForSecondsRealtime(1f);
        }
        Time.timeScale = 1;
        _enablerDisabler.EnableObjects();
    }
}
