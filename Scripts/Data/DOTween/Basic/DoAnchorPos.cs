using DG.Tweening;
using UnityEngine;

namespace Data.DOTween.Basic
{
    internal sealed class DoAnchorPos
    {
        private readonly Vector3 EndValue;
        private readonly float Duration;
        private readonly bool Snapping;

        private readonly Ease Ease;
        
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