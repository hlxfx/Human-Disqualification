using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 按顺序执行多个子节点，若成功执行一个子节点，则不继续执行下一个子节点。
/// （可以增加子节点权值，用于筛选行为）
/// </summary>
public class SelectNode : BaseNode
{

    public override bool excute()
    {
        foreach (var node in childNode)
        {
            //如果有一个子节点执行成功，则跳出
            if (node.excute() == true) 
            { 
                return true;
            }
        }
        return base.excute();
    }
}
