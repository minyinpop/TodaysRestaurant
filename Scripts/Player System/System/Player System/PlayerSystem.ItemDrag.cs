using Common.Item_Slot.New.Main;
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
                #region 從當前點擊到的格子獲取要被拖曳的物品
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
                
                if (_destinationSlot.Item is null)
                {
                    #region 把拖曳中的物品給添加到當前點擊的格子
                        if (_destinationSlot.TryAddItem(_draggedItem))
                        {
                            PlayerUISystem.RequireItemDragUI(false, null);

                            _sourceSlot = null;
                            _destinationSlot = null;
                            _draggedItem = null;
                        }
                    #endregion
                }
                else
                {
                    #region 與當前點擊的格子交換物品
                        /*
                         * 可能要幫 ItemSlot 做一個交換物品的 function。
                         */
                        if (_destinationSlot.TryGetItem(out var item))
                        {
                            if (_destinationSlot.TryAddItem(_draggedItem))
                            {
                                _draggedItem = item;
                                
                                PlayerUISystem.RequireItemDragUI(true, _draggedItem);
                                
                                _sourceSlot = _destinationSlot;
                                _destinationSlot = null;
                            }
                        }
                    #endregion
                }
            }
        }
    }
}