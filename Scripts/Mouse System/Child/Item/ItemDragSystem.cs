using Common.Object.Storage_Slot;
using Item;
using Mouse_System.Child.Item.Object;
using UnityEngine;

namespace Mouse_System.Child.Item
{
    internal sealed class ItemDragSystem : MonoBehaviour
    {
        [field: Header("Drag UI Settings")]
        [field: SerializeField] private GameObject dragUIPrefab;
        [field: SerializeField] private Transform dragUIParent;
        private GameObject dragUI;
        private ItemDragUI dragUI_ItemDragUI;
        
        // Storage
        public StorageSlot sourceSlot; // TODO public to private
        public StorageSlot destinationSlot; // TODO public to private
        
        // Item
        private ItemSO draggedItem;

        public void OnClick(GameObject itemSlot)
        {
            if (itemSlot is null) return;
            if (draggedItem is null) TryToTakeItem();
            else
            {
                destinationSlot = itemSlot.GetComponent<StorageSlot>();
                if (destinationSlot.IsEmpty()) PutItemToEmptySlot();
                else SwitchItem();
            }

            return;
            
            void TryToTakeItem()
            {
                sourceSlot = itemSlot.GetComponent<StorageSlot>();
                sourceSlot.TryGetItem(out var item);
                if (item is null)
                {
                    sourceSlot = null;
                    return;
                }

                draggedItem = item;
                dragUI = Instantiate(dragUIPrefab, dragUIParent);
                dragUI_ItemDragUI = dragUI.GetComponent<ItemDragUI>();
                dragUI_ItemDragUI.SetSprite(item.ItemSprite);
            }

            void PutItemToEmptySlot()
            {
                if (!destinationSlot.TryAddItem(draggedItem)) return;
                Destroy(dragUI);
                dragUI = null;
                dragUI_ItemDragUI = null;
                sourceSlot = null;
                destinationSlot = null;
                draggedItem = null;
            }

            void SwitchItem()
            {
                destinationSlot.TryGetItem(out var item);
                if (!destinationSlot.TryAddItem(draggedItem)) return;
                draggedItem = item;
                dragUI_ItemDragUI.SetSprite(item.ItemSprite);
                destinationSlot = null;
                draggedItem = null;
            }
        }
    }
}