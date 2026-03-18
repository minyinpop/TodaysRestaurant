using System;
using System.Collections.Generic;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Common.Item.Data;
using Common.Item.Data.Ingredient;
using Common.Value;
using UI_System.Message_UI_System.Child.Item_Get_UI_System.Object;
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
        [field: SerializeField] private DoFade_CanvasGroup fadeInSettings;
        [field: SerializeField] private DoFade_CanvasGroup fadeOutSettings;
        
        [field: Header("UI")]
        [field: SerializeField] private ItemGetUI itemGetUI;

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
            
            if (itemGetUI is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(itemGetUI)} cannot be null.");
                Destroy(gameObject);
                return;
            }
            
            mask.gameObject.SetActive(false);
            itemGetUI.gameObject.SetActive(false);
        }

        public void ShowUI(PopUpUIContent content, IItem[] items, Action onConfirm)
        {
            mask.gameObject.SetActive(true);
            
            animation.DoFade_CanvasGroup(
                canvasGroup: mask,
                settings: fadeInSettings,
                onComplete: () =>
                {
                });
        }
    }
}