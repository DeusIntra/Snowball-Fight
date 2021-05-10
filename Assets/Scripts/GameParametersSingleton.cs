using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[CreateAssetMenu(fileName = "Game Parameters Singleton", menuName = "ScriptableObjects/Game Parameters Singleton", order = 8)]
public class GameParametersSingleton : ScriptableObject
{
    public bool showTitle = true;
    public bool music;
    public bool sounds;

    public int goldAmount = 0;
    public List<int> finishedLevelsOnLocation;

    public int currentLocationIndex = -1;
    public int currentLevelIndex = -1;

    public Inventory inventory;

    private void OnEnable()
    {
        showTitle = true;
    }

    public void Save()
    {
        for (int i = 0; i < finishedLevelsOnLocation.Count; i++)
        {
            PlayerPrefs.SetInt($"location {i}", finishedLevelsOnLocation[i]);
        }

        PlayerPrefs.SetInt("gold amount", goldAmount);

        List<ItemData> itemData = inventory.GetItemData();

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ItemData>));

        using (StringWriter stringWriter = new StringWriter())
        {
            xmlSerializer.Serialize(stringWriter, itemData);
            PlayerPrefs.SetString("item data", stringWriter.ToString());
        }

        PlayerPrefs.SetInt("music", music ? 1 : 0);
        PlayerPrefs.SetInt("sounds", sounds ? 1 : 0);

        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (finishedLevelsOnLocation == null) 
            finishedLevelsOnLocation = new List<int>();
        
        int i = 0;
        while (PlayerPrefs.HasKey($"location {i}"))
        {
            int count = PlayerPrefs.GetInt($"location {i}");
            if (finishedLevelsOnLocation.Count < i + 1)
            {
                finishedLevelsOnLocation.Add(count);
            }
            else
            {
                finishedLevelsOnLocation[i] = count;
            }
            i++;
        }

        if (finishedLevelsOnLocation.Count == 0)
            finishedLevelsOnLocation.Add(0);

        if (PlayerPrefs.HasKey("game loaded before"))
            goldAmount = PlayerPrefs.GetInt("gold amount");
        else
        {
            PlayerPrefs.SetInt("game loaded before", 0);
            goldAmount = 50;
            PlayerPrefs.SetInt("gold amount", goldAmount);
        }

        if (PlayerPrefs.HasKey("item data"))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ItemData>));

            string xml = PlayerPrefs.GetString("item data");
            List<ItemData> itemData;

            using (StringReader stringReader = new StringReader(xml))
            {
                itemData = (List<ItemData>)xmlSerializer.Deserialize(stringReader);
            }

            inventory.SetStashedItems(itemData);
        }

        if (PlayerPrefs.HasKey("music") == false)
        {
            PlayerPrefs.SetInt("music", 1);
        }
        music = PlayerPrefs.GetInt("music") == 1;

        if (PlayerPrefs.HasKey("sounds") == false)
        {
            PlayerPrefs.SetInt("sounds", 1);
        }
        sounds = PlayerPrefs.GetInt("sounds") == 1;
    }

    public void ResetProgress()
    {
        finishedLevelsOnLocation = new List<int>();
        finishedLevelsOnLocation.Add(0);
        PlayerPrefs.DeleteAll();
    }

}
