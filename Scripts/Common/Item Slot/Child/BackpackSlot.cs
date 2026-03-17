using Common.Item_Slot.Main;
using Common.Item.Data;
using UnityEngine;

namespace Common.Item_Slot.Child
{
    public sealed class BackpackSlot : StorageSlot
    {
        [field: SerializeField] private ItemSO currentItemData;
        
        #region PointerEvent
            protected override void OnPointerEnter()
            {
                if (!interactable) return;
                animation.DoScale_UI(slotRect, scaleUpSettings);
            }

            protected override void OnPointerExit()
            {
                if (!interactable) return;
                animation.DoScale_UI(slotRect, scaleDownSettings);
            }
        #endregion
        
        #region Interaction
            public override void Selected() { }
            public override void UnSelected() { }
            public override void Use() { }
        #endregion
        
        #region Item
            public override void TryGetItem(out IItem itemData)
            {
                itemData = currentItemData;
            }
        #endregion
    }
}