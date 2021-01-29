using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用于功能测试
/// </summary>
public class TestManeger : MonoBehaviour
{
    public GameObject Grid;
    public GameObject player;
    public GameObject target;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameInput.GetMouseBtn(1))
        {
            player = RoleInterface.GetPlayer();
            offset = new Vector3(player.GetComponent<BoxCollider2D>().offset.x, player.GetComponent<BoxCollider2D>().offset.y, 0f);
            target.transform.position = Camera.main.ScreenToWorldPoint(GameInput.GetMousePos());
            List<Vector3> path = MapInterface.FindPath(player.transform.position + offset, target.transform.position);
            if(path!=null)
            {
                if (RoleInterface.GetPlayerState() != States.autoMove) RoleInterface.SetPlayerState(States.autoMove);
                path.RemoveAt(0);
                RoleInterface.OnAutoMove(player.GetComponent<Animator>(), path, 0.01f);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        SpriteRenderer[] temp = Grid.GetComponentsInChildren<SpriteRenderer>();
        Color color = new Color(0.0f, 1.0f, 1.0f, 1.0f);
        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i].gameObject.layer == 9)
                Gizmos.color = color;
            else
                Gizmos.color = Color.clear;
            Gizmos.DrawWireCube(temp[i].bounds.center, new Vector3(0.16f, 0.16f, 0));
        }
    }



}
