using UnityEngine;

[CreateAssetMenu(fileName = "New Level Data", menuName = "ScriptableObjects/Level Data", order = 1)]
public class LevelDataObject : ScriptableObject
{
    [Header("Minions Count")]
    public int minion1Count;
    public int minion2Count;

    public bool hasBoss;

    [Header("Enemies Prefabs")]
    public GameObject minion1Prefab;
    public GameObject minion2Prefab;
    public GameObject bossPrefab;
}
