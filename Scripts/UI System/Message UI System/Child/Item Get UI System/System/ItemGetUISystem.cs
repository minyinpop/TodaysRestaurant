using System;
using System.Collections.Generic;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Common.Item.Data;
using Common.Value;
using UI_System.Message_UI_System.Child.Item_Get_UI_System.Object.Main;
using UnityEngine;

namespace UI_System.Message_UI_System.Child.Item_Get_UI_System.System
{
    [RequireComponent(typeof(DoAnimation))]
    public sealed class ItemGetUISystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private new DoAnimation animation;
        
        [field: Header("Mask")]
        [field: SerializeField] private CanvasGroup mask;
        [field: SerializeField] private DoFade_CanvasGroup maskFadeInSettings;
        [field: SerializeField] private DoFade_CanvasGroup maskFadeOutSettings;
        
        [field: Header("UI")]
        [field: SerializeField] private CanvasGroup itemGetUI;
        [field: SerializeField] private DoFade_CanvasGroup itemGetUIFadeInSettings;
        [field: SerializeField] private DoFade_CanvasGroup itemGetUIFadeOutSettings;

        private void Awake()
        {
            if (animation is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(animation)} cannot be null.");
            }

            if (mask is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(mask)} cannot be null.");
            }
            
            if (itemGetUI is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(itemGetUI)} cannot be null.");           
            }
            
            mask.gameObject.SetActive(false);
            itemGetUI.gameObject.SetActive(false);
        }

        public void ShowUI(PopUpUIContent content, IReadOnlyList<ItemSO> items, Action onConfirm)
        {
            #region 初始設定 mask
                mask.gameObject.SetActive(true);
            #endregion
            
            #region 跑 mask 的動畫
                animation.DoFade_CanvasGroup(
                    canvasGroup: mask,
                    settings: maskFadeInSettings,
                    onComplete: () =>
                    {
                        #region 初始設定 itemGetUI
                            itemGetUI.gameObject.SetActive(true);
                            itemGetUI.GetComponent<ItemGetUI>().SetMessage(content);
                        #endregion
                            
                        #region 跑 itemGetUI 的動畫
                            animation.DoFade_CanvasGroup(
                                canvasGroup: itemGetUI,
                                settings: itemGetUIFadeInSettings,
                                onComplete: () =>
                                {
                                    itemGetUI.GetComponent<ItemGetUI>().ShowItemGet(
                                        items: items,
                                        onConfirm: () =>
                                        {
                                            HideUI(
                                                onComplete: () =>
                                                {
                                                    onConfirm.Invoke();
                                                });
                                        });
                                });
                        #endregion
                    });
            #endregion
        }

        private void HideUI(Action onComplete)
        {
            animation.DoFade_CanvasGroup(
                canvasGroup: itemGetUI,
                settings: itemGetUIFadeOutSettings,
                onComplete: () =>
                {
                    animation.DoFade_CanvasGroup(
                        canvasGroup: mask,
                        settings: maskFadeOutSettings,
                        onComplete: () =>
                        {
                            mask.gameObject.SetActive(false);
                            itemGetUI.gameObject.SetActive(false);
                            
                            itemGetUI.GetComponent<ItemGetUI>().ClearMessage();
                            onComplete.Invoke();
                        });
                });
        }
    }
}