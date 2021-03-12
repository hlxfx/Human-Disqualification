using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNode
{
    protected List<BaseNode> childNode;
    protected BaseNode parentNode;
    public string nodeName;
    public int priority;

    public BaseNode()
    {
        childNode = new List<BaseNode>();
    }


    /// <summary>
    /// 根节点添加控制节点
    /// 控制节点添加行为节点
    /// </summary>
    /// <param name="node"></param>
    public void AddNode(BaseNode node)
    {
        node.parentNode = this;
        childNode.Add(node);
        childNode.Sort((a, b) => {
            int o = a.priority - b.priority;
            return -o;
        });
    }

    /// <summary>
    /// 控制节点添加子控制节点
    /// </summary>
    /// <param name="nodeName"></param>
    /// <param name="node"></param>
    public void AddNode2Contol(string nodeName , BaseNode node)
    {
        BaseNode temp = FindControlNode(childNode, nodeName);
        temp.AddNode(node);
        node.parentNode = temp;
    }

    private BaseNode FindControlNode(List<BaseNode> nodes , string nodeName)
    {
        BaseNode temp = nodes.Find(v => v.nodeName == nodeName);
        if(temp == null && nodes.Count > 0)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                temp = FindControlNode(nodes[i].childNode, nodeName);
                if (temp != null)
                {
                    return temp;
                }
            }
        }
        return temp;
    }


    /// <summary>
    /// 每个行为节点的具体行为
    /// 执行成功返回true  失败返回false 可根据执行情况是否需要继续执行后续行为
    /// </summary>
    /// <returns></returns>
    public virtual bool excute()
    {
        return true;
    }
}
