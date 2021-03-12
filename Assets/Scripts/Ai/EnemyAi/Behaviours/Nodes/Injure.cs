using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Injure : BaseNode
{
    public override bool excute()
    {
        //---------------测试代码
        Debug.Log("受伤了！");
        return true;
        //--------------------

        return base.excute();
    }
}
