using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class counterattack : BaseNode
{
    public override bool excute()
    {
        //---------------测试代码
        Debug.Log("反击！");
        return true;
        //--------------------
        return base.excute();
    }
}
