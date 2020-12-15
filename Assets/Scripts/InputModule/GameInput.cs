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


}
