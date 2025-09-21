using DG.Tweening;

namespace Data.Animation.DOTween.Basic
{
    internal sealed class DoFade_CanvasGroup
    {
        private readonly float EndValue;
        private readonly float Duration;
        
        private readonly Ease Ease;
        
        public DoFade_CanvasGroup(float endValue, float duration, Ease ease)
        {
            EndValue = endValue;
            Duration = duration;
            
            Ease = ease;
        }

        public void GetValues(out float endValue, out float duration, out Ease ease)
        {
            endValue = EndValue;
            duration = Duration;
            
            ease = Ease;
        }
    }
}