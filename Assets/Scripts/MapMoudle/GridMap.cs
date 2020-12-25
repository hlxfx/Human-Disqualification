using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GridMap 
{
    public List<Grid> openList;
    public List<Grid> closeList;
    public List<Grid> grids = new List<Grid>();
    public float gridSize = 0.16f;
    private Object gridPrefab;

    public GridMap()
    {
        gridPrefab = Resources.Load("prefabs/grid");
    }

    public void CreatGrid(int mapW, int mapH ,Transform parent)
    {
        
        ClearGrids();
        int temp = 0;
        for (int x = 0; x < mapW; x++)
        {
            for (int y = 0; y < mapH; y++)
            {
                if(grids.Count < temp + 1)
                {
                    grids.Add(new Grid(GameObject.Instantiate(gridPrefab) as GameObject));
                }
                else
                {
                    grids[temp].gameObject.SetActive(true);
                }
                grids[temp].SetPos(new Vector3(gridSize * x, gridSize * y, gridSize * 0) + new Vector3(gridSize / 2, gridSize / 2, 0), parent);
                temp++;
            }
        }
    }

    public Vector3 CalculateRoad(Transform self,Transform target)
    {
        return Vector3.zero;
    }

    public void ClearGrids()
    {
        for(int i = 0; i < grids.Count; i++)
        {
            grids[i].gameObject.SetActive(false);
        }
    }
}

public enum E_NODE_TYPE
{
    //能走
    Walk,
    //不能走
    Stop
}

public class Grid
{
    //寻路消耗
    public float f;
    //离起点的距离
    public float g;
    //离终点的距离
    public float h;
    public Grid father;
    public GameObject gameObject;
    public E_NODE_TYPE type;

    public Grid(GameObject gameObject,Grid grid = null , E_NODE_TYPE e_NODE_TYPE = E_NODE_TYPE.Walk)
    {
        this.gameObject = gameObject;
        father = grid;
        type = e_NODE_TYPE;
    }

    public void SetPos(Vector3 pos,Transform parent)
    {
        gameObject.transform.position = pos;
        gameObject.transform.SetParent(parent);
    }
}
