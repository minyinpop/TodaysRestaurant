using DG.Tweening;
using UnityEngine;

namespace Data.DOTween_Value
{
    internal sealed class DoRotateValue
    {
        private readonly Vector3 EndValue;
        private readonly float Duration;
        private readonly RotateMode RotateMode;
        
        private Ease Ease;

        public DoRotateValue(Vector3 EndValue, float Duration, RotateMode RotateMode, Ease Ease)
        {
            this.EndValue = EndValue;
            this.Duration = Duration;
            this.RotateMode = RotateMode;
            
            this.Ease = Ease;
        }
        
        public void GetValues(out Vector3 EndValue, out float Duration, out RotateMode RotateMode, out Ease Ease)
        {
            EndValue = this.EndValue;
            Duration = this.Duration;
            RotateMode = this.RotateMode;
            
            Ease = this.Ease;
        }
    }
}