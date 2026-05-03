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
    public sealed class BackpackSlot : PointerEvent, ItemSlot
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
        
        public IItem Item { get; private set; }
        
        private void Awake()
        {
            #region 必要條件檢查
                if (animation is null)
                {
                    throw new InvalidOperationException(nameof(animation));
                }
                
                if (slotRect is null)
                {
                    throw new InvalidOperationException(nameof(slotRect));
                }

                if (itemImage is null)
                {
                    throw new InvalidOperationException(nameof(itemImage));
                }
            #endregion
        }
        
        #region 鼠標互動
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
        
        #region 物品事件
            public bool AddItem(IItem item)
            {
                if (item is null)
                {
                    throw new ArgumentNullException(nameof(item));
                }
                
                if (Item is not null)
                {
                    return false;
                }
                    
                Item = item;
                
                itemImage.sprite = Item.ItemSprite;
                itemImage.gameObject.SetActive(true);
                return true;
            }

            public bool GetItem(out IItem ingredient)
            {
                if (Item is null)
                {
                    ingredient = null;
                    return false;
                }
                    
                itemImage.gameObject.SetActive(false);
                itemImage.sprite = null;
                
                ingredient = Item;
                Item = null;
                return true;
            }
                
            public bool ChangeItem(IItem targetItem, out IItem slotItem)
            {
                if (targetItem is null)
                {
                    throw new ArgumentNullException(nameof(targetItem));
                }
                
                if (Item is null)
                {
                    throw new InvalidOperationException(nameof(Item));
                }
                
                slotItem = Item;
                Item = targetItem;
                    
                itemImage.sprite = Item.ItemSprite;
                return true;
            }
            
            public void RemoveItem()
            {
                if (Item is null)
                {
                    return;
                }
            
                itemImage.gameObject.SetActive(false);
                itemImage.sprite = null;
            
                Item.Remove();
                Item = null;
            }
        #endregion

        public void Refresh()
        {
            itemImage.gameObject.SetActive(Item is not null);
            itemImage.sprite = Item?.ItemSprite;
        }
    }
}