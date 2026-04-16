using System;
using System.Collections.Generic;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Audio_System.Data;
using Audio_System.Main;
using Common.Item_Slot.New.Child;
using Common.Item.Data;
using UI_System.Player_UI_System.Child.Backpack_UI_System.Object;
using UnityEngine;

namespace UI_System.Player_UI_System.Child.Backpack_UI_System.System
{
    [RequireComponent(typeof(DoAnimation))]
    public sealed class BackpackUISystem : MonoBehaviour
    {
        [field: Header("組件")]
        [field: SerializeField] private new DoAnimation animation;
        
        [field: Header("遮罩")]
        [field: SerializeField] private CanvasGroup maskCanvasGroup;
        [field: SerializeField] private DoFade_CanvasGroup maskFadeInSettings;
        [field: SerializeField] private DoFade_CanvasGroup maskFadeOutSettings;
        
        [field: Header("背包")]
        [field: SerializeField] private BackpackUI backpackUI;
        [field: SerializeField] private CanvasGroup backpackUICanvasGroup;
        [field: SerializeField] private DoFade_CanvasGroup backpackUIFadeInSettings;
        [field: SerializeField] private DoFade_CanvasGroup backpackUIFadeOutSettings;
        
        [field: Header("音效")]
        [field: SerializeField] private PlaySFXData openBackpackSFXData;
        [field: SerializeField] private PlaySFXData closeBackpackSFXData;
        
        private bool _canSetBackpackUI = true;
        
        private void Awake()
        {
            if (animation is null)
            {
                throw new InvalidOperationException($"{nameof(animation)} 沒有被掛載。");
            }

            if (maskCanvasGroup is null)
            {
                throw new InvalidOperationException($"{nameof(maskCanvasGroup)} 沒有被掛載。");
            }
            
            if (backpackUI is null)
            {
                throw new InvalidOperationException($"{nameof(backpackUI)} 沒有被掛載。");
            }

            if (backpackUICanvasGroup is null)
            {
                throw new InvalidOperationException($"{nameof(backpackUICanvasGroup)} 沒有被掛載。");
            }
        }
        
        #region 裝置輸入
            public void SetBackpackUI()
            {
                if (backpackUI.gameObject.activeSelf)
                {
                    CloseBackpackUI();
                }
                else
                {
                    OpenBackpackUI();
                }
            }
            
            public void SetBackpackUI(bool isEnabled)
            {
                if (isEnabled)
                {
                    OpenBackpackUI();
                }
                else
                {
                    CloseBackpackUI();
                }
            }
        #endregion

        public bool AddItem(IItem item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return backpackUI.AddItem(item);
        }

        public IReadOnlyList<BackpackSlot> GetBackpackSlots()
        {
            return backpackUI.BackpackSlots;
        }

        private void OpenBackpackUI()
        {
            if (!_canSetBackpackUI)
            {
                return;
            }

            _canSetBackpackUI = false;
            
            maskCanvasGroup.gameObject.SetActive(true);
                
            animation.DoFade_CanvasGroup(
                canvasGroup: maskCanvasGroup,
                settings: maskFadeInSettings,
                onComplete: () =>
                {
                    AudioSystem.Instance.UISFX.PlayOneShot(openBackpackSFXData);
                        
                    backpackUICanvasGroup.gameObject.SetActive(true);
                        
                    animation.DoFade_CanvasGroup(
                        canvasGroup: backpackUICanvasGroup,
                        settings: backpackUIFadeInSettings,
                        onComplete: () =>
                        {
                            _canSetBackpackUI = true;
                        });
                });
        }

        private void CloseBackpackUI()
        {
            if (!_canSetBackpackUI)
            {
                return;
            }
            
            _canSetBackpackUI = false;
            
            AudioSystem.Instance.UISFX.PlayOneShot(closeBackpackSFXData);
            
            animation.DoFade_CanvasGroup(
                canvasGroup: backpackUICanvasGroup,
                settings: backpackUIFadeOutSettings,
                onComplete: () =>
                {
                    backpackUICanvasGroup.gameObject.SetActive(false);
                    
                    animation.DoFade_CanvasGroup(
                        canvasGroup: maskCanvasGroup,
                        settings: maskFadeOutSettings,
                        onComplete: () =>
                        {
                            maskCanvasGroup.gameObject.SetActive(false);
                            
                            _canSetBackpackUI = true;
                        });
                });
        }
    }
}