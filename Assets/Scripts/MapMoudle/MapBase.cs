using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MapBase
{
    public GameObject map;

    public int mapID;
    public string mapName;

    public List<ItemData> items;
    public List<GameObject> itemObjects;

    public MapBase(GameObject gameObject, MapData mapData)
    {
        itemObjects = new List<GameObject>();
        mapID = mapData.mapID;
        mapName = mapData.mapName;
        items = mapData.itemList;

        map = GeneraMethod.LoadGameObject("prefabs/map/" + mapName);
        map.GetComponent<SpriteRenderer>().sprite = GeneraMethod.LoadSprite("pic/map/" + mapName);
        map.transform.SetParent(gameObject.transform);

        foreach (var item in items)
        {
            if (item.isShow)
            {
                GameObject temp = GeneraMethod.LoadGameObject("prefabs/items/" + item.itemName);
                temp.transform.SetParent(map.transform);
                itemObjects.Add(temp);
            }
        }
    }

    public GameObject GetMapObject()
    {
        return map;
    }
}
