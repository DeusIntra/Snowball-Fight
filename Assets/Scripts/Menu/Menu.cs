using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public LevelDataHolder levelDataHolder;
    public GameParametersSingleton parameters;
    public Inventory inventory;

    public GameObject titlePanel;
    public List<AnimatedObject> menuButtons;

    public List<string> scenes;

    public List<LevelDataObject> location1;
    public List<LevelDataObject> location2;
    public List<LevelDataObject> location3;
    public List<LevelDataObject> location4;

    private int _savedLocationIndex = -1;
    private List<List<LevelDataObject>> locations;

    private void Awake()
    {
        locations = new List<List<LevelDataObject>>();
        locations.Add(location1);
        locations.Add(location2);
        locations.Add(location3);
        locations.Add(location4);

        parameters.Load();
    }

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        if (!parameters.showTitle)
        {
            Destroy(titlePanel);
            foreach (AnimatedObject button in menuButtons)
            {
                button.Animate();
            }
        }
    }

    public void ChooseLocationIndex(int locationIndex)
    {
        if (inventory.IsEmpty) LoadLevel(locationIndex);
        else
        {
            _savedLocationIndex = locationIndex;
        }
    }

    public void LoadLevel()
    {
        LoadLevel(_savedLocationIndex);
    }

    public void HideTitleAndShowButtons()
    {
        titlePanel.GetComponent<Button>().interactable = false;
        titlePanel.GetComponent<AnimatedObject>().Animate();

        foreach (AnimatedObject button in menuButtons)
        {
            button.Animate();
        }

        parameters.showTitle = false;
    }

    [ContextMenu("reset prefs")]
    public void ResetPrefs()
    {
        parameters.ResetProgress();
    }

    private void LoadLevel(int locationIndex)
    {
        Debug.Log("TODO: Async");

        var levelsOpened = parameters.finishedLevelsOnLocation;
        int levelIndex = levelsOpened[locationIndex];

        if (levelIndex == 10)
        {
            levelIndex = Random.Range(5, 10);
        }

        levelDataHolder.levelData = locations[locationIndex][levelIndex];
        parameters.currentLocationIndex = locationIndex;
        parameters.currentLevelIndex = levelIndex;
        SceneManager.LoadScene(scenes[locationIndex]);
    }
}
