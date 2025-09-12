using DG.Tweening;
using UnityEngine;

namespace Data.DOTween.Basic
{
    internal sealed class DoRotate
    {
        private readonly Vector3 EndValue;
        private readonly float Duration;
        private readonly RotateMode RotateMode;
        
        private readonly Ease Ease;
        
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