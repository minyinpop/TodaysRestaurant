using System.Collections.Generic;
using Animation_System.Spine;
using Common.Value;
using UnityEngine;

namespace Explore_System.System.Child.Battle_System.Object.Card.Battle
{
    [CreateAssetMenu(menuName = "Minyinpop/Card/Battle Data", fileName = "New Data")]
    public sealed class BattleCardSO : CardSO
    {
        [field: Header("抽取機率")]
        [field: SerializeField, Range(0, 100)] private float drawChance;
                                               public float DrawChance => drawChance;
        [field: Header("卡片傷害")]
        [field: SerializeField] private Damage damage;
                                public Damage Damage => damage;
        
        [field: Header("使用動畫")]
        [field: SerializeField] private List<SpineAnimation> attackSpine;
                                public IReadOnlyList<SpineAnimation> AttackSpine => attackSpine;
    }
}