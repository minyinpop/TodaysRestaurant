using System;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using TMPro;
using UnityEngine;

namespace Common.Tutorial_Tip.Main
{
    [RequireComponent(typeof(DoAnimation))]
    public abstract class TutorialTip : MonoBehaviour
    {
        [field: Header("動畫")]
        [field: SerializeField] protected new DoAnimation animation;
        
        [field: Header("提示框")]
        [field: SerializeField] protected CanvasGroup canvasGroup;
        [field: SerializeField] protected DoFade_CanvasGroup fadeInSettings;
        [field: SerializeField] protected DoFade_CanvasGroup fadeOutSettings;
        
        [field: Header("文字")]
        [field: SerializeField] protected TextMeshProUGUI tipText;

        protected virtual void Awake()
        {
            if (animation is null)
            {
                throw new InvalidOperationException($"{nameof(animation)} 沒有被掛載。");
            }
            
            if (canvasGroup is null)
            {
                throw new InvalidOperationException($"{nameof(canvasGroup)} 沒有被掛載。");
            }
            
            if (tipText is null)
            {
                throw new InvalidOperationException($"{nameof(tipText)} 沒有被掛載。");
            }
        }

        public abstract void ShowTip(Action onComplete);

        public void HideTip(Action onComplete)
        {
            animation.DoFade_CanvasGroup(
                canvasGroup: canvasGroup,
                settings: fadeOutSettings,
                onComplete: () =>
                {
                    onComplete.Invoke();
                    
                    gameObject.SetActive(false);
                });
        }
    }
}