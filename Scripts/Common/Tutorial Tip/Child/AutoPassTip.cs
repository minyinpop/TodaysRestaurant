using System;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Common.Tutorial_Tip.Main;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Tutorial_Tip.Child
{
    public sealed class AutoPassTip : TutorialTip
    {
        [field: Header("進度條")]
        [field: SerializeField] private Image progressImage;
        [field: SerializeField] private DoFillAmount countDownSettings;
        
        public override void ShowTip(Action onComplete)
        {
            gameObject.SetActive(true);
            
            animation.DoFade_CanvasGroup(
                canvasGroup: canvasGroup,
                settings: fadeInSettings,
                onComplete: () =>
                {
                    if (progressImage is null)
                    {
                        onComplete?.Invoke();
                        return;
                    }
                    
                    animation.DoFillAmount(
                        image: progressImage,
                        settings: countDownSettings,
                        onComplete: () =>
                        {
                            HideTip(onComplete);
                        });
                });
        }
    }
}