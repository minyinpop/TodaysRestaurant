using System;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Item.Data;
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
        
        #region Interaction
            public virtual void Selected() =>
                throw new NotImplementedException();
            public virtual void UnSelected() =>
                throw new NotImplementedException();
            public virtual void Use() =>
                throw new NotImplementedException();
        #endregion
        
        #region Item
            public virtual void TryAddItem(ItemSO item, out bool isSuccess) =>
                throw new NotImplementedException();
            public virtual void TryAddItem(ItemSO item, Action onComplete) =>
                throw new NotImplementedException();
            public virtual void GetItem(out ItemSO item) =>
                throw new NotImplementedException();
        #endregion
        
        #region Status
            public virtual bool IsEmpty() =>
                throw new NotImplementedException();
            public virtual void SetInteractable(bool interactable) =>
                throw new NotImplementedException();
        #endregion
    }
}