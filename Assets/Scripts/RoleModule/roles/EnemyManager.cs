using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EnemyManager : IScript
{
    [SerializeField]
    private List<EnemyState> enemyState;
    private Transform parentTrans;

    public EnemyManager(GameObject gameObject)
    {
        parentTrans = gameObject.transform;
        enemyState = new List<EnemyState>();
    }

    // Start is called before the first frame update
    public void Start()
    {
        foreach (var item in enemyState)
        {
            item.Start();
        }
    }

    // Update is called once per frame
    public void Update()
    {
        foreach (var item in enemyState)
        {
            item.Update();
        }
    }

    public bool CreatEnemy(string name)
    {
        GameObject enemyPrefab = GeneraMethod.LoadGameObject("prefabs/player/" + name);
        if (enemyPrefab)
        {
            enemyPrefab = GameObject.Instantiate(enemyPrefab) as GameObject;
            enemyPrefab.transform.SetParent(parentTrans);
            enemyPrefab.GetComponent<SpriteRenderer>().sortingOrder = 9;
            enemyPrefab.AddComponent<ColliderEvent>();
            enemyState.Add(new EnemyState(enemyPrefab));
            return true;
        }
        Debug.LogError("不存在该对象：prefabs/player/" + name);
        return false;
    }

    public void TestSetPos(Transform pos = null)
    {
        foreach (var item in enemyState)
        {
            GameObject player = RoleInterface.GetPlayer();
            GameObject enemy = item.GetGameObject();
            if (pos)
            {
                enemy.transform.position = new Vector3(pos.position.x, pos.position.y, 0);
            }

            Vector3 offset = new Vector3(enemy.GetComponent<BoxCollider2D>().offset.x, enemy.GetComponent<BoxCollider2D>().offset.y, 0f);
            List<Vector3> path = MapInterface.FindPath(enemy.transform.position + offset, player.transform.position + offset);
            if (path != null)
            {
                if (item.curState._state != States.autoMove) item.SetState(States.autoMove); ;
                path.RemoveAt(0);
                item.curState.OnAutoMove(player.GetComponent<Animator>(), path, 0.01f);
            }
        }
    }
}
