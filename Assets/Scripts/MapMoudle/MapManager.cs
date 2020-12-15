using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MapManager 
{
    private GameObject gameObject;
    /// <summary>
    /// int为层，list为层内所有的地图
    /// </summary>
    public Dictionary<int,List<MapBase>> allMaps;
    public MapBase curMap;
    public MapDataList mapDataList;


    public MapManager(GameObject gameObject)
    {
        this.gameObject = gameObject;
        allMaps = new Dictionary<int, List<MapBase>>();
        mapDataList = MapMassageFromJson.LoadMapDataJson("MapData");

        LoadMap(0);
    }
    

    /// <summary>
    /// 加载某一层的地图
    /// </summary>
    public void LoadMap(int mapID)
    {
        int floor = mapID / 10000;
        int roomNum = mapID % 10000;
        MapData mapdate = mapDataList.MapDatas.Find(v => v.mapID == mapID);

        curMap = new MapBase(gameObject, mapdate);
    }


    /// <summary>
    /// 加载层内的子地图
    /// </summary>
    /// <param name="floor">层</param>
    public void LoadChildMap(GameObject floor,int mapID)
    {

    }



    // Update is called once per frame
    public void Update()
    {
        
    }
}
