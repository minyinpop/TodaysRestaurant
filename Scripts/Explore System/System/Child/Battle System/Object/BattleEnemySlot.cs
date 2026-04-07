using Explore_System.System.Child.Battle_System.Object.Creature.Enemy;
using UnityEngine;

namespace Explore_System.System.Child.Battle_System.Object
{
    public sealed class BattleEnemySlot : MonoBehaviour
    {
        public BattleEnemyObject battleEnemyObject { get; private set; }

        public bool SetEnemy(BattleEnemyObject enemyObject)
        {
            #region 必要條件檢查
                if (battleEnemyObject is not null)
                {
                    return false;
                }

                if (enemyObject is null)
                {
                    return false;
                }
            #endregion

            battleEnemyObject = Instantiate(enemyObject.gameObject, transform.position, Quaternion.identity, transform).GetComponent<BattleEnemyObject>();
            return true;
        }
    }
}