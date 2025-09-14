using System.Collections.Generic;
using Data.Attribute;
using UnityEngine;

namespace Data.Card.Battle
{
    [CreateAssetMenu(menuName = "Minyinpop/Card/Battle", fileName = "New Data")]
    internal sealed class BattleCardSO : ScriptableObject
    {
        [field: SerializeField] public ChanceValue ChanceValue { get; private set; }
        [field: SerializeField] public DamageValue DamageValue { get; private set; }
        [field: SerializeField] private List<SkeletonAnimationValue> SkeletonAnimationValue;

        public void GetRandomAnimation(out SkeletonAnimationValue animation)
        {
            var randomIndex = Random.Range(0, SkeletonAnimationValue.Count);
            animation = SkeletonAnimationValue[randomIndex];
        }
    }
}