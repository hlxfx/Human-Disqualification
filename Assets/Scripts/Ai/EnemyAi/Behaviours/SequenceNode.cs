using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 按顺序执行多个子节点，若遇到一个子节点不能执行，则不继续执行下一个子节点。
/// </summary>
public class SequenceNode : BaseNode
{

    public override bool excute()
    {
        foreach (var node in childNode)
        {
            //如果有一个子节点执行失败，则跳出
            if (node.excute() == false)
            {
                return false;
            }
        }
        return base.excute();
    }
}
