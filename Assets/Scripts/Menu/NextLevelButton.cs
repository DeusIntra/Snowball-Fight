using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelButton : MonoBehaviour
{
    public LevelDataHolder levelDataHolder;
    public GameParametersSingleton parameters;
    public int lastLevelIndex = 9;

    public List<string> scenes;

    public List<LevelDataObject> location1;
    public List<LevelDataObject> location2;
    public List<LevelDataObject> location3;
    public List<LevelDataObject> location4;

    private List<List<LevelDataObject>> _locations;

    private void Awake()
    {
        _locations = new List<List<LevelDataObject>>();
        _locations.Add(location1);
        _locations.Add(location2);
        _locations.Add(location3);
        _locations.Add(location4);

        if (parameters.currentLevelIndex == lastLevelIndex)
        {
            gameObject.SetActive(false);
        }
    }

    public void NextLevel()
    {
        int locationIndex = parameters.currentLocationIndex;
        int levelIndex = parameters.currentLevelIndex + 1;

        levelDataHolder.levelData = _locations[locationIndex][levelIndex];
        parameters.currentLocationIndex = locationIndex;
        parameters.currentLevelIndex = levelIndex;
        SceneManager.LoadScene(scenes[locationIndex]);
    }
}
