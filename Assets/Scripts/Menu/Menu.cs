using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject LocationButtons;
    public GameObject LevelButtons;

    public List<Scene> scenes;

    public List<LevelDataObject> location1;
    public List<LevelDataObject> location2;
    public List<LevelDataObject> location3;
    public List<LevelDataObject> location4;

    private int locationIndex = -1;
    //private int levelIndex = -1;
    private List<List<LevelDataObject>> locations;

    private void Awake()
    {
        locations = new List<List<LevelDataObject>>();
        locations.Add(location1);
        locations.Add(location2);
        locations.Add(location3);
        locations.Add(location4);
    }

    public void ChooseLocation(int index)
    {
        locationIndex = index;
        LocationButtons.SetActive(false);
        LevelButtons.SetActive(true);
    }

    public void LoadLevel(int levelIndex)
    {

    }


}
