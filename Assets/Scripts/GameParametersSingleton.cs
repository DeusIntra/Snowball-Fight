using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Parameters Singleton", menuName = "ScriptableObjects/Game Parameters Singleton", order = 8)]
public class GameParametersSingleton : ScriptableObject
{
    public bool showTitle = true;
    public int goldAmount = 0;
    public List<int> openedLevelsOnLocation;

    public int currentLocationIndex = -1;
    public int currentLevelIndex = -1;

    private void OnEnable()
    {
        showTitle = true;
    }

    public void Save()
    {
        for (int i = 0; i < openedLevelsOnLocation.Count; i++)
        {
            PlayerPrefs.SetInt($"location {i + 1}", openedLevelsOnLocation[i]);
        }

        PlayerPrefs.SetInt("gold amount", goldAmount);
        PlayerPrefs.Save();

        Debug.Log("Player prefs: " + PlayerPrefs.GetInt("location 1"));
    }

    public void Load()
    {
        if (openedLevelsOnLocation == null) openedLevelsOnLocation = new List<int>();
        for (int i = 0; i < openedLevelsOnLocation.Count; i++)
        {
            if (PlayerPrefs.HasKey($"location {i + 1}"))
            {
                openedLevelsOnLocation.Add(PlayerPrefs.GetInt($"location {i}"));
                Debug.Log("Prefs Load: " + PlayerPrefs.GetInt($"location {i}"));
            }
        }
        goldAmount = PlayerPrefs.GetInt("gold amount");

    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
    }
}
