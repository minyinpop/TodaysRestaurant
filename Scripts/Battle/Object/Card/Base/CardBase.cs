using System;
using Battle.Object.Card.Child_System;
using Battle.Object.Card.Data;
using Data.DOTween_Value;
using UnityEngine;

namespace Battle.Object.Card.Base
{
    [RequireComponent(typeof(AnimationSystem))]
    internal abstract class CardBase : MonoBehaviour, ICard
    {
        [field: Header("Component")]
        [field: SerializeField] private RectTransform Rect;
        
        [field: Header("Child System")]
        [field: SerializeField] private AnimationSystem AnimationSystem;
        
        [field: Header("Data")]
        [field: SerializeField] private CardSO CardSO;

        public void GetDrawChance(out float DrawChance)
        {
            CardSO.GetDrawChance(out DrawChance);
        }

        public void MoveToParent(Transform Parent, DoAnchorPosValue DoAnchorPosValue, Action OnComplete = null)
        {
            Rect.SetParent(Parent);
            AnimationSystem.MoveTo(DoAnchorPosValue, OnComplete);
        }
    }
}