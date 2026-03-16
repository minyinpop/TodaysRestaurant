using System;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using UnityEngine;

namespace UI_System.Explore_UI_System
{
    [RequireComponent(typeof(DoAnimation))]
    public sealed class ExploreUISystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private new DoAnimation animation;
        
        [field: Header("Object")]
        [field: SerializeField] private CanvasGroup mask;
        [field: SerializeField] private DoFade_CanvasGroup fadeInSettings;
        [field: SerializeField] private DoFade_CanvasGroup fadeOutSettings;

        private void Awake()
        {
            if (mask is null)
            {
                Debug.Log($"{nameof(ExploreUISystem)} > {nameof(mask)} cannot be null.");
                Destroy(gameObject);
                return;
            }
            
            mask.gameObject.SetActive(false);
        }

        public void FadeIn(Action onComplete = null)
        {
            mask.gameObject.SetActive(true);
            
            animation.DoFade_CanvasGroup(
                canvasGroup: mask,
                settings: fadeInSettings,
                onComplete: onComplete);
        }

        public void FadeOut(Action onComplete = null)
        {
            animation.DoFade_CanvasGroup(
                canvasGroup: mask,
                settings: fadeOutSettings,
                onComplete: () =>
                {
                    mask.gameObject.SetActive(false);
                    onComplete?.Invoke();
                });
        }
    }
}