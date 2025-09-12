using Data.DOTween.Basic;
using DG.Tweening;
using UnityEngine;

namespace System.Card_Battle_System.Object.Card.Type.Battle.System.Child
{
    internal sealed class AnimationSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private RectTransform Rect;
        
        [field: Header("Object")]
        [field: SerializeField] private GameObject Front;
        [field: SerializeField] private GameObject Back;

        private Tween CurrentSequence;
        private Tween MoveTween;
        private Tween RotateTween;
        private Tween ScaleTween;
        
        #region Combine Animation

        /// <summary>
        /// 反轉卡片到正面的動畫
        /// </summary>
        /// <param name="rotateSettings">旋轉的動畫參數</param>
        /// <param name="scaleSettings01">前半部分的動畫參數</param>
        /// <param name="scaleSettings02">後半部分的動畫參數</param>
        /// <returns>動畫形參</returns>
        public Tween FlipToFront(DoRotate rotateSettings, DoScale scaleSettings01, DoScale scaleSettings02)
            {
                CurrentSequence?.Kill();

                CurrentSequence = DOTween.Sequence()
                    .Append(RotateTo(rotateSettings))
                    .Join(ScaleTo(scaleSettings01)
                        .OnComplete(() => ScaleTo(scaleSettings02)))
                    .OnKill(() => CurrentSequence = null);
                
                return CurrentSequence;
            }
        #endregion

        #region Basic Animation
            /// <summary>
            /// 移動卡片到指定位置的動畫
            /// </summary>
            /// <param name="settings">動畫參數</param>
            /// <returns>動畫形參</returns>
            public Tween MoveTo(DoAnchorPos settings)
            {
                MoveTween?.Kill();
                
                settings.GetValues(out var endValue, out var duration, out var snapping, out var ease);
                
                MoveTween = Rect
                    .DOAnchorPos(endValue, duration, snapping)
                    .SetEase(ease)
                    .OnComplete(() => Rect.anchoredPosition = endValue)
                    .OnKill(() => MoveTween = null);

                return MoveTween;
            }

            /// <summary>
            /// 旋轉卡片到目標面的動畫
            /// </summary>
            /// <param name="settings">動畫參數</param>
            /// <returns>動畫形參</returns>
            public Tween RotateTo(DoRotate settings)
            {
                RotateTween?.Kill();
                
                settings.GetValues(out var endValue, out var duration, out var rotateMode, out var ease);

                RotateTween = Rect
                    .DORotate(endValue, duration, rotateMode)
                    .SetEase(ease)
                    .OnUpdate(() =>
                    {
                        var y = Rect.eulerAngles.y;

                        if (Front.activeSelf && y is < 360 and > 270 or < 90 and > 0)
                        {
                            Front.SetActive(false);
                            Back.SetActive(true);
                        }
                        else if (Back.activeSelf && y is < 270 and > 90)
                        {
                            Front.SetActive(true);
                            Back.SetActive(false);
                        }
                    })
                    .OnComplete(() => Rect.eulerAngles = endValue)
                    .OnKill(() => RotateTween = null);
                
                return RotateTween;
            }
            
            /// <summary>
            /// 縮放卡片到目標大小的動畫
            /// </summary>
            /// <param name="settings">動畫參數</param>
            /// <returns>動畫形參</returns>
            public Tween ScaleTo(DoScale settings)
            {
                ScaleTween?.Kill();
                
                settings.GetValues(out var endValue, out var duration, out var ease);
                
                ScaleTween = Rect
                    .DOScale(endValue, duration)
                    .SetEase(ease)
                    .OnComplete(() => Rect.localScale = endValue)
                    .OnKill(() => ScaleTween = null);
                
                return ScaleTween;
            }
        #endregion
    }
}