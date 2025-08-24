using System;
using Battle_Management.Card.Base;
using Battle_Management.Card.Data;
using DG.Tweening;
using UnityEngine;

namespace Battle_Management.Card.Type.Battle.Base
{
    [RequireComponent(typeof(System.Animation))]
    internal class BattleCardBase : MonoBehaviour, ICard, IBattleCard
    {
        [field: Header("Component")]
        [field: SerializeField] private RectTransform Rect;
        
        [field: Header("Card Data")]
        [field: SerializeField] private CardSO CardData;
        
        private System.Animation Animation;

        private void Awake()
        {
            Animation = GetComponent<System.Animation>();
        }
        
        #region ICard
            public void GetDrawChance(out float chance)
            {
                CardData.GetDrawChance(out chance);
            }

            public void MoveToParent(Transform parent, float duration = 1f, Action OnComplete = null)
            {
                Rect.SetParent(parent);
                MoveToZero(duration)
                    .OnComplete(() => OnComplete?.Invoke());
            }
        #endregion
        
        #region Animation
            private Tween MoveToZero(float duration = 1f)
            {
                return Animation.MoveToZero(duration);
            }
        #endregion
    }
}