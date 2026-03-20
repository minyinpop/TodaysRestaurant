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
    public sealed class PutIngredientSlot : PointerEvent, IItemSlot
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
        
        private IItem _targetItem;
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
            public bool TryAddItem(IItem item)
            {
                #region 條件檢查
                    if (!_initialized)
                    {
                        Debug.Log($"{name} > {GetType().Name} > need to initialize first.");
                        Destroy(gameObject);
                        return false;
                    }
                    
                    if (item is null)
                    {
                        Debug.Log($"{name} > {GetType().Name} > {nameof(TryAddItem)} > {nameof(item)} cannot be null.");
                        Destroy(gameObject);
                        return false;
                    }
                        
                    if (Item is not null)
                    {
                        return false;
                    }

                    if (_targetItem != item)
                    {
                    }
                #endregion
                
                Item = item;
                
                itemImage.sprite = Item.ItemSprite;
                itemImage.color = hadItemColor;
                return true;
            }

            public bool TryGetItem(out IItem item)
            {
                if (!_initialized)
                {
                    Debug.Log($"{name} > {GetType().Name} > need to initialize first.");
                    Destroy(gameObject);
                    item = null;
                    return false;
                }
                
                if (Item is null)
                {
                    item = null;
                    return false;
                }
                
                itemImage.sprite = null;
                itemImage.color = noItemColor;
                
                item = Item;
                Item = null;
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