using System;
using Animation_System.DOTween;
using Item;
using UnityEngine;

namespace Common.Object.Storage_Slot.Base
{
    [RequireComponent(typeof(DoAnimation))]
    internal abstract class StorageSlot : PointerEvent
    {
        protected override void OnPointerEnter() { }
        protected override void OnPointerExit() { }

        public virtual void TryAddItem(ITem item, out bool isSuccess) { isSuccess = false; }
        public virtual void TryAddItem(ITem item, Action onComplete) { onComplete?.Invoke(); }
        
        public virtual void GetItem(out ITem item) { item = null; }
        
        public virtual bool IsEmpty() { return true; }

        public virtual void SetInteractable(bool interactable) { }
    }
}