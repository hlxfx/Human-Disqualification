using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : BaseNode
{
    public override bool excute()
    {
        //---------------测试代码
        Debug.Log("闪避！");
        return true;
        //--------------------


        return base.excute();
    }
}
