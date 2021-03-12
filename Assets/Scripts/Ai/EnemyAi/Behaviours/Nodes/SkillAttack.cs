using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAttack : BaseNode
{
    public override bool excute()
    {
        //---------------测试代码
        Debug.Log("使用了技能攻击");
        return true;
        //--------------------


        return base.excute();
    }
}
