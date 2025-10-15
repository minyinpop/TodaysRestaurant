using System;
using DG.Tweening;
using UnityEngine;

namespace Data.Animation.DOTween.Basic
{
    [Serializable]
    internal sealed class DoFade_CanvasGroup
    {
        [field: Header("Values")]
        [field: SerializeField, Range(0, 1)] private float EndValue;
        [field: SerializeField] private float Duration;
        
        [field: Header("Ease")]
        [field: SerializeField] private Ease Ease;
        
        public DoFade_CanvasGroup(float endValue, float duration, Ease ease)
        {
            EndValue = endValue;
            Duration = duration;
            
            Ease = ease;
        }

        public void GetValues(out float endValue, out float duration, out Ease ease)
        {
            endValue = EndValue;
            duration = Duration;
            ease = Ease;
        }
    }
}