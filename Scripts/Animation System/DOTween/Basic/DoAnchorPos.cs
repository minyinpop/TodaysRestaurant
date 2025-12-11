using System;
using DG.Tweening;
using UnityEngine;

namespace Animation_System.DOTween.Basic
{
    [Serializable]
    public sealed class DoAnchorPos
    {
        [field: Header("Values")]
        [field: SerializeField] private Vector3 EndValue;
        [field: SerializeField] private float Duration;
        [field: SerializeField] private bool Snapping;

        [field: Header("Ease")]
        [field: SerializeField] private Ease Ease;
        
        public DoAnchorPos(Vector3 endValue, float duration, bool snapping, Ease ease)
        {
            EndValue = endValue;
            Duration = duration;
            Snapping = snapping;
            
            Ease = ease;
        }

        public void GetValues(out Vector3 endValue, out float duration, out bool snapping, out Ease ease)
        {
            endValue = EndValue;
            duration = Duration;
            snapping = Snapping;
            
            ease = Ease;
        }
    }
}