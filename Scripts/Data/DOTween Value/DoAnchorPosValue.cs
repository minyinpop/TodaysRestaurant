using DG.Tweening;
using UnityEngine;

namespace Data.DOTween_Value
{
    internal sealed class DoAnchorPosValue
    {
        private readonly Vector3 EndValue;
        private readonly float Duration;
        private readonly bool Snapping;

        private Ease Ease;

        public DoAnchorPosValue(Vector3 EndValue, float Duration, bool Snapping, Ease Ease)
        {
            this.EndValue = EndValue;
            this.Duration = Duration;
            this.Snapping = Snapping;
            
            this.Ease = Ease;
        }

        public void GetValues(out Vector3 EndValue, out float Duration, out bool Snapping, out Ease Ease)
        {
            EndValue = this.EndValue;
            Duration = this.Duration;
            Snapping = this.Snapping;
            
            Ease = this.Ease;
        }
    }
}