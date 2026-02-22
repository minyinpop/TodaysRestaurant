using Common.Item.Data;
using Common.Storage_Slot.Main;
using UI_System.Player_UI_System.Main;
using UnityEngine;

namespace Player_System.Object
{
    internal sealed class ItemDragSystem : MonoBehaviour
    {
        private StorageSlot _sourceSlot;
        private StorageSlot _destinationSlot;
        
        private IItem _draggedItem;
        
        public void OnClick(GameObject itemSlot)
        {
            if (itemSlot is null) return;
            if (_draggedItem is null) TryToTakeItem();
            else
            {
                _destinationSlot = itemSlot.GetComponent<StorageSlot>();
                if (_destinationSlot.IsEmpty()) PutItemToEmptySlot();
                // else SwitchItem(); // TODO [2025.12.31] 暫時禁止物品交互功能
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
                PlayerUISystem.RequireItemDragUI(true, _draggedItem);
            }

            void PutItemToEmptySlot()
            {
                if (!_destinationSlot.TryAddItem(_draggedItem)) return;
                PlayerUISystem.RequireItemDragUI(false, null);
                
                _sourceSlot = null;
                _destinationSlot = null;
                _draggedItem = null;
            }

            /*
            void SwitchItem()
            {
                _destinationSlot.TryGetItem(out var item);
                if (!_destinationSlot.TryAddItem(_draggedItem)) return;
                _draggedItem = item;
                UISystem.RequireItemDragUI(true, _draggedItem);
                
                _sourceSlot = _destinationSlot;
                _destinationSlot = null;
            }
            */
        }
    }
}