using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[CSharpCallLua]
public interface IMassageInterface 
{    
    /// <summary>
     /// 处理消息
     /// </summary>
     /// <param name="id">消息Id</param>
     /// <param name="param1">参数1</param>
     /// <param name="param2">参数2</param>
     /// <returns>是否终止消息派发</returns>
    bool HandleMessage(int id, string param1, GameObject param2);

    /// <summary>
    /// 消息优先级
    /// </summary>
    /// <returns></returns>
    int MessagePriority();
}
