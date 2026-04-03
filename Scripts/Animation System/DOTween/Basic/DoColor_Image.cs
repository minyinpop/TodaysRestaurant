using System;
using DG.Tweening;
using UnityEngine;

namespace Animation_System.DOTween.Basic
{
    [Serializable]
    public class DoColor_Image
    {
        [field: Header("Values")]
        [field: SerializeField] private Color EndValue;
        [field: SerializeField] private float Duration;
        
        [field: Header("Ease")]
        [field: SerializeField] private Ease Ease;
        
        public DoColor_Image(Color endValue, float duration, Ease ease)
        {
            EndValue = endValue;
            Duration = duration;
            
            Ease = ease;
        }

        public void GetValues(out Color endValue, out float duration, out Ease ease)
        {
            endValue = EndValue;
            duration = Duration;
            ease = Ease;
        }
    }
}