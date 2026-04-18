using System.Collections.Generic;
using Audio_System.Data;
using Common.Item.Data;
using Common.Scene_Starter;
using Explore_System.System.Child.Battle_System.Object.Creature.Enemy.Main;
using UnityEngine;

namespace Common.Enemy_Battle_Group
{
    [CreateAssetMenu(menuName = "Minyinpop/Enemy Battle Group", fileName = "New Data")]
    public sealed class EnemyBattleGroupSO : SceneStarterData
    {
        [field: Header("出場的敵人")]
        [field: SerializeField] private BattleEnemyObject[] enemyObjects;
                                public IReadOnlyList<BattleEnemyObject> EnemyObjects => enemyObjects;
                                
        [field: Header("戰利品")]
        [field: SerializeField] private ItemSO[] lootsData;
                                public IReadOnlyList<ItemSO> LootsData => lootsData;
                                
        [field: Header("戰鬥音樂")]
        [field: SerializeField] private FadeInBGMData battleBGMData;
                                public FadeInBGMData BattleBGMData => battleBGMData;
    }
}