using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public List<GameObject> objectsToHideOnPause;
    public List<GameObject> objectsToRevealOnPause;


    public void Pause()
    {
        Time.timeScale = 0f;

        foreach (GameObject obj in objectsToHideOnPause)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in objectsToRevealOnPause)
        {
            obj.SetActive(true);
        }
    }


    public void Unpause()
    {
        Time.timeScale = 1f;

        foreach (GameObject obj in objectsToHideOnPause)
        {
            obj.SetActive(true);
        }
        foreach (GameObject obj in objectsToRevealOnPause)
        {
            obj.SetActive(false);
        }
    }
}
