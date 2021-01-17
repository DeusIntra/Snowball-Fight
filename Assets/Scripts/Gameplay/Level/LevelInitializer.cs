﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelInitializer : MonoBehaviour
{
    public LevelDataHolder levelDataHolder;
    public Transform enemyLines;
    public Inventory inventory;
    public ProgressBarTimed shotProgressBar;
    public TextMeshProUGUI timerText;

    private List<Transform> _spawnPoints;
    private int _bossSpawnPointIndex = -1;
    private int _enemyCount = 0;
    private LevelDataObject _levelData;

    private GameObject _player;
    private EnablerDisabler _enablerDisabler;
    private EnemyHolder _enemyHolder;

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
        _enemyHolder = GetComponent<EnemyHolder>();
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

            GameObject enemy = Instantiate(_levelData.bossPrefab, spawnPoint.position, Quaternion.identity);
            _enemyHolder.enemies.Add(enemy);

            _enemyCount++;
        }

        if (_levelData.minion1Count > 0)
            SpawnEnemies(_levelData.minion1Prefab, _levelData.minion1Count);
        if (_levelData.minion2Count > 0)
            SpawnEnemies(_levelData.minion2Prefab, _levelData.minion2Count);
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
                        SpeedMultiplier(effect.value);
                        break;
                    case "Energy Up":
                        EnergyUp(effect.value);
                        break;
                    case "Snowball Scale Up":
                        SnowballScaleUp(effect.value);
                        break;
                    case "Health Up":
                        HealthUp();
                        break;
                    case "Vampirism":
                        Vampirism();
                        break;
                    default:
                        Debug.LogError("Effect name " + effect.name + " is not used");
                        break;
                }
            }

        }

        //inventory.ClearPassiveItems();
        Debug.Log("TODO: uncomment me");
        #endregion

        StartCoroutine(CountdownCoroutine());
    }

    private void SpawnEnemies(GameObject prefab, int count)
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
            GameObject enemy = Instantiate(prefab, spawnPoint.position + offsetZ, Quaternion.identity);
            _enemyHolder.enemies.Add(enemy);

            _enemyCount++;
        }
    }

    #region Passive item effects
    private void SpeedMultiplier(float value)
    {
        PlayerMover playerMover = _player.GetComponent<PlayerMover>();
        playerMover.speedMultiplier = value;
    }

    private void EnergyUp(float value)
    {
        shotProgressBar.timeToFillSeconds /= value;
    }

    private void SnowballScaleUp(float value)
    {
        _player.GetComponent<PlayerShooter>().snowballScale = value;
    }

    private void HealthUp()
    {
        Health playerHealth = _player.GetComponent<Health>();
        playerHealth.max = 24;
        playerHealth.ResetCurrent();
        Player p = _player.GetComponent<Player>();
        p.healthBar.gameObject.SetActive(false);
        p.healthBar = p.healthBar6;
        p.healthBar.gameObject.SetActive(true);
    }

    private void Vampirism()
    {
        foreach (GameObject enemy in _enemyHolder.enemies)
        {
            enemy.GetComponent<HealthGiver>().isEnabled = true;
        }
    }
    #endregion

    private IEnumerator CountdownCoroutine(int seconds = 3)
    {
        Time.timeScale = 0;
        _enablerDisabler.DisableObjects();
        for (int i = seconds; i > 0; i--)
        {
            timerText.text = i.ToString();
            yield return new WaitForSecondsRealtime(1f);
        }

        timerText.text = "GO!";
        yield return new WaitForSecondsRealtime(1f);

        Time.timeScale = 1;
        _enablerDisabler.EnableObjects();
        timerText.gameObject.SetActive(false);
    }
}
