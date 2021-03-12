using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : BaseNode
{
    public override bool excute()
    {
        //---------------测试代码
        Debug.Log("使用了普通攻击");
        return true;
        //--------------------


        return base.excute();
    }
}
