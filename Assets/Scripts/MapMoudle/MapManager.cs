using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MapManager :IMassageInterface
{
    private GameObject gameObject;
    /// <summary>
    /// int为层，list为层内所有的地图
    /// </summary>
    public List<MapBase> allMaps;
    public MapBase curMap;
    public MapDataList mapDataList;

    private Stack<Vector3> doorPos; //进入新房间后，记录之前的门的pos,用于出房间时setPos


    public MapManager(GameObject gameObject)
    {
        this.gameObject = gameObject;
        allMaps = new List<MapBase>();
        mapDataList = MapMassageFromJson.LoadMapDataJson("MapData");
        GameManager.instance.rootMassageNode.AttachEventListener(MassageList.EnterMap, this);
        GameManager.instance.rootMassageNode.AttachEventListener(MassageList.OutMap, this);
        doorPos = new Stack<Vector3>();
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
            if(curMap!= null) curMap.GetMapObject().SetActive(false);
            curMap = new MapBase(gameObject, mapdate);
            allMaps.Add(curMap);
        }
        else
        {
            curMap.GetMapObject().SetActive(false);
            curMap = map;
            curMap.GetMapObject().SetActive(true);
        }
    }


    public bool HandleMessage(int id, string mapName, GameObject param2)
    {
        if (mapName != null)
        {
            LoadMap(mapName);
        }
        if(id == MassageList.EnterMap)
        {
            Transform transform = RoleInterface.GetPlayer().transform;
            SetPos(transform,true);
            CameraInterface.SetCameraPos(transform.position);
        }else if(id == MassageList.OutMap)
        {
            Transform transform = RoleInterface.GetPlayer().transform;
            SetPos(transform,false);
            CameraInterface.SetCameraPos(transform.position);
        }
        return false;
    }

    private void SetPos(Transform target,bool enter)
    {
        if (enter)
        {
            doorPos.Push(target.position);
            target.position = curMap.GetMapObject().transform.Find("rootPos").position;
        }
        else
        {
            target.position = doorPos.Pop();
        }
    }

    public int MessagePriority()
    {
        return 1;
    }

    // Update is called once per frame
    public void Update()
    {
        
    }
}
