using System;
using Battle_System.Card_System.Animation_System;
using Battle_System.Card_System.Data;
using DoTween_Settings;
using UnityEngine;

namespace Battle_System.Card_System.Base
{
    [RequireComponent(typeof(AnimationSystem))]
    internal abstract class CardBase : MonoBehaviour, ICard
    {
        [field: Header("Component")]
        [field: SerializeField] private RectTransform Rect;
        [field: SerializeField] private AnimationSystem AnimationSystem;
        
        [field: Header("Data")]
        [field: SerializeField] private CardSO CardData;

        private bool IsFront;

        #region ICard
            public void GetDrawChance(out float DrawChance)
            {
                CardData.GetDrawChance(out DrawChance);
            }

            public void MoveToParent(Transform Parent, DoAnchorPosSettings Settings, Action OnComplete = null)
            {
                Rect.SetParent(Parent);
                AnimationSystem.MoveTo(Settings, OnComplete);
            }

            public void FlipCard()
            {
            }
        #endregion
    }
}