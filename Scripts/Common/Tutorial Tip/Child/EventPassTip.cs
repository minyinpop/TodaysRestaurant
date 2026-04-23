using System;
using Common.Tutorial_Tip.Main;

namespace Common.Tutorial_Tip.Child
{
    public sealed class EventPassTip : TutorialTip
    {
        public override void ShowTip(Action onComplete)
        {
            gameObject.SetActive(true);
            
            animation.DoFade_CanvasGroup(
                canvasGroup: canvasGroup,
                settings: fadeInSettings,
                onComplete: () =>
                {
                    onComplete.Invoke();
                });
        }
    }
}