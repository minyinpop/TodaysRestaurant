using DG.Tweening;
using UnityEngine;

namespace DoTween_Settings
{
    internal sealed class DoScaleSettings
    {
        private readonly Vector3 TargetValue;
        private readonly float Duration;
        private readonly Ease Ease;

        public DoScaleSettings(Vector3 TargetValue, float Duration, Ease Ease)
        {
            this.TargetValue = TargetValue;
            this.Duration = Duration;
            this.Ease = Ease;
        }
        
        public void GetValues(out Vector3 TargetValue, out float Duration, out Ease Ease)
        {
            TargetValue = this.TargetValue;
            Duration = this.Duration;
            Ease = this.Ease;
        }
    }
}