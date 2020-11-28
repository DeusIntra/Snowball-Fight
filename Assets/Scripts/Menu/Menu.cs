using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public LevelDataHolder levelDataHolder;

    public GameObject LocationButtons;
    public GameObject LevelButtons;

    public List<string> scenes;

    public List<LevelDataObject> location1;
    public List<LevelDataObject> location2;
    public List<LevelDataObject> location3;
    public List<LevelDataObject> location4;

    private int locationIndex = -1;
    private List<List<LevelDataObject>> locations;

    private void Awake()
    {
        locations = new List<List<LevelDataObject>>();
        locations.Add(location1);
        locations.Add(location2);
        locations.Add(location3);
        locations.Add(location4);
    }

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    public void ChooseLocation(int index)
    {
        locationIndex = index;
        LocationButtons.SetActive(false);
        LevelButtons.SetActive(true);
    }

    public void LoadLevel(int levelIndex)
    {
        levelDataHolder.levelData = locations[locationIndex - 1][levelIndex - 1];
        SceneManager.LoadScene(scenes[locationIndex - 1]);
    }
}
