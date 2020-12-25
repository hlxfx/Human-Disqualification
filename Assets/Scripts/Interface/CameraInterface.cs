using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraInterface 
{
    public static void SetCameraPos(Vector3 pos)
    {
        GameManager.instance.cameraMove.SetCameraPos(pos);
    }

    public static bool SetTarget(Transform transform)
    {
        return GameManager.instance.cameraMove.ChangeTarget(transform);
    }
}
