using Common.Item_Slot.Main;
using Common.Item.Data;
using UI_System.Player_UI_System.Main;

namespace Player_System.System.Player_System
{
    public partial class PlayerSystem
    {
        private static IItemSlot _sourceSlot;
        private static IItemSlot _destinationSlot;
        
        private static IItem _draggedItem;
        
        public static void DragItemFromItemSlot(IItemSlot itemSlot)
        {
            if (itemSlot is null)
            {
                return;
            }
            
            if (_draggedItem is null)
            {
                #region 開始拖曳物品
                    _sourceSlot = itemSlot;
                    _sourceSlot.TryGetItem(out var item);
                    
                    if (item is null)
                    {
                        _sourceSlot = null;
                        return;
                    }

                    _draggedItem = item;
                    PlayerUISystem.RequireItemDragUI(true, _draggedItem);
                #endregion
            }
            else
            {
                _destinationSlot = other.GetComponent<StorageSlot>();
                
                if (_destinationSlot.IsEmpty())
                {
                    PutItemToEmptySlot();
                }
                // else SwitchItem(); // TODO [2025.12.31] 暫時禁止物品交互功能
            }

            return;
            
            void TryToTakeItem()
            {
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