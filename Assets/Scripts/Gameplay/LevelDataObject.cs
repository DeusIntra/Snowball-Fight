using UnityEngine;

[CreateAssetMenu(fileName = "New Level Data", menuName = "ScriptableObjects/Level Data", order = 1)]
public class LevelDataObject : ScriptableObject
{
    [Header("Enemies Count")]
    public int girlCount;
    public int boyCount;
    public int penguinCount;
    public int polarBearCount;

    [Header("Enemies Prefabs")]
    public GameObject girlPrefab;
    public GameObject boyPrefab;
    public GameObject penguinPrefab;
    public GameObject polarBearPrefab;
}
