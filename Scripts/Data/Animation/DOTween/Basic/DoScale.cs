using DG.Tweening;
using UnityEngine;

namespace Data.Animation.DOTween.Basic
{
    internal sealed class DoScale
    {
        private readonly Vector3 EndValue;
        private readonly float Duration;
        
        private readonly Ease Ease;
        
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