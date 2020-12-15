using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MapBase
{
    private GameObject gameObject;

    /// <summary>
    /// mapID / 10000 = 当前层数   每层预留1W个小房间
    /// </summary>
    public int mapID;
    public string mapName;

    public List<ItemData> items;
    public List<GameObject> itemObjects;

    public MapBase(GameObject gameObject, MapData mapData)
    {
        this.gameObject = gameObject;
        itemObjects = new List<GameObject>();
        mapID = mapData.mapID;
        mapName = mapData.mapName;
        items = mapData.itemList;

        GameObject map = Instantiate("prefabs/mapBase");
        map.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("pic/map/" + mapName);
        map.transform.SetParent(gameObject.transform);

        foreach (var item in items)
        {
            if (item.isShow)
            {
                GameObject temp = Instantiate("prefabs/items/" + item.itemName);
                temp.transform.SetParent(map.transform);
                itemObjects.Add(temp);
            }
        }
    }

    private GameObject Instantiate(string path)
    {
        GameObject temp = Resources.Load<GameObject>(path);
        temp = GameObject.Instantiate(temp);
        return temp;
    }

}
