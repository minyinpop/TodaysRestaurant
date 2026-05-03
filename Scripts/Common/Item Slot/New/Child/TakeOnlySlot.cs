using System;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Common.Item_Slot.New.Main;
using Common.Item.Data;
using Common.Pointer_Event;
using Player_System.System.Player_System;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Item_Slot.New.Child
{
    [RequireComponent(typeof(DoAnimation))]
    public sealed class TakeOnlySlot : PointerEvent, ItemSlot
    {
        [field: Header("格子")]
        [field: SerializeField] private RectTransform slotRect;
        [field: SerializeField] private Image itemImage;
        
        [field: Header("動畫")]
        [field: SerializeField] private new DoAnimation animation;
        [field: SerializeField] private DoScale scaleUpSettings;
        [field: SerializeField] private DoScale scaleDownSettings;

        public event Action<TakeOnlySlot> OnTake;

        private bool _initialized;
        private bool _interactable;
        
        public IItem Item { get; private set; }

        #region PointerEvent
            protected override void OnPointerEnter()
            {
                if (_interactable)
                {
                    animation.DoScale_UI(
                        rect: slotRect,
                        settings: scaleUpSettings);
                }
            }

            protected override void OnPointerExit()
            {
                if (_interactable)
                {
                    animation.DoScale_UI(
                        rect: slotRect,
                        settings: scaleDownSettings);
                }
            }

            protected override void OnPointerClick()
            {
                if (_interactable)
                {
                    PlayerSystem.DragItemFromItemSlot(this);
                }
            }
        #endregion

        #region ItemSlot
            public bool AddItem(IItem item)
            {
                return false;
            }

            public bool GetItem(out IItem item)
            {
                if (!_initialized)
                {
                    throw new InvalidOperationException("請先初始化。");
                }

                if (OnTake is null)
                {
                    throw new InvalidOperationException($"{nameof(OnTake)} 沒有被其它 class 訂閱。");
                }

                itemImage.gameObject.SetActive(false);
                itemImage.sprite = null;
                
                item = Item;
                
                if (Item is not null)
                {
                    OnTake.Invoke(this);
                }
                
                Item = null;
                
                return true;
            }

            public bool ChangeItem(IItem targetItem, out IItem slotItem)
            {
                slotItem = null;
                return false;
            }
        #endregion
        
        public void Initialize(IItem item)
        {
            if (_initialized)
            {
                Debug.Log($"{name} 已經初始化過了！");
                return;
            }
            
            #region 格子狀態
                _initialized = true;
                Item = item;
            #endregion
            
            #region 物品顯示
                if (Item is null)
                {
                    itemImage.sprite = null;
                    itemImage.gameObject.SetActive(false);
                }
                else
                {
                    itemImage.sprite = Item.ItemSprite;
                    itemImage.gameObject.SetActive(true);
                }
            #endregion
        }

        public void SetInteractable(bool interactable)
        {
            if (!_initialized)
            {
                Debug.Log($"請先將 {name} 初始化！");
                return;
            }
            
            _interactable = interactable;

            if (!_interactable)
            {
                animation.DoScale_UI(
                    rect: slotRect,
                    settings: scaleDownSettings);
            }
        }
    }
}