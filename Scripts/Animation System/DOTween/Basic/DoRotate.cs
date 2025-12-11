using System;
using DG.Tweening;
using UnityEngine;

namespace Animation_System.DOTween.Basic
{
    [Serializable]
    public sealed class DoRotate
    {
        [field: Header("Values")]
        [field: SerializeField] private Vector3 EndValue;
        [field: SerializeField] private float Duration;
        [field: SerializeField] private RotateMode RotateMode;
        
        [field: Header("Ease")]
        [field: SerializeField] private Ease Ease;
        
        public DoRotate(Vector3 endValue, float duration, RotateMode rotateMode, Ease ease)
        {
            EndValue = endValue;
            Duration = duration;
            RotateMode = rotateMode;
            
            Ease = ease;
        }

        public void GetValues(out Vector3 endValue, out float duration, out RotateMode rotateMode, out Ease ease)
        {
            endValue = EndValue;
            duration = Duration;
            rotateMode = RotateMode;
            
            ease = Ease;
        }
    }
}