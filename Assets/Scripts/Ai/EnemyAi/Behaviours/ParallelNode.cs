using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 同时执行多个节点。
/// </summary>
public class ParallelNode : BaseNode 
{
    public override bool excute()
    {
        foreach (var node in childNode)
        {
            //执行所有子节点
            node.excute();
        }
        return base.excute();
    }
}
