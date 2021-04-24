using UnityEngine;

public class EquipButton : MonoBehaviour
{
    public Item item;

    public float scaleMultiplier = 1f;
    public float itemOffsetZ = -10f;
    public float itemYRotation = 100f;

    public void SpawnItem()
    {
        // create 3d item
        GameObject itemGO = Instantiate(item.prefab, transform);
        itemGO.transform.position += new Vector3(0, 0, itemOffsetZ);
        itemGO.transform.localScale *= scaleMultiplier;

        Rotator rotator = itemGO.AddComponent<Rotator>();
        rotator.rotation = new Vector3(0, itemYRotation, 0);
    }
}
