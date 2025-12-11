using System;
using DG.Tweening;
using UnityEngine;

namespace Animation_System.DOTween.Basic
{
    [Serializable]
    public sealed class DoScale
    {
        [field: Header("Values")]
        [field: SerializeField] private Vector3 EndValue;
        [field: SerializeField] private float Duration;
        
        [field: Header("Ease")]
        [field: SerializeField] private Ease Ease;
        
        public DoScale(Vector3 endValue, float duration, Ease ease)
        {
            EndValue = endValue;
            Duration = duration;
            
            Ease = ease;
        }
        
        public void GetValues(out Vector3 endValue, out float duration, out Ease ease)
        {
            endValue = EndValue;
            duration = Duration;
            
            ease = Ease;
        }
    }
}