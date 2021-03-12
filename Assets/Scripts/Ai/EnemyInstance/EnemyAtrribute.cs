using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using LitJson;
using UnityEngine;

[Serializable]
public class EnemyDataList
{
    public List<EnemyData> monster;
}

[Serializable]
public class EnemyData
{
    public int id;
    public string name;
    public float hp;
    public float mp;
    public RootNodeData behaviours;
}

[Serializable]
public class RootNodeData
{
    public string rootNodeName;
    public int rootNodeType;
    public List<ControlNodeData> nodes;
}

[Serializable]
public class ControlNodeData
{
    public string parentNodeName;
    public string controlNodeName;
    public int controlNodeType;
    public int priority;
    public List<string> nodes;      
}

public static class EnemyDataFromJson
{
    public static string m_JsonPath = "Assets/Resources/Datas/test/enemyTest.json";

    public static EnemyDataList LoadEnemyDataJson()
    {

        StreamReader json = File.OpenText(m_JsonPath);
        string input = json.ReadToEnd();
        EnemyDataList jsonTemp = new EnemyDataList();
        jsonTemp = JsonMapper.ToObject<EnemyDataList>(input);
        return jsonTemp;
    }
}


public class EnemyAtrribute 
{
    private EnemyData data;
    private BaseNode rootNode;
    private BaseNode temp;

    public EnemyAtrribute(/*怪物id*/)
    {
        /*
         根据怪物id从json或者其他配置中读取出怪物的各种属性，以及行为
         
         例如：
        "monster": [
        {
            "id": 1,
            "name" : "test1",
            "hp": 100,
            "mp": 100,
            "behaviours": {
                "rootNodeType" : 1  //1为选择控制节点  2为顺序控制节点  3为并行节点
                "nodes" : [
                    {
                        "controlNodeName" : "寻找敌人",
                        "controlNodeType" : 2  
                        "nodes" :[
                            "..." //    基础行为1 如寻找敌人
                            "..." //    基础行为2 如突进到目标位置并攻击
                        ]
                    },

                    {
                        "controlNodeName": "被攻击状态",
                        "controlNodeType" : 1
                        "nodes" :[
                            {
                                "controlNodeName" : "防御",
                                "controlNodeType" : 2    //顺序结构，要是防御成功，则可以触发防御反击
                                "nodes" :[
                                    {
                                        "controlNodeName" : "防御手段",
                                        "controlNodeType" : 1
                                        "nodes" :[
                                            "格挡" //    基础行为1 
                                            "闪避" //    基础行为2 
                                         ]
                                    },
                                    "防御反击手段"  //可以为选择控制节点，多种反击手段
                                ]
                            },
                            "受伤类名"
                        ]
                    },
                    .......
                    状态行为可配置
                ]
            },
        },                                                   
         */

        //---------------------------测试用代码
        data = EnemyDataFromJson.LoadEnemyDataJson().monster.Find(v => v.id == 1 /* 怪物id*/);
        InitRootNode();
    }


    /// <summary>
    /// 当前怪物处于哪种行为
    /// </summary>
    /// <returns></returns>
    public BaseNode GetCurBehaviour()
    {
        
        return null;
    }

    /// <summary>
    /// 将行为名(nodes)转换为类反射出来
    /// </summary>
    private void InitRootNode()
    {
        SwithControlNode(data.behaviours.rootNodeType,ref rootNode);
        AnalysisAction(data.behaviours.nodes);
    }

    /// <summary>
    /// 根据json解析怪物行为，并生成行为树
    /// </summary>
    /// <param name="behaviours"></param>
    /// <returns></returns>
    public bool AnalysisAction(List<ControlNodeData> behaviours)
    {

        foreach (var item in behaviours)
        {
            SwithControlNode(item.controlNodeType, ref temp);
            temp.nodeName = item.controlNodeName;
            temp.priority = item.priority;
            if (item.parentNodeName == "root")
            {
                rootNode.AddNode(temp);
            }
            else
            {
                rootNode.AddNode2Contol(item.parentNodeName, temp);
            }
            for (int i = 0; i < item.nodes.Count; i++)
            {
                //根据行为类名 string  反射出对应行为类，比将其作为行为节点 加载到对应的控制节点下
                Type type = Type.GetType(rootNode.GetType().Namespace + "." + item.nodes[i].ToString(), true, true);
                var action = Activator.CreateInstance(type) as BaseNode;
                temp.AddNode(action);
            }
        }


        //--------------------
        //测试用
        rootNode.excute();
        //--------------------
        return true;
    }

    public void SwithControlNode(int type,ref BaseNode node)
    {
        switch (type)
        {
            case 1:
                node = new SelectNode();
                break;
            case 2:
                node = new SequenceNode();
                break;
            case 3:
                node = new ParallelNode();
                break;
        }
    }

    //....  后续还有对其他属性的增减 以及获取的方法
}
