using UnityEngine;

[CreateAssetMenu(fileName = "New Level Data", menuName = "ScriptableObjects/Level Data", order = 1)]
public class LevelDataObject : ScriptableObject
{
    [Header("Enemies Count")]
    public int girl;
    public int boy;
    public int penguin;
    public int polarBear;
}
