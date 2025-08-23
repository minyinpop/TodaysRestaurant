using System;
using DG.Tweening;
using UnityEngine;

namespace Battle_Management_System.Card_System.Card_System.Battle_Card
{
    [RequireComponent(typeof(AnimationSystem))]
    internal abstract class BattleCardBase : MonoBehaviour, ICard, IBattleCard
    {
        [field: Header("Component")]
        [field: SerializeField] private RectTransform Rect;
        
        [field: Header("Card Data")]
        [field: SerializeField] private CardSO CardData;
        
        private AnimationSystem AnimationSystem;

        private void Awake()
        {
            AnimationSystem = GetComponent<AnimationSystem>();
        }

        #region ICard
            public int GetDrawChance()
            {
                return CardData.GetDrawChance();
            }

            public void MoveCardToSlot(Transform parent, Action onComplete)
            {
                Rect.SetParent(parent);
                
                AnimationSystem.MoveCardToVectorZero()
                    .OnComplete(() => onComplete?.Invoke());
            }

            public void MoveCardToSlotAndFlip(Transform parent, Action onComplete)
            {
                Rect.SetParent(parent);
                
                AnimationSystem.MoveCardToSlotAndFlip()
                    .OnComplete(() => onComplete?.Invoke());
            }
        #endregion
    }
}