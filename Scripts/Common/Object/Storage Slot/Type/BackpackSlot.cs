using Common.Item;
using UnityEngine;

namespace Common.Object.Storage_Slot.Type
{
    public sealed class BackpackSlot : StorageSlot
    {
        [field: SerializeField] private ItemSO currentItemData;
        
        #region PointerEvent
            protected override void OnPointerEnter()
            {
                if (!Interactable) return;
                DoAnimation.DoScale_UI(SlotRect, ScaleUpSettings);
            }

            protected override void OnPointerExit()
            {
                if (!Interactable) return;
                DoAnimation.DoScale_UI(SlotRect, ScaleDownSettings);
            }
        #endregion
        
        #region Interaction
            public override void Selected() { }
            public override void UnSelected() { }
            public override void Use() { }
        #endregion
        
        #region Item
            public override void TryGetItem(out ItemSO itemData)
            {
                itemData = currentItemData;
            }
        #endregion
    }
}