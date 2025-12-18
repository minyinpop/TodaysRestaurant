using System;
using DG.Tweening;
using UnityEngine;

namespace Animation_System.DOTween.Basic
{
    [Serializable]
    public sealed class DoValue_Slider
    {
        [field: Header("Values")]
        [field: SerializeField] private float EndValue;
        [field: SerializeField] private float Duration;
        [field: SerializeField] private bool Snapping;
        
        [field: Header("Ease")]
        [field: SerializeField] private Ease Ease;
        
        public DoValue_Slider(float endValue, float duration, bool snapping, Ease ease)
        {
            EndValue = endValue;
            Duration = duration;
            Snapping = snapping;
            
            Ease = ease;
        }

        public void GetValues(out float endValue, out float duration, out bool snapping, out Ease ease)
        {
            endValue = EndValue;
            duration = Duration;
            snapping = Snapping;
            
            ease = Ease;
        }
    }
}