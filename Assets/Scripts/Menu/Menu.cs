using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public LevelDataHolder levelDataHolder;
    public GameParametersSingleton parameters;
    public Inventory inventory; // used in BuyItemButton.cs

    public GameObject titlePanel;
    public List<AnimatedObject> menuButtons;

    public List<string> scenes;

    public List<LevelDataObject> location1;
    public List<LevelDataObject> location2;
    public List<LevelDataObject> location3;
    public List<LevelDataObject> location4;

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

    public void LoadLevel(int locationIndex)
    {
        Debug.Log("TODO: Async");
        var levelsOpened = parameters.openedLevelsOnLocation;
        int levelIndex;
        if (levelsOpened.Count != 0)
            levelIndex = levelsOpened[locationIndex - 1];
        else
            levelIndex = 1;
        levelDataHolder.levelData = locations[locationIndex - 1][levelIndex - 1];
        parameters.currentLocationIndex = locationIndex;
        parameters.currentLevelIndex = levelIndex;
        SceneManager.LoadScene(scenes[locationIndex - 1]);
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
}
