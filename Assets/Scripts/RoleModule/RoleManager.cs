using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class RoleManager : IScript
{
    [SerializeField]
    private PlayerManager playerManager;
    
    public RoleManager(GameObject gameObject)
    {
        playerManager = new PlayerManager(gameObject);
    }

    // Start is called before the first frame update
    public void Start()
    {
        playerManager.Start();
    }

    // Update is called once per frame
    public void Update()
    {
        playerManager.Update();
    }
}
