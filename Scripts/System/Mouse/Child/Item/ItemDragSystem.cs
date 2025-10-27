using System.Mouse.Child.Item.Object;
using System.Storage_Slot.Base;
using Data.Item.Base;
using UnityEngine;

namespace System.Mouse.Child.Item
{
    internal sealed class ItemDragSystem : MonoBehaviour
    {
        [field: Header("Drag UI")]
        [field: SerializeField] private GameObject DragUIPrefab;
        [field: SerializeField] private Transform DragUIParent;
        
        private GameObject DragUI;
        private ItemDragUI DragUIScript;
        
        private ITem DraggedItemData;

        public void OnClick(GameObject itemSlot)
        {
            if (itemSlot is null) return;
            if (DraggedItemData is null) TryToTakeItem();
            else
            {
                var currentItemSlotScript = itemSlot.GetComponent<StorageSlot>();
                if (currentItemSlotScript.IsEmpty()) PutItemToEmptySlot(currentItemSlotScript);
                else SwitchItem(currentItemSlotScript);
            }

            return;
            
            void TryToTakeItem()
            {
                itemSlot.GetComponent<StorageSlot>().GetItem(out var item);
                if (item is null) return;
                DraggedItemData = item;
                DragUI = Instantiate(DragUIPrefab, DragUIParent);
                DragUIScript = DragUI.GetComponent<ItemDragUI>();
                DraggedItemData.GetItemSprite(out var sprite);
                DragUIScript.SetSprite(sprite);
            }

            void PutItemToEmptySlot(StorageSlot currentStorageSlotScript)
            {
                currentStorageSlotScript.TryAddItem(DraggedItemData, out var isSuccess);
                if (!isSuccess) return;
                Destroy(DragUI);
                DragUI = null;
                DragUIScript = null;
                DraggedItemData = null;
            }

            void SwitchItem(StorageSlot currentStorageSlotScript)
            {
                currentStorageSlotScript.GetItem(out var item);
                currentStorageSlotScript.TryAddItem(DraggedItemData, out _);
                DraggedItemData = item;
                DraggedItemData.GetItemSprite(out var sprite);
                DragUIScript.SetSprite(sprite);
            }
        }
    }
}