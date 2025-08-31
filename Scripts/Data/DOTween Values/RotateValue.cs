using DG.Tweening;
using UnityEngine;

namespace Data.DOTween_Values
{
    internal sealed class RotateValue
    {
        private readonly Vector3 EndValue;
        private readonly float Duration;

        private readonly Ease Ease;
        
        public RotateValue(Vector3 EndValue, float Duration, Ease Ease)
        {
            this.EndValue = EndValue;
            this.Duration = Duration;
            
            this.Ease = Ease;
        }
        
        public void GetValues(out Vector3 EndValue, out float Duration, out Ease Ease)
        {
            EndValue = this.EndValue;
            Duration = this.Duration;
            
            Ease = this.Ease;
        }
    }
}