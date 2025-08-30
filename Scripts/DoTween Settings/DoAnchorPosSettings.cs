using DG.Tweening;
using UnityEngine;

namespace DoTween_Settings
{
    internal sealed class DoAnchorPosSettings
    {
        private readonly Vector3 TargetPosition;
        private readonly float Duration;
        private readonly bool Snapping;
        private readonly Ease Ease;

        public DoAnchorPosSettings(Vector3 TargetPosition, float Duration, bool Snapping, Ease Ease)
        {
            this.TargetPosition = TargetPosition;
            this.Duration = Duration;
            this.Snapping = Snapping;
            this.Ease = Ease;
        }
        
        public void GetValues(out Vector3 TargetPosition, out float Duration, out bool Snapping, out Ease Ease)
        {
            TargetPosition = this.TargetPosition;
            Duration = this.Duration;
            Snapping = this.Snapping;
            Ease = this.Ease;
        }
    }
}