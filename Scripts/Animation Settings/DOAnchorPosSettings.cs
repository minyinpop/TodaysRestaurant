using UnityEngine;

namespace Animation_Settings
{
    internal sealed class DOAnchorPosSettings
    {
        private readonly Vector3 TargetPos;
        private readonly float Duration;
        private readonly bool Snapping;
        
        public DOAnchorPosSettings(Vector3 TargetPos, float Duration, bool Snapping)
        {
            this.TargetPos = TargetPos;
            this.Duration = Duration;
            this.Snapping = Snapping;
        }

        public void GetValues(out Vector3 TargetPos, out float Duration, out bool Snapping)
        {
            TargetPos = this.TargetPos;
            Duration = this.Duration;
            Snapping = this.Snapping;
        }
    }
}