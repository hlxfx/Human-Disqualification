using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstance : MonoBehaviour
{
    public bool isAlive = true;
    private EnemyAtrribute enemyAtrribute;

    private void Start()
    {
        LoadNewEnemy();
    }

    public void LoadNewEnemy(/*怪物id或其他可判别怪物属性配置的字段*/)
    {
        enemyAtrribute = new EnemyAtrribute(/*传入*/);
        //同时可在此更换不同怪物的gameobject
    
    }

    public EnemyAtrribute GetAtrribute()
    {
        return enemyAtrribute;
    }

    /*
    血量减少后判断是否死亡，死亡将isalive置false  下次更新怪物池时将移除它 
    ...
    
     */
}
