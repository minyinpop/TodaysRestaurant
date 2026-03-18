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
                #region 獲取當前處物格的物品並開始拖曳
                    _sourceSlot = itemSlot;
                    _sourceSlot.TryGetItem(out var item);
                    
                    if (item is null)
                    {
                        _sourceSlot = null;
                    }
                    else
                    {
                        _draggedItem = item;
                        PlayerUISystem.RequireItemDragUI(true, _draggedItem);
                    }
                #endregion
            }
            else
            {
                _destinationSlot = itemSlot;
                
                if (_destinationSlot.CurrentItem is null)
                {
                    #region 直接添加物品到該儲存格並結束拖曳
                        if (_destinationSlot.TryAddItem(_draggedItem))
                        {
                            PlayerUISystem.RequireItemDragUI(false, null);
                            
                            _sourceSlot = null;
                            _destinationSlot = null;
                            _draggedItem = null;
                        }
                    #endregion
                }
                // else SwitchItem(); // TODO [2025.12.31] 暫時禁止物品交互功能
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