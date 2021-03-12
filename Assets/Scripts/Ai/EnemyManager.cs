using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace myTest
{
    public class EnemyManager : MonoBehaviour
    {
        //怪物池
        [SerializeField]
        protected EnemyPoor enemyPoor;
        //行为树
        [SerializeField]
        protected BehaviourTree behaviourTree;


        // Start is called before the first frame update
        void Start()
        {
            enemyPoor = new EnemyPoor();
            behaviourTree = new BehaviourTree();
        }


        void Update()
        {
            /*
             放入各种定时更新方法中都可(协程，mono中的update，或者其他的)
             */
            UpdateEnemyPoor();
            UpdateEnemyBehavior();
        }



        /// <summary>
        /// 用于更新怪物池
        /// </summary>
        /// <returns></returns>

        public bool UpdateEnemyPoor()
        {
            if (enemyPoor.Update())
            {
                return true;
            }
            return false;
        }

        public void UpdateEnemyBehavior()
        {
            behaviourTree.UpdateBehaviour(enemyPoor.GetEnemies());
        }
    }
}