using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Common.Item_Slot.New.Main;
using Common.Item.Data;
using Common.Item.Data.Ingredient;
using Common.Pointer_Event;
using Player_System.System.Player_System;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Item_Slot.New.Child
{
    [RequireComponent(typeof(DoAnimation))]
    public sealed class PutIngredientSlot : PointerEvent, ItemSlot
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
        
        private bool _initialized;
        
        private IIngredient _targetItem;
        public IItem Item { get; private set; }
        
        private void Awake()
        {
            if (animation is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(animation)} cannot be null.");
                Destroy(gameObject);
                return;
            }
            
            if (slotRect is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(slotRect)} cannot be null.");
                Destroy(gameObject);
                return;
            }

            if (itemImage is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(itemImage)} cannot be null.");
                Destroy(gameObject);
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
                        throw new System.ArgumentNullException($"傳入的 {nameof(item)} 為空值。");
                    }

                    if (item is not IIngredient ingredient)
                    {
                        Debug.Log($"{item.ItemName} 不是 {nameof(IIngredient)} 類型，無法被放置到 {GetType().Name}");
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

                #region 檢查傳入的物品跟目標物品條件是否通過
                    if (ingredient.IngredientType != _targetItem.IngredientType)
                    {
                        return false;
                    }

                    if (ingredient.IngredientTier < _targetItem.IngredientTier)
                    {
                        return false;
                    }
                #endregion
                
                Item = item;
                
                itemImage.sprite = Item.ItemSprite;
                itemImage.color = hadItemColor;
                return true;
            }

            public bool GetItem(out IItem item)
            {
                #region 檢查格子狀態
                    if (!_initialized)
                    {
                        throw new System.InvalidOperationException($"{name} > {GetType().Name} > need to initialize first.");
                    }
                
                    if (Item is null)
                    {
                        item = null;
                        return false;
                    }
                #endregion
                
                itemImage.sprite = _targetItem.ItemSprite;
                itemImage.color = noItemColor;
                
                item = Item;
                Item = null;
                return true;
            }

            public bool ChangeItem(IItem targetItem, out IItem slotItem)
            {
                #region 檢查傳入的物品
                    if (targetItem is null)
                    {
                        throw new System.ArgumentNullException($"{name} > {GetType().Name} > {nameof(AddItem)} > {nameof(targetItem)} cannot be null.");
                    }

                    if (targetItem is not IIngredient ingredient)
                    {
                        throw new System.ArgumentException($"{name} > {GetType().Name} > {nameof(AddItem)} > {nameof(targetItem)} is not ingredient.");
                    }
                #endregion
                
                #region 檢查格子狀態
                    if (!_initialized)
                    {
                        throw new System.InvalidOperationException($"{name} > {GetType().Name} > need to initialize first.");
                    }

                    if (Item is null)
                    {
                        throw new System.InvalidOperationException($"{name} > {GetType().Name} > {nameof(ChangeItem)} > {nameof(Item)} cannot be null.");
                    }
                #endregion
                
                #region 檢查傳入的物品跟目標物品條件是否通過
                    if (ingredient.IngredientType != _targetItem.IngredientType)
                    {
                        slotItem = null;
                        return false;
                    }

                    if (ingredient.IngredientTier < _targetItem.IngredientTier)
                    {
                        slotItem = null;
                        return false;
                    }
                #endregion

                slotItem = Item;
                Item = targetItem;
                
                itemImage.sprite = Item.ItemSprite;
                itemImage.color = hadItemColor;
                return true;
            }
        #endregion
        
        public void Initialize(IIngredient item)
        {
            if (_initialized)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(_initialized)} is true.");
                Destroy(gameObject);
                return;
            }

            if (item is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(Initialize)} > {nameof(item)} cannot be null.)");
                Destroy(gameObject);
                return;
            }
            
            _initialized = true;
            _targetItem = item;
            
            itemImage.sprite = _targetItem.ItemSprite;
            itemImage.color = noItemColor;
        }
    }
}