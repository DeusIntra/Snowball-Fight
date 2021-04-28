using System.Collections.Generic;
using UnityEngine;

public class EnablerDisabler : MonoBehaviour
{
    public List<GameObject> gameObjects;

    public void EnableObjects()
    {
        foreach (GameObject obj in gameObjects)
        {
            obj.SetActive(true);
        }
    }

    public void DisableObjects()
    {
        foreach (GameObject obj in gameObjects)
        {
            obj.SetActive(false);
        }
    }
}
