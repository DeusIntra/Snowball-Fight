using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public bool occupied = false;
    public Item item;

    public float scaleMultiplier = 1f;
    public float itemOffsetZ = -10f;
    public float itemYRotation = 100f;

    private ItemSlotGroup _parent;

    private void Start()
    {
        _parent = transform.parent.GetComponent<ItemSlotGroup>();
    }

    public void Equip(Item item)
    {
        if (occupied) return;
        occupied = true;

        this.item = item;

        // spawn item
        GameObject itemGO = Instantiate(item.prefab, transform);
        itemGO.transform.position += new Vector3(0, 0, itemOffsetZ);
        itemGO.transform.localScale *= scaleMultiplier;

        GameObjectUtil.IterateChildren(gameObject, SetUILayer, true);

        Rotator rotator = itemGO.AddComponent<Rotator>();
        rotator.rotation = new Vector3(0, itemYRotation, 0);
    }

    public void Unequip()
    {
        if (occupied == false) return;
        occupied = false;

        _parent.Unequip(item);

        // despawn item
        Destroy(transform.GetChild(0).gameObject);
    }

    private void SetUILayer(GameObject obj)
    {
        obj.layer = 5;
    }
}
