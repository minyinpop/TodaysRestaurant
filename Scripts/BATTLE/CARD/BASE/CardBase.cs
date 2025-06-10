using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BATTLE.CARD.BASE
{
    internal abstract class CardBase : MonoBehaviour, ICard
    {
        private bool Selected { get; set; }
        
        private Sequence ShakePos { get; set; }
        private Sequence ShakeRot { get; set; }
        
        private Tween PosTween { get; set; }
        private Tween RotTween { get; set; }
        private Tween ScaleTween { get; set; }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ScaleTween = transform
                .DOScale(
                    endValue: Vector3.one * 1.2f,
                    duration:.2f
                    )
                .SetEase(
                    ease: Ease.OutSine
                    );
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ScaleTween = transform
                .DOScale(
                    endValue: Vector3.one,
                    duration: .2f
                    )
                .SetEase(
                    ease: Ease.OutSine
                    );
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Selected = !Selected;
            
            ShakePos = DOTween.Sequence();
            ShakeRot = DOTween.Sequence();

            ShakePos
                .Append(PosTween = transform
                    .DOShakePosition(
                        duration: .2f,
                        strength: 10,
                        vibrato: 10,
                        randomness: 90,
                        snapping: false,
                        fadeOut: true,
                        randomnessMode: ShakeRandomnessMode.Harmonic
                    )
                    .SetEase(
                        ease: Ease.OutSine
                    )
                )
                .Join(
                    PosTween = transform
                    .DOMoveY(
                        endValue: Selected ? transform.parent.position.y + 100 : transform.parent.position.y,
                        duration: .5f
                        )
                    .SetEase(
                        ease: Ease.OutSine
                        )
                );

            ShakeRot
                .Append(RotTween = transform
                    .DOShakeRotation(
                        duration: .2f,
                        strength: Vector3.forward * 10,
                        vibrato: 10,
                        randomness: 90,
                        fadeOut: true,
                        randomnessMode: ShakeRandomnessMode.Harmonic
                        )
                    .SetEase(
                        ease: Ease.OutSine
                        )
                )
                .Append(RotTween = transform
                    .DORotate(
                        endValue: Vector3.zero,
                        duration: .2f
                        )
                    .SetEase(
                        ease: Ease.OutSine
                        )
                );
        }
    }
}