using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class RoleManager : IScript
{
    [SerializeField]
    private PlayerManager playerManager;
    [SerializeField]
    private EnemyManager enemyManager;
    private GameObject gameObject;
    public bool haveRole;
    
    public RoleManager(GameObject gameObject)
    {
        this.gameObject = gameObject;
        haveRole = false;
        LoadRole();
    }

    public void LoadRole()
    {
        playerManager = new PlayerManager(gameObject);
        enemyManager = new EnemyManager(gameObject);
        haveRole = true;
    }

    public  GameObject GetPlayer()
    {
        return playerManager.GetPlayer();
    }

    public PlayerState GetPlayerState()
    {
        return playerManager.GetState();
    }

    public EnemyManager GetEnemyManager()
    {
        return enemyManager;
    }

    /// <summary>
    /// 从存档中读取数据后需进行的初始化
    /// </summary>
    public void Start()
    {
        if (haveRole)
        {
            playerManager.Start();
            enemyManager.Start();

            //存档中的位置，或初始位置
            GameManager.instance.rootMassageNode.SendEvent(MassageList.loadMap, "Floor_1");
        }
    }

    // Update is called once per frame
    public void Update()
    {
        if (haveRole)
        {
            playerManager.Update();
            enemyManager.Update();
        }
    }
}
