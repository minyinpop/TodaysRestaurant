using System.Collections.Generic;
using Common.Enemy.Data;
using Common.Item.Data;
using Common.Scene_Starter;
using UnityEngine;

namespace Common.Enemy_Battle_Group
{
    [CreateAssetMenu(menuName = "Minyinpop/Enemy Battle Group", fileName = "New Data")]
    public sealed class EnemyBattleGroupSO : SceneStarterData
    {
        [field: SerializeField] private EnemySO[] enemiesData;
                                public EnemySO[] EnemiesData => enemiesData;
        [field: SerializeField] private ItemSO[] lootsData;
                                public IReadOnlyList<ItemSO> LootsData => lootsData;
    }
}