using DG.Tweening;
using UnityEngine;

namespace DoTween_Settings
{
    internal sealed class DoRotateSettings
    {
        private readonly Vector3 TargetValue;
        private readonly float Duration;
        private readonly RotateMode RotateMode;
        private readonly Ease Ease;

        public DoRotateSettings(Vector3 TargetValue, float Duration, RotateMode RotateMode, Ease Ease)
        {
            this.TargetValue = TargetValue;
            this.Duration = Duration;
            this.RotateMode = RotateMode;
            this.Ease = Ease;
        }
        
        public void GetValues(out Vector3 TargetValue, out float Duration, out RotateMode RotateMode, out Ease Ease)
        {
            TargetValue = this.TargetValue;
            Duration = this.Duration;
            RotateMode = this.RotateMode;
            Ease = this.Ease;
        }
    }
}