using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class PlayerManager : IScript
{
    [SerializeField]
    private PlayerState playerState;
    private GameObject player;

    public PlayerManager(GameObject gameObject)
    {
        player =  GeneraMethod.LoadGameObject("prefabs/player/player");
        player = GameObject.Instantiate(player) as GameObject;
        player.transform.SetParent(gameObject.transform);
        player.GetComponent<SpriteRenderer>().sortingOrder = 9;
        player.AddComponent<ColliderEvent>();
        playerState = new PlayerState(player);
    }

    // Start is called before the first frame update
    public void Start()
    {
        playerState.Start();
    }

    // Update is called once per frame
    public void Update()
    {
        playerState.Update();
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public PlayerState GetState()
    {
        return playerState;
    }
}
