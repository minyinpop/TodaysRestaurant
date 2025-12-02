using Data.Item.Base;
using Tool;
using UnityEngine;

namespace System.Storage_Slot.Base
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