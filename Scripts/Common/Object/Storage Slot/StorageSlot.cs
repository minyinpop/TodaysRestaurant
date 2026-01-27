using System;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Common.Item;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Object.Storage_Slot
{
    [RequireComponent(typeof(DoAnimation))]
    public abstract class StorageSlot : PointerEvent
    {
        [field: Header("Slot Settings")]
        [field: SerializeField] protected RectTransform SlotRect;
        [field: SerializeField] protected Image SlotImage;
        [field: SerializeField] protected Color SelectedColor;
        [field: SerializeField] protected Color UnSelectedColor;

        [field: Header("Item Settings")]
        [field: SerializeField] protected RectTransform ItemRect;
        [field: SerializeField] protected Image ItemImage;
        [field: SerializeField] protected Sprite PointerEnterSprite;
        [field: SerializeField] protected Sprite PointerExitSprite;
        [field: SerializeField] protected Color HaveItemColor;
        [field: SerializeField] protected Color NoItemColor;
        
        [field: Header("Animation Settings")]
        [field: SerializeField] protected DoAnimation DoAnimation;
        [field: SerializeField] protected DoScale ScaleUpSettings;
        [field: SerializeField] protected DoScale ScaleDownSettings;

        [field: Header("Status Settings")]
        [field: SerializeField] protected bool Interactable;

        protected ItemSO _currentItemData;
        
        #region Interaction
            public abstract void Selected();
            public abstract void UnSelected();
            public abstract void Use();
        #endregion
        
        #region Item
            public virtual bool TryAddItem(ItemSO itemData) => false;
            public virtual void TryAddItem(ItemSO itemData, Action onComplete) => onComplete?.Invoke();
            public virtual void TryGetItem(out ItemSO itemData) => itemData = _currentItemData;
            public virtual void TryPeekItem(out ItemSO itemData) => itemData = _currentItemData;
            public virtual bool TryRemoveItem(ItemSO itemData) => false;
        #endregion
        
        #region Status
            public virtual bool IsEmpty()
            {
                return _currentItemData is null;
            }
        #endregion
    }
}