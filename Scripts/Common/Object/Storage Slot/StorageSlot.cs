using System;
using Animation_System.DOTween;
using Item.Data;
using UnityEngine;

namespace Common.Object.Storage_Slot
{
    [RequireComponent(typeof(DoAnimation))]
    public abstract class StorageSlot : PointerEvent
    {
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