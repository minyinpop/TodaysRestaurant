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
    public sealed class ServingNoteSlot : PointerEvent, ItemSlot
    {
        [field: Header("Component")]
        [field: SerializeField] private new DoAnimation animation;
        
        [field: Header("State")]
        [field: SerializeField] private bool canInteract;
        
        [field: Header("Slot RectTransform")]
        [field: SerializeField] private RectTransform slotRect;
        [field: SerializeField] private DoScale slotScaleUpSettings;
        [field: SerializeField] private DoScale slotScaleDownSettings;
        
        [field: Header("Item Image")]
        [field: SerializeField] private Image itemImage;
        [field: SerializeField] private Color hadItemColor;
        [field: SerializeField] private Color noItemColor;

        private IItem _targetItem;
        public IItem Item { get; private set; }

        private bool _initialized;
        
        private void Awake()
        {
            if (animation is null)
            {
                throw new System.ArgumentNullException($"{name} > {GetType().Name} > {nameof(animation)} is null.");
            }
            
            if (slotRect is null)
            {
                throw new System.ArgumentNullException($"{name} > {GetType().Name} > {nameof(slotRect)} is null.");
            }

            if (itemImage is null)
            {
                throw new System.ArgumentNullException($"{name} > {GetType().Name} > {nameof(itemImage)} is null.");
            }
        }
        
        #region PointerEvent
            protected override void OnPointerEnter()
            {
                if (canInteract)
                {
                    animation.DoScale_UI(slotRect, slotScaleUpSettings);
                }
            }

            protected override void OnPointerExit()
            {
                if (canInteract)
                {
                    animation.DoScale_UI(slotRect, slotScaleDownSettings);
                }
            }

            protected override void OnPointerClick()
            {
                if (canInteract)
                {
                    PlayerSystem.DragItemFromItemSlot(this);
                }
            }
        #endregion
        
        #region IItemSlot
            public bool AddItem(IItem item)
            {
                #region 檢查傳入的物品
                    if (item is null)
                    {
                        throw new System.ArgumentNullException($"{name} > {GetType().Name} > {nameof(AddItem)} > {nameof(item)} is null.");
                    }

                    if (item.ItemID != _targetItem.ItemID)
                    {
                        Debug.Log($"{item.ItemName} 與 {_targetItem.ItemName} 不是一樣的東西。");
                        return false;
                    }
                #endregion
                
                #region 檢查格子狀態
                    if (!_initialized)
                    {
                        throw new System.InvalidOperationException($"{name} > {GetType().Name} > need to initialize first.");
                    }
                    
                    if (Item is not null)
                    {
                        throw new System.InvalidOperationException($"{name} > {GetType().Name} > {nameof(AddItem)} > {nameof(Item)} is not null.");
                    }
                #endregion
                
                Item = item;
                
                itemImage.sprite = Item.ItemSprite;
                itemImage.color = hadItemColor;
                return true;
            }

            public bool GetItem(out IItem ingredient)
            {
                #region 檢查格子狀態
                    if (!_initialized)
                    {
                        throw new System.InvalidOperationException($"{name} > {GetType().Name} > need to initialize first.");
                    }
                    
                    if (Item is null)
                    {
                        ingredient = null;
                        return false;
                    }
                #endregion
                    
                itemImage.sprite = _targetItem.ItemSprite;
                itemImage.color = noItemColor;
                
                ingredient = Item;
                Item = null;
                return true;
            }
            
            public bool ChangeItem(IItem targetItem, out IItem slotItem)
            {
                #region 檢查傳入的物品
                    if (targetItem is null)
                    {
                        throw new System.ArgumentNullException($"{name} > {GetType().Name} > {nameof(AddItem)} > {nameof(targetItem)} is null.");
                    }

                    if (targetItem != _targetItem)
                    {
                        slotItem = null;
                        return false;
                    }
                #endregion
                
                #region 檢查格子狀態
                    if (Item is null)
                    {
                        throw new System.InvalidOperationException($"{name} > {GetType().Name} > {nameof(ChangeItem)} > {nameof(Item)} is null.");
                    }
                #endregion

                slotItem = Item;
                Item = targetItem;
                
                itemImage.sprite = Item.ItemSprite;
                return true;
            }
        #endregion

        public void Initialize(IItem item)
        {
            if (_initialized)
            {
                throw new System.InvalidOperationException($"{name} > {GetType().Name} > {nameof(_initialized)} is true.");
            }

            if (item is null)
            {
                throw new System.ArgumentNullException($"{name} > {GetType().Name} > {nameof(Initialize)} > {nameof(item)} is null.");
            }
            
            _initialized = true;
            _targetItem = item;
            
            itemImage.sprite = _targetItem.ItemSprite;
            itemImage.color = noItemColor;
        }
    }
}