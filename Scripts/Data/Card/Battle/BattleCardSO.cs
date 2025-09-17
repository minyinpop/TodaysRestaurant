using System.Collections.Generic;
using Data.Animation.Spine;
using UnityEngine;

namespace Data.Card.Battle
{
    [CreateAssetMenu(menuName = "Minyinpop/Card/Battle", fileName = "New Data")]
    internal sealed class BattleCardSO : ScriptableObject
    {
        #region Damage
            [field: Header("Damage")]
            [field: SerializeField] private float Damage;

            public void GetDamage(out float damage)
            {
                damage = Damage;
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
        
        #region SkeletonAnimation
            [field: SerializeField] private List<SkeletonAnimationSettings> SkeletonAnimationValue;

            public void GetRandomAnimation(out SkeletonAnimationSettings animationSettings)
            {
                var randomIndex = Random.Range(0, SkeletonAnimationValue.Count);
                animationSettings = SkeletonAnimationValue[randomIndex];
            }
        #endregion
    }
}