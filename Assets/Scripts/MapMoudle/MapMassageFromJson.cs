using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class MapMassageFromJson 
{
    public static string m_JsonPath = "Datas/Map/";

    public static MapDataList LoadMapDataJson(string name)
    {
        TextAsset jsonText = Resources.Load<TextAsset>(m_JsonPath + name);
        MapDataList jsonTemp = new MapDataList();
        jsonTemp = JsonUtility.FromJson<MapDataList>(jsonText.text);
        return jsonTemp;
    }
}

[Serializable]
public class MapDataList
{
    public List<MapData> MapDatas;
}

[Serializable]
public class MapData
{
    public string mapName;
    public int mapID;
    public List<ItemData> itemList;
}

[Serializable]
public class ItemData
{
    public bool isShow;
    public string itemName;
    public bool isTrigger;
}

