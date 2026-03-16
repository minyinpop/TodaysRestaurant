using System;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Common.Value;
using UI_System.Message_UI_System.Child.Defeat_UI_System.Object;
using UnityEngine;

namespace UI_System.Message_UI_System.Child.Defeat_UI_System.System
{
    [RequireComponent(typeof(DoAnimation))]
    public sealed class DefeatUISystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private new DoAnimation animation;
        
        [field: Header("Mask")]
        [field: SerializeField] private CanvasGroup mask;
        [field: SerializeField] private DoFade_CanvasGroup fadeInSettings;
        [field: SerializeField] private DoFade_CanvasGroup fadeOutSettings;
        
        [field: Header("UI")]
        [field: SerializeField] private DefeatUI defeatUI;

        private void Awake()
        {
            if (animation is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(animation)} cannot be null.");
                Destroy(gameObject);
                return;
            }

            if (mask is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(mask)} cannot be null.");
                Destroy(gameObject);
                return;
            }
            
            if (defeatUI is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(defeatUI)} cannot be null.");
                Destroy(gameObject);
                return;
            }
            
            mask.gameObject.SetActive(false);
            defeatUI.gameObject.SetActive(false);
        }

        public void ShowUI(PopUpUIContent content, Action onConfirm)
        {
            mask.gameObject.SetActive(true);
            
            animation.DoFade_CanvasGroup(
                canvasGroup: mask,
                settings: fadeInSettings,
                onComplete: () =>
                {
                    defeatUI.ShowMessage(content, OnConfirm);
                    defeatUI.gameObject.SetActive(true);
                    return;

                    void OnConfirm()
                    {
                        animation.DoFade_CanvasGroup(
                            canvasGroup: mask,
                            settings: fadeOutSettings,
                            onComplete: () =>
                            {
                                mask.gameObject.SetActive(false);
                                
                                defeatUI.gameObject.SetActive(false);
                                defeatUI.ClearMessage();
                                
                                onConfirm.Invoke();
                            });
                    }
                });
        }
    }
}