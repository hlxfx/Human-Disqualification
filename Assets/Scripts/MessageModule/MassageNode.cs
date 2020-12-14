using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassageNode 
{
    //物体本身
    private GameObject gameObject = null;
    /// <summary>
    /// 节点优先级
    /// </summary>
    public int EventNodePriority { set; get; }

    /// <summary>
    /// 所有监听集合
    /// </summary>
    private Dictionary<int, List<IMassageInterface>> mListeners;

    /// <summary>
    /// 子消息节点
    /// </summary>
    private List<MassageNode> mNodeList;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="gameObject">当前物体</param>
    /// <param name="eventNodePriority">节点事件执行优先级</param>
    public MassageNode(GameObject gameObject,int eventNodePriority = 0)
    {
        this.gameObject = gameObject;
        mListeners = new Dictionary<int, List<IMassageInterface>>();
        mNodeList = new List<MassageNode>();
        EventNodePriority = eventNodePriority;
    }

    /// <summary>
    /// 挂载一个子消息节点到当前节点上
    /// </summary>
    /// <param name="node">消息节点</param>
    /// <returns>如果当前节点里面已经包含要添加的这个子节点那么返回false</returns>
    public bool AttachEventNode(MassageNode node)
    {
        if (node == null)
        {
            return false;
        }

        if (mNodeList.Contains(node))
        {
            return false;
        }
        int pos = 0;
        for (int i = 0; i < mNodeList.Count; i++)
        {
            if (node.EventNodePriority > mNodeList[i].EventNodePriority)
            {
                break;
            }
            pos++;
        }

        mNodeList.Insert(pos, node);
        return true;
    }

    /// <summary>
    /// 卸载一个子消息节点
    /// </summary>
    /// <param name="node">消息节点</param>
    /// <returns>如果子节点不存在那么返回false</returns>
    public bool DetachEventNode(MassageNode node)
    {
        if (!mNodeList.Contains(node))
        {
            return false;
        }
        mNodeList.Remove(node);
        return true;
    }

    /// <summary>
    /// 挂载一个消息监听器到当前的消息节点
    /// </summary>
    /// <param name="key">消息ID</param>
    /// <param name="listener">消息监听器</param>
    /// <returns>当前消息节点已经挂载了这个消息监听器那么返回false</returns>
    public bool AttachEventListener(int key, IMassageInterface listener)
    {
        if (listener == null)
        {
            return false;
        }
        if (!mListeners.ContainsKey(key))
        {
            mListeners.Add(key, new List<IMassageInterface>() { listener });
            return true;
        }
        if (mListeners[key].Contains(listener))
        {
            return false;
        }
        int pos = 0;
        for (int i = 0; i < mListeners[key].Count; i++)
        {
            if (listener.MessagePriority() > mListeners[key][i].MessagePriority())
            {
                break;
            }
            pos++;
        }
        mListeners[key].Insert(pos, listener);
        return true;
    }

    /// <summary>
    /// 卸载一个消息监听器
    /// </summary>
    /// <returns>如果当前消息监听器不存在那么返回false</returns>
    public bool DetachEventListener(int key, IMassageInterface listener)
    {
        if (mListeners.ContainsKey(key) && mListeners[key].Contains(listener))
        {
            mListeners[key].Remove(listener);
            return true;
        }
        return false;
    }

    public void SendEvent(int key, object param1 = null, object param2 = null)
    {
        DispatchEvent(key, param1, param2);
    }

    /// <summary>
    /// 派发消息到子消息节点以及自己节点下的监听器上
    /// </summary>
    /// <param name="key">消息ID</param>
    /// <param name="param1"></param>
    /// <param name="param2"></param>
    /// <returns>如果中断消息返回true</returns>
    private bool DispatchEvent(int key, object param1, object param2)
    {
        for (int i = 0; i < mNodeList.Count; i++)
        {
            if (mNodeList[i].DispatchEvent(key, param1, param2)) return true;
        }
        return TriggerEvent(key, param1, param2);
    }


    /// <summary>
    /// 消息触发
    /// </summary>
    /// <param name="key">消息id</param>
    /// <param name="param1"></param>
    /// <param name="param2"></param>
    /// <returns>是否中断</returns>
    private bool TriggerEvent(int key, object param1, object param2)
    {
        if (!gameObject.activeSelf || !gameObject.activeInHierarchy || (gameObject.GetComponent<Renderer>() && !gameObject.GetComponent<Renderer>().enabled))
        {
            return false;
        }

        if (!mListeners.ContainsKey(key))
        {
            return false;
        }
        List<IMassageInterface> listeners = mListeners[key];
        for (int i = 0; i < listeners.Count; i++)
        {
            if (listeners[i].HandleMessage(key, param1, param2)) return true;
        }
        return false;
    }

}
