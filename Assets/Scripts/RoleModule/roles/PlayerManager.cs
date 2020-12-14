using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class PlayerManager : IScript
{
    [SerializeField]
    private PlayerState playerMove;

    public PlayerManager(GameObject gameObject)
    {
        GameObject player =  Resources.Load<GameObject>("prefabs/player");
        player = GameObject.Instantiate(player) as GameObject;
        player.transform.SetParent(gameObject.transform);
        player.GetComponent<SpriteRenderer>().sortingOrder = 9;
        playerMove = new PlayerState(player);
    }

    // Start is called before the first frame update
    public void Start()
    {
        playerMove.Start();
    }

    // Update is called once per frame
    public void Update()
    {
        playerMove.Update();
    }
}
