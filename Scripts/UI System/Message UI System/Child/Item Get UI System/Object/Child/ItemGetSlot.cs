using System;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Common.Item.Data;
using Common.Pointer_Event;
using UnityEngine;
using UnityEngine.UI;
using ItemSlot = Common.Item_Slot.New.Main.ItemSlot;
using NotImplementedException = System.NotImplementedException;

namespace UI_System.Message_UI_System.Child.Item_Get_UI_System.Object.Child
{
    [RequireComponent(typeof(DoAnimation))]
    public sealed class ItemGetSlot : PointerEvent, ItemSlot
    {
        [field: Header("Animation")]
        [field: SerializeField] private RectTransform slotRect;
        [field: SerializeField] private new DoAnimation animation;
        [field: SerializeField] private DoScale scaleUpSettings;
        [field: SerializeField] private DoScale scaleDownSettings;
        
        [field: Header("Image")]
        [field: SerializeField] private Image itemImage;
        
        public IItem Item { get; private set; }

        private bool _canInteract;

        private void Awake()
        {
            if (slotRect is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(slotRect)} cannot be null.");
            }

            if (animation is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(animation)} cannot be null.");
            }
            
            itemImage.gameObject.SetActive(false);
        }

        #region PointerEvent
            protected override void OnPointerEnter()
            {
                if (_canInteract)
                {
                    animation.DoScale_UI(
                        rect: slotRect,
                        settings: scaleUpSettings);
                }
            }

            protected override void OnPointerExit()
            {
                if (_canInteract)
                {
                    animation.DoScale_UI(
                        rect: slotRect,
                        settings: scaleDownSettings);
                }
            }
        #endregion
        
        #region ItemSlot
            public bool AddItem(IItem item)
            {
                #region 檢查參數
                    if (item is null)
                    {
                        throw new ArgumentNullException($"{name} > {GetType().Name} > {nameof(AddItem)} > {nameof(item)} cannot be null.");
                    }
                #endregion

                #region 設定資料
                    Item = item;
                #endregion
                
                #region 設定顯示
                    itemImage.gameObject.SetActive(true);
                #endregion

                #region 開跑動畫
                    animation.DoScale_UI(
                        rect: slotRect,
                        settings: scaleDownSettings,
                        onComplete: () =>
                        {
                            _canInteract = true;
                        });
                #endregion
                
                return true;
            }

            public bool GetItem(out IItem item)
            {
                throw new NotImplementedException();
            }

            public bool ChangeItem(IItem targetItem, out IItem slotItem)
            {
                throw new NotImplementedException();
            }
        #endregion
    }
}