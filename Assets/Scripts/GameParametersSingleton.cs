using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Parameters Singleton", menuName = "ScriptableObjects/Game Parameters Singleton", order = 8)]
public class GameParametersSingleton : ScriptableObject
{
    public bool showTitle = true;
    public int goldAmount = 0;
    public int openedLevelsOnLocation1 = 1;
    public int openedLevelsOnLocation2 = 0;
    public int openedLevelsOnLocation3 = 0;
    public int openedLevelsOnLocation4 = 0;

    public void Save()
    {
        Debug.Log("Not implemented yet");
    }

    public void Load()
    {
        Debug.Log("Not implemented yet");        
    }
}
