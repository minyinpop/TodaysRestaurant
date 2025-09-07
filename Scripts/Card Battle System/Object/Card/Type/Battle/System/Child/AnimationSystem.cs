using System;
using Data.DOTween;
using DG.Tweening;
using UnityEngine;

namespace Card_Battle_System.Object.Card.Type.Battle.System.Child
{
    internal sealed class AnimationSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private RectTransform Rect;

        private Tween MoveTween;

        /// <summary>
        /// 移動的動畫，並會依照傳入的設定進行播放，並會在播放完畢後回傳
        /// </summary>
        /// <param name="settings">移動的動畫參數</param>
        /// <param name="onComplete">完成後的回傳</param>
        /// <returns>移動的動畫參數</returns>
        public Tween MoveTo(DoAnchorPos settings, Action onComplete = null)
        {
            MoveTween?.Kill();
            
            settings.GetValues(out var endValue, out var duration, out var snapping, out var ease);
            
            MoveTween = Rect
                .DOAnchorPos(endValue, duration, snapping)
                .SetEase(ease)
                .OnComplete(() =>
                {
                    Rect.anchoredPosition = endValue;
                    onComplete?.Invoke();
                })
                .OnKill(() => MoveTween = null);

            return MoveTween;
        }
    }
}