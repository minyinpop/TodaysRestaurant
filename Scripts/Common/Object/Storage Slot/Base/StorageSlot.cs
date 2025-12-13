using System;
using Animation_System.DOTween;
using Item;
using UnityEngine;

namespace Common.Object.Storage_Slot.Base
{
    [RequireComponent(typeof(DoAnimation))]
    public abstract class StorageSlot : PointerEvent
    {
        public virtual void OnSelected() =>
            throw new NotImplementedException();
        
        public virtual void TryAddItem(ITem item, out bool isSuccess) =>
            throw new NotImplementedException();
        public virtual void TryAddItem(ITem item, Action onComplete) =>
            throw new NotImplementedException();
        
        public virtual void GetItem(out ITem item) =>
            throw new NotImplementedException();
        
        public virtual bool IsEmpty() =>
            throw new NotImplementedException();
        
        public virtual void SetInteractable(bool interactable) =>
            throw new NotImplementedException();
    }
}