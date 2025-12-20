using System;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Item;
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
        [field: SerializeField] protected Color HaveItemColor;
        [field: SerializeField] protected Color NoItemColor;
        
        [field: Header("Animation Settings")]
        [field: SerializeField] protected DoAnimation DoAnimation;
        [field: SerializeField] protected DoScale ScaleUpSettings;
        [field: SerializeField] protected DoScale ScaleDownSettings;

        [field: Header("Status Settings")]
        [field: SerializeField] protected bool Interactable;

        protected ItemSO ItemData;
        
        #region Interaction
            public virtual void Selected() { }
            public virtual void UnSelected() { }
            public virtual void Use() { }
        #endregion
        
        #region Item
            public virtual bool TryAddItem(ItemSO item) => false;
            public virtual void TryAddItem(ItemSO item, Action onComplete) { }
            public virtual void TryGetItem(out ItemSO item) => item = ItemData;
        #endregion
        
        #region Status
            public bool IsEmpty()
            {
                return ItemData is null;
            }
            
            public void SetInteractable(bool interactable)
            {
                Interactable = interactable;
                if (!interactable) DoAnimation?.DoScale_UI(SlotRect, ScaleDownSettings);
            }
        #endregion
    }
}