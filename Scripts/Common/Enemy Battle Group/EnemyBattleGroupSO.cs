using System.Collections.Generic;
using Common.Item.Data;
using Explore_System.System.Child.Battle_System.Object.Creature.Enemy;
using UnityEngine;

namespace Common.Enemy_Battle_Group
{
    [CreateAssetMenu(menuName = "Minyinpop/Enemy Battle Group", fileName = "New Data")]
    public sealed class EnemyBattleGroupSO : ScriptableObject
    {
        [field: Header("出場的敵人")]
        [field: SerializeField] private BattleEnemyObject[] enemyObjects;
                                public IReadOnlyList<BattleEnemyObject> EnemyObjects => enemyObjects;
                                
        [field: Header("戰利品")]
        [field: SerializeField] private ItemSO[] lootsData;
                                public IReadOnlyList<ItemSO> LootsData => lootsData;
    }
}