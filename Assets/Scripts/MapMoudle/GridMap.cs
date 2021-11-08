using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum NodeType
{
    walk = 9,
    unWalk = 10
}

public class GridMap 
{
    public List<Grid> openList = new List<Grid>();
    public List<Grid> closeList = new List<Grid>();
    public List<Grid> grids = new List<Grid>();
    public float gridSize = 0.16f;
    private GameObject gridPrefab;
    private Vector3 offset;
    private int curMapH;
    private int curMapW;

    public GridMap()
    {
        gridPrefab = GeneraMethod.LoadGameObject("prefabs/grid");
    }

    public void CreatGrid(int mapW, int mapH ,Transform parent , BoxCollider2D[] obstacles = null)
    {
        curMapH = mapH;
        curMapW = mapW;
        offset = new Vector3((float)mapW / 2 * gridSize, (float)mapH / 2 * gridSize, 0);
        ClearGrids();
        int temp = 0;
        //x,y便是格子在格子地图中的x，y坐标
        for (int x = 0; x < mapW; x++)
        {
            for (int y = 0; y < mapH; y++)
            {
                if(grids.Count < temp + 1)
                {
                    grids.Add(new Grid(GameObject.Instantiate(gridPrefab), temp));
                }
                else
                {
                    grids[temp].gameObject.SetActive(true);
                }
                grids[temp].SetPos(new Vector3(gridSize * x, gridSize * y, gridSize * 0) + new Vector3(gridSize / 2, gridSize / 2, 0) - offset, parent);
                if (CanWalk(obstacles, grids[temp].gameObject.transform.position))
                {
                    grids[temp].SetNodeType(NodeType.walk);
                }
                else
                {
                    grids[temp].SetNodeType(NodeType.unWalk);
                }
                grids[temp].gameObject.name = (x * mapH + y).ToString();
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

    public List<Vector3> CalculateRoad(Vector3 start, Vector3 end)
    {
        start = start + offset;
        end = end + offset;
        //将目标点映射到格子地图的中的x，y坐标，再转换到格子列表中  grids[int num]
        int targetNum = Mathf.FloorToInt(Mathf.Abs(end.x / gridSize)) * curMapH + Mathf.CeilToInt(Mathf.Abs(end.y / gridSize));
        targetNum -= 1; //因为算出来的是在list中的位置 要-1转为下标
        //是否在地图内
        if (grids[targetNum].gameObject == null || !grids[targetNum].gameObject.activeSelf)
        {
            Debug.Log("目标点不在地图内! num" + targetNum);
            return null;
        }

        //是不是阻挡点
        if(grids[targetNum].gameObject.layer == (int)NodeType.unWalk)
        {
            Debug.Log("目标点不可抵达! num = " + targetNum);
            return null;
        }
        Debug.Log("目标点! num" + targetNum);

        int startX = Mathf.FloorToInt(Mathf.Abs(start.x / gridSize));
        int startY = Mathf.CeilToInt(Mathf.Abs(start.y / gridSize));
        //清空上一次寻路的信息
        closeList.Clear();
        openList.Clear();
        int startNum = startX * curMapH + startY;
        //将起始点放入close列表
        grids[startNum].father = null;
        grids[startNum].f = 0;
        grids[startNum].g = 0;
        grids[startNum].h = 0;
        closeList.Add(grids[startNum]);

        while (true)
        {
            //寻找周围的点
            //左上    startNum - curMapH + 1
            FindNearNode2OpenList(startNum - curMapH + 1, gridSize * 1.4f, grids[startNum], grids[targetNum]);
            //上      startNum + 1
            FindNearNode2OpenList(startNum + 1, gridSize, grids[startNum], grids[targetNum]);
            //右上    startNum + curMapH + 1
            FindNearNode2OpenList(startNum + curMapH + 1, gridSize * 1.4f, grids[startNum], grids[targetNum]);
            //左      startNum - curMapH
            FindNearNode2OpenList(startNum - curMapH, gridSize, grids[startNum], grids[targetNum]);
            //右      startNum + curMapH
            FindNearNode2OpenList(startNum + curMapH, gridSize, grids[startNum], grids[targetNum]);
            //左下    startNum - curMapH - 1
            FindNearNode2OpenList(startNum - curMapH - 1, gridSize * 1.4f, grids[startNum], grids[targetNum]);
            //下      startNum - 1
            FindNearNode2OpenList(startNum - 1, gridSize, grids[startNum], grids[targetNum]);
            //右下    startNum + curMapH - 1
            FindNearNode2OpenList(startNum + curMapH - 1, gridSize * 1.4f, grids[startNum], grids[targetNum]);

            if(openList.Count == 0)
            {
                Debug.Log("死路");
                return null;
            }
            //将f值最小的格子加入关闭列表
            openList.Sort(SortOpenList);
            closeList.Add(openList[0]);
            startNum = openList[0].index;
            openList.RemoveAt(0);

            if (startNum == targetNum)
            {
                List<Vector3> path = new List<Vector3>();
                Grid target = grids[targetNum];
                path.Add(target.gameObject.transform.position);
                while(target.father != null)
                {
                    target = target.father;
                    path.Add(target.gameObject.transform.position);
                }
                //翻转列表获得路径
                path.Reverse();
                return path;
            }
        }
    }

    private int SortOpenList(Grid a, Grid b) {
        if (a.f > b.f) 
            return 1; 
        else if (a.f == b.f) 
            return 1;
        else
            return - 1; 
    }

    private void FindNearNode2OpenList(int num, float g, Grid father, Grid endNode)
    {
        if (num < 0 || grids[num].gameObject == null || !grids[num].gameObject.activeSelf)
        {
            //Debug.Log(num+ "点不在地图内");
            return;
        }

        //是不是阻挡点
        if (grids[num].gameObject.layer == (int)NodeType.unWalk)
        {
            //Debug.Log(num + "点是阻挡点!");
            return;
        }

        //目标点是否在开启或关闭列表中
        if (closeList.Contains(grids[num]) || openList.Contains(grids[num]))
        {
            return;
        }

        //计算f值
        //f = g + h
        grids[num].father = father;
        grids[num].g = g + father.g;
        grids[num].h = Mathf.Abs(endNode.gameObject.transform.position.x - grids[num].gameObject.transform.position.x)
            + Mathf.Abs(endNode.gameObject.transform.position.y - grids[num].gameObject.transform.position.y);
        grids[num].f = grids[num].g + grids[num].h;
        openList.Add(grids[num]);
        return;
    }

    public void ClearGrids()
    {
        for(int i = 0; i < grids.Count; i++)
        {
            grids[i].gameObject.SetActive(false);
        }
    }
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
    public NodeType type;
    //格子在格子地图列表中的索引，可以用于设置为下一起点
    public int index; 

    public Grid(GameObject gameObject, int index)
    {
        this.gameObject = gameObject;
        this.index = index;
    }

    public void SetNodeType(NodeType e_NODE_TYPE)
    {
        type = e_NODE_TYPE;
        if (type == NodeType.walk)
        {
            gameObject.layer = (int)NodeType.walk;       //walk层
        }
        else
        {
            gameObject.layer = (int)NodeType.unWalk;       //can not walk层
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
