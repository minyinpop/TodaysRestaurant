using System.General;
using Data.Item.Base;
using General.Object.Item_Slot.Type;
using UnityEngine;

namespace System.Inventory.Item_Drag
{
    internal sealed class ItemDragSystem : PointerEvent
    {
        [field: Header("Drag UI")]
        [field: SerializeField] private GameObject DragUIPrefab;
        [field: SerializeField] private Transform DragUIParent;
        
        private GameObject DragUI;
        private ItemDragUI DragUIScript;
        
        private void OnEnable()
        {
            InventorySlot.OnClicked += OnClickItemSlot;
        }

        private void OnDisable()
        {
            InventorySlot.OnClicked -= OnClickItemSlot;
        }

        private bool OnClickItemSlot(ItemSO item)
        {
            if (item is null) return false;
            DragUI = Instantiate(DragUIPrefab, DragUIParent);
            DragUIScript = DragUI.GetComponent<ItemDragUI>();
            DragUIScript.Add(item);
            return true;
        }
    }
}