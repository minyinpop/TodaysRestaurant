using System.Collections.Generic;
using Animation_System.Spine;
using Common.Value.Type;
using UnityEngine;

namespace Explore_System.System.Child.Battle_System.Object.Card.Battle
{
    [CreateAssetMenu(menuName = "Minyinpop/Card/Battle Data", fileName = "New Data")]
    public sealed class BattleCardSO : CardSO
    {
        [field: Header("戰鬥卡片資料 - 卡片類型")]
        [field: SerializeField] private BattleCardType battleCardType;
                                public BattleCardType BattleCardType => battleCardType;
        [field: SerializeField] private AttackType attackType;
                                public AttackType AttackType => attackType;
        
        [field: Header("戰鬥卡片資料 - 傷害")]
        [field: SerializeField] private int damage;
                                public int Damage => damage;
        
        [field: Header("戰鬥卡片資料 - 抽取機率")]
        [field: SerializeField, Range(0, 100)] private float drawChance;
                                               public float DrawChance => drawChance;
        
        [field: Header("戰鬥卡片資料 - 使用動畫")]
        [field: SerializeField] private List<SpineAnimation> attackSpine;
                                public IReadOnlyList<SpineAnimation> AttackSpine => attackSpine;
    }
}