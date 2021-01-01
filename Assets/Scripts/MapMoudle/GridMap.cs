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
    private Vector3 offset;

    public GridMap()
    {
        gridPrefab = Resources.Load("prefabs/grid");
    }

    public void CreatGrid(int mapW, int mapH ,Transform parent , BoxCollider2D[] obstacles = null)
    {
        offset = new Vector3(mapW / 2 * gridSize, mapH / 2 * gridSize, 0);
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
                grids[temp].SetPos(new Vector3(gridSize * x, gridSize * y, gridSize * 0) + new Vector3(gridSize / 2, gridSize / 2, 0) - offset, parent);
                if (CanWalk(obstacles, grids[temp].gameObject.transform.position))
                {
                    grids[temp].SetNodeType(E_NODE_TYPE.Walk);
                }
                else
                {
                    grids[temp].SetNodeType(E_NODE_TYPE.Stop);
                }
                temp++;
            }
        }
    }

    public bool CanWalk(BoxCollider2D[] obstacles, Vector3 selfPos)
    {
        for(int i = 0; i < obstacles.Length; i++)
        {
            Vector2 temp = obstacles[i].offset + new Vector2(obstacles[i].gameObject.transform.position.x, obstacles[i].gameObject.transform.position.y);
            if (selfPos.x> temp.x- obstacles[i].size.x / 2 && selfPos.x < temp.x + obstacles[i].size.x / 2)
            {
                if (selfPos.y > temp.y - obstacles[i].size.y / 2 && selfPos.y < temp.y + obstacles[i].size.y / 2)
                {
                    return false;
                }
            }
        }
        return true;
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

    public Grid(GameObject gameObject,Grid grid = null)
    {
        this.gameObject = gameObject;
        father = grid;
    }

    public void SetNodeType(E_NODE_TYPE e_NODE_TYPE)
    {
        type = e_NODE_TYPE;
        if (type == E_NODE_TYPE.Walk)
        {
            gameObject.layer = 9;       //walk层
        }
        else
        {
            gameObject.layer = 10;      //can not walk层
        }
    }

    /// <summary>
    /// 设置每个格子的位置,以及用来A*寻路的父节点
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="parent"></param>
    public void SetPos(Vector3 pos,Transform parent)
    {
        gameObject.transform.position = pos;
        gameObject.transform.SetParent(parent);
    }
}
