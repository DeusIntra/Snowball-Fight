using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Parameters Singleton", menuName = "ScriptableObjects/Game Parameters Singleton", order = 8)]
public class GameParametersSingleton : ScriptableObject
{
    public bool showTitle = true;
    public int goldAmount = 0;
    public List<int> finishedLevelsOnLocation;

    public int currentLocationIndex = -1;
    public int currentLevelIndex = -1;

    private void OnEnable()
    {
        showTitle = true;
    }

    public void Save()
    {
        for (int i = 0; i < finishedLevelsOnLocation.Count; i++)
        {
            PlayerPrefs.SetInt($"location {i}", finishedLevelsOnLocation[i]);
        }

        PlayerPrefs.SetInt("gold amount", goldAmount);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (finishedLevelsOnLocation == null) 
            finishedLevelsOnLocation = new List<int>();
        
        int i = 0;
        while (PlayerPrefs.HasKey($"location {i}"))
        {
            int count = PlayerPrefs.GetInt($"location {i}");
            if (finishedLevelsOnLocation.Count < i + 1)
            {
                finishedLevelsOnLocation.Add(count);
            }
            else
            {
                finishedLevelsOnLocation[i] = count;
            }
            i++;
        }

        // TODO: REMOVE
        Debug.Log("REMOVE THIS CODE");

        for (int j = 0; j < 5 - finishedLevelsOnLocation.Count; j++)
        {
            finishedLevelsOnLocation.Add(0);
        }

        //////////////////////////////


        if (finishedLevelsOnLocation.Count == 0)
            finishedLevelsOnLocation.Add(0);

        goldAmount = PlayerPrefs.GetInt("gold amount");
    }

    public void ResetProgress()
    {
        finishedLevelsOnLocation = new List<int>();
        finishedLevelsOnLocation.Add(0);
        PlayerPrefs.DeleteAll();
    }
}
