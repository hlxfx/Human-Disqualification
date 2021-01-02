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
            List<Vector3> temp = MapInterface.FindPath(player.transform.position, Camera.main.ScreenToWorldPoint(GameInput.GetMousePos()));
            foreach (var item in temp)
            {
                Debug.Log(item);
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
