using System;
using Common.Object.Storage_Slot;
using Item;
using UnityEngine;

namespace Player_System.System.Child.Mouse_System.Child
{
    internal sealed class ItemDragSystem : MonoBehaviour
    {
        private StorageSlot _sourceSlot;
        private StorageSlot _destinationSlot;
        
        private ItemSO _draggedItem;
        
        public static event Action<bool, ItemSO> RequiresUI;

        public void OnClick(GameObject itemSlot)
        {
            if (itemSlot is null) return;
            if (_draggedItem is null) TryToTakeItem();
            else
            {
                _destinationSlot = itemSlot.GetComponent<StorageSlot>();
                if (_destinationSlot.IsEmpty()) PutItemToEmptySlot();
                else SwitchItem();
            }

            return;
            
            void TryToTakeItem()
            {
                _sourceSlot = itemSlot.GetComponent<StorageSlot>();
                _sourceSlot.TryGetItem(out var item);
                if (item is null)
                {
                    _sourceSlot = null;
                    return;
                }

                _draggedItem = item;
                RequiresUI?.Invoke(true, _draggedItem);
            }

            void PutItemToEmptySlot()
            {
                if (!_destinationSlot.TryAddItem(_draggedItem)) return;
                RequiresUI?.Invoke(false, null);
                
                _sourceSlot = null;
                _destinationSlot = null;
                _draggedItem = null;
            }

            void SwitchItem()
            {
                _destinationSlot.TryGetItem(out var item);
                if (!_destinationSlot.TryAddItem(_draggedItem)) return;
                _draggedItem = item;
                RequiresUI?.Invoke(true, _draggedItem);
                
                _sourceSlot = _destinationSlot;
                _destinationSlot = null;
            }
        }
    }
}