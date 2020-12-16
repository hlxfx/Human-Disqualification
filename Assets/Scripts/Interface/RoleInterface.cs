using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RoleInterface 
{
    public static GameObject GetPlayer()
    {
        if (GameManager.instance.roleManager.haveRole)
        {
            return GameManager.instance.roleManager.GetPlayer();
        }
        return null;
    }
    
}
