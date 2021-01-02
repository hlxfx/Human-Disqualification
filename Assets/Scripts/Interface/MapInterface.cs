using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapInterface 
{
    public static List<Vector3> FindPath(Vector3 start, Vector3 end)
    {
        return GameManager.instance.mapManager.FindPath(start, end);
    }
}
