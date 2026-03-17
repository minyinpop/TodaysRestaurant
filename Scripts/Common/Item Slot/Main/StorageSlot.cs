using System;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Common.Item.Data;
using Common.Item.Data.Ingredient;
using Common.Pointer_Event;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Item_Slot.Main
{
    [RequireComponent(typeof(DoAnimation))]
    public abstract class StorageSlot : PointerEvent
    {
        [field: Header("Slot Settings")]
        [field: SerializeField] protected RectTransform slotRect;
        [field: SerializeField] protected Image slotImage;
        [field: SerializeField] protected Color focusSlotColor;
        [field: SerializeField] protected Color normalSlotColor;

        [field: Header("Item Settings")]
        [field: SerializeField] protected Image itemImage;
        [field: SerializeField] protected Sprite pointerEnterSprite;
        [field: SerializeField] protected Sprite pointerExitSprite;
        [field: SerializeField] protected Color haveItemColor;
        [field: SerializeField] protected Color noItemColor;
        
        [field: Header("Animation Settings")]
        [field: SerializeField] protected new DoAnimation animation;
        [field: SerializeField] protected DoScale scaleUpSettings;
        [field: SerializeField] protected DoScale scaleDownSettings;

        [field: Header("Status Settings")]
        [field: SerializeField] protected bool interactable;

        protected IItem _currentItem;
        
        #region Interaction
            public virtual void Selected() { }
            public virtual void UnSelected() { }
            public virtual void Use() { }
        #endregion
        
        #region Item
            public virtual bool TryAddItem(IItem item)
            {
                Debug.Log($"{gameObject.name} > {GetType().Name} > {nameof(TryAddItem)} > is not implemented, but you try to use it.");
                return false;
            }
            
            public virtual void TryAddItem(ItemSO itemData, Action onComplete)
            {
                Debug.Log($"{gameObject.name} > {GetType().Name} > {nameof(TryAddItem)} > is not implemented, but you try to use it.");
                onComplete?.Invoke();
            }

            public virtual bool TryAddItem(IIngredient ingredient)
            {
                Debug.Log($"{gameObject.name} > {GetType().Name} > {nameof(TryAddItem)} > is not implemented, but you try to use it.");
                return false;
            }

            public virtual void TryGetItem(out IItem itemData) => itemData = _currentItem;
            
            public virtual void TryPeekItem(out IItem itemData) => itemData = _currentItem;
            public virtual bool TryRemoveItem(IItem itemData) => false;
        #endregion
        
        #region Status
            public virtual bool IsEmpty()
            {
                return _currentItem is null;
            }
        #endregion
    }
}