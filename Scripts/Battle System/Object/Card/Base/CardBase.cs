using System;
using Battle_System.Object.Card.Data;
using Battle_System.Object.Card.System;
using Data.DOTween_Values;
using UnityEngine;

namespace Battle_System.Object.Card.Base
{
    [RequireComponent(typeof(AnimationSystem))]
    internal abstract class CardBase : MonoBehaviour, ICard
    {
        [field: Header("Component")]
        [field: SerializeField] private RectTransform Rect;
        [field: SerializeField] private AnimationSystem AnimationSystem;
        
        [field: Header("Data")]
        [field: SerializeField] private CardSO CardSO;

        public void GetDrawChance(out float GetDrawChance)
        {
            CardSO.GetDrawChance(out GetDrawChance);
        }

        public void MoveToParent(Transform Parent, AnchorPosValue AnchorPosValue, Action OnComplete = null)
        {
            Rect.SetParent(Parent);
            AnimationSystem.MoveTo(AnchorPosValue, OnComplete);
        }
    }
}