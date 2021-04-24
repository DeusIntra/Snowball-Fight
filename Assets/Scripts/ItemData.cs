[System.Serializable]
public class ItemData
{
    public enum Type {Active, Passive};

    public string name;
    public Type type;
    public ItemData()
    {

    }

    public ItemData(string name, Type type)
    {
        this.name = name;
        this.type = type;
    }
}
