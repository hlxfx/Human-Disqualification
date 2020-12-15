﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class RoleManager : IScript
{
    [SerializeField]
    private PlayerManager playerManager;
    private GameObject gameObject;
    public bool haveRole;
    
    public RoleManager(GameObject gameObject)
    {
        this.gameObject = gameObject;
        haveRole = false;
    }

    public void LoadRole()
    {
        playerManager = new PlayerManager(gameObject);
    }

    /// <summary>
    /// 从存档中读取数据后需进行的初始化
    /// </summary>
    public void Start()
    {
        if (haveRole)
        {
            playerManager.Start();
        }
    }

    // Update is called once per frame
    public void Update()
    {
        if (haveRole)
        {
            playerManager.Update();
        }
    }
}
