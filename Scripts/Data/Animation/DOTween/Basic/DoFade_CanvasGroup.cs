using System;
using DG.Tweening;
using UnityEngine;

namespace Data.Animation.DOTween.Basic
{
    [Serializable]
    internal sealed class DoFade_CanvasGroup
    {
        [field: Header("Values")]
        [field: SerializeField] private bool EndValue;
        [field: SerializeField] private float Duration;
        
        [field: Header("Ease")]
        [field: SerializeField] private Ease Ease;
        
        public DoFade_CanvasGroup(bool endValue, float duration, Ease ease)
        {
            EndValue = endValue;
            Duration = duration;
            
            Ease = ease;
        }

        public void GetValues(out bool endValue, out float duration, out Ease ease)
        {
            endValue = EndValue;
            duration = Duration;
            
            ease = Ease;
        }
    }
}