using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EnemyPoor
{
    /// <summary>
    /// 用于控制场景中怪物最大数量，即怪物实例
    /// </summary>
    [SerializeField]
    private List<EnemyInstance> poor;
    [SerializeField]
    private List<EnemyInstance> prefabs;

    public EnemyPoor()
    {
        poor = new List<EnemyInstance>();
        prefabs = new List<EnemyInstance>(); /*用于缓存为用到或后续很可能会用到的怪物prefab*/

    }

    public List<EnemyInstance> GetEnemies()
    {
        return poor;
    }

    /// <summary>
    /// 判断怪物存活，定时更新怪物池,此中可抛出异常
    /// </summary>
    /// <returns></returns>
    public bool Update()
    {
        foreach (var item in poor)
        {
            if (!item.isAlive)
            {
                item.gameObject.SetActive(false);
                prefabs.Add(item);
                poor.Remove(item);
            }
        }
        return true;
    }

    public bool AddEnemy(/*怪物id*/)
    {
        for (int i = 0; i < prefabs.Count; i++)
        {
            if(/*prefabs[i].GetAtrribute().id == 怪物id*/ true)
            {
                prefabs[i].gameObject.SetActive(true);
                poor.Add(prefabs[i]);
                prefabs.RemoveAt(i);
                return true;
            }
        }
        /*如果没有该怪物缓存则直接新加
        先加载怪物的prefab，然后给怪物添加EnemyInstance组件 ， 然后将其加入到poor中
        */

        return true;
    }


    /*
    也可以在场景一开始时便往场景中增加一些怪物 
     */
}
