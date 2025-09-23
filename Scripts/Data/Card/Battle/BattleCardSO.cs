using System.Collections.Generic;
using Data.Animation.Spine;
using Data.General;
using Data.General.Enum;
using UnityEngine;

namespace Data.Card.Battle
{
    [CreateAssetMenu(menuName = "Minyinpop/Card/Battle Data", fileName = "New Data")]
    internal sealed class BattleCardSO : ScriptableObject
    {
        #region CardType
            [field: Header("Card Type")]
            [field: SerializeField] private CardType CardType;
            
            public void GetCardType(out CardType type)
            {
                type = CardType;
            }
        #endregion
        
        #region DrawChance
            [field: Header("Draw Chance")]
            [field: SerializeField, Range(0, 100)] private float DrawChance;
            
            public void GetDrawChance(out float chance)
            {
                chance = DrawChance;
            }
        #endregion
        
        #region Damage
            [field: Header("Damage")]
            [field: SerializeField] private Damage Damage;

            public void GetDamage(out Damage damage)
            {
                damage = Damage;
            }
        #endregion
        
        #region SkeletonAnimation
            [field: SerializeField] private List<SpineAnimation> AttackAnima;

            public void GetRandomAnimation(out SpineAnimation animationAnimation)
            {
                var randomIndex = Random.Range(0, AttackAnima.Count);
                animationAnimation = AttackAnima[randomIndex];
            }
        #endregion
    }
}