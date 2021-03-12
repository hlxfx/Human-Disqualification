using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block:BaseNode
{
    public override bool excute()
    {
        //---------------测试代码
        Debug.Log("格挡！");
        return true;
        //--------------------
        return base.excute();
    }
}
