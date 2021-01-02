using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MapManager :IMassageInterface
{
    private GameObject gameObject;
    public MapBase curMap;
    public MapDataList mapDataList;
    public GridMap gridMap;

    public List<MapBase> allMaps;
    private GameObject gridParent;

    public MapManager(GameObject gameObject)
    {
        this.gameObject = gameObject;
        allMaps = new List<MapBase>();
        gridMap = new GridMap();
        mapDataList = MapMassageFromJson.LoadMapDataJson("MapData");
        GameManager.instance.rootMassageNode.AttachEventListener(MassageList.loadMap, this);
        gridParent = GameObject.Find("Grid");
    }
    
    public void LoadMap(string mapName)
    {
        MapBase map = allMaps.Find(v => v.mapName == mapName);
        if (map == null)
        {

            MapData mapdate = mapDataList.MapDatas.Find(v => v.mapName == mapName);
            if (mapdate == null)
            {
                Debug.LogError("该地图信息不存在");
                return;
            }

            if (curMap != null && curMap.GetMapObject()!= null) 
            {
                curMap.GetMapObject().SetActive(false);
            }
            curMap = new MapBase(gameObject, mapdate);
            allMaps.Add(curMap);
        }
        else
        {
            curMap.GetMapObject().SetActive(false);
            curMap = map;
            curMap.GetMapObject().SetActive(true);
        }
        //curMap.GetMapObject().transform.position = curMap.GetMapObject().transform.position;


        //**************生成格子地图,用于寻路
        float temp = curMap.GetMapObject().GetComponent<SpriteRenderer>().sprite.rect.width / 16;
        float temp2 = curMap.GetMapObject().GetComponent<SpriteRenderer>().sprite.rect.height / 16;
        //获取障碍物/不可走区域
        BoxCollider2D[] obstacle = curMap.GetMapObject().GetComponentsInChildren<BoxCollider2D>();
        gridMap.CreatGrid((int)temp, (int)temp2, gridParent.transform, obstacle);
        //**************
    }

    public bool HandleMessage(int id, string mapName, GameObject param2)
    {
        if (mapName != null)
        {
            LoadMap(mapName);
        }
        if (id == MassageList.loadMap)
        {
            Transform transform = RoleInterface.GetPlayer().transform;
            SetPos(transform, param2);
            CameraInterface.SetCameraPos(transform.position);
        }
        return false;
    }

    public int MessagePriority()
    {
        return 1;
    }

    private void SetPos(Transform target, GameObject door)
    {
        if (door != null)
        {
            target.position = door.transform.Find("desPos").position;
        }
        else
        {
            target.position = curMap.GetMapObject().transform.Find("rootPos").position;
        }
    }

    public List<Vector3> FindPath(Vector3 start, Vector3 end)
    {
        return gridMap.CalculateRoad(start, end);
    }

    // Update is called once per frame
    public void Update()
    {
        
    }
}
