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

    public static void OnAutoMove(Animator ani,List<Vector3> dir,float offset)
    {
        GameManager.instance.roleManager.GetPlayerState().curState.OnAutoMove(ani, dir, offset);
        return;
    }

    public static States GetPlayerState()
    {
        return GameManager.instance.roleManager.GetPlayerState().GetState();
    }

    public static void SetPlayerState(States state)
    {
        GameManager.instance.roleManager.GetPlayerState().SetState(state);
    }


}
