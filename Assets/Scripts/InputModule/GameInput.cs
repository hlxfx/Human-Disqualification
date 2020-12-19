using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameInput
{
    public static float GetAxis(string operate)
    {
        float axis = Input.GetAxis(operate);
        return axis;
    }


    public static bool GetKey(KeyCode operate)
    {
        bool axis = Input.GetKey(operate);
        return axis;
    }

    public static Vector3 GetMousePos()
    {
        return Input.mousePosition;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="btn">0 左键   1 右键   2 滚轮</param>
    /// <returns></returns>

    public static bool GetMouseBtn(int btn)
    {
        return Input.GetMouseButton(btn);
    }


}
