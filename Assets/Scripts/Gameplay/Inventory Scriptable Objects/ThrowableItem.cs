using UnityEngine;

[CreateAssetMenu(fileName = "New Throwable Item", menuName = "ScriptableObjects/Throwable Item", order = 4)]
public class ThrowableItem : ActiveItem
{
    public int damage;
    public GameObject prefab;
}
