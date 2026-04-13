using Audio_System.Data;
using Audio_System.Main;
using Common.Item_Slot.New.Main;
using Common.Item.Data;
using UI_System.Player_UI_System.Main;
using UnityEngine;

namespace Player_System.System.Player_System
{
    public partial class PlayerSystem
    {
        [field: Header("拖曳物品音效")]
        [field: SerializeField] private PlaySFXData itemDragSFX;
                                private static PlaySFXData _itemDragSFX;
        
        private static ItemSlot _sourceSlot;
        private static ItemSlot _destinationSlot;
        
        private static IItem _draggedItem;
        
        public static void DragItemFromItemSlot(ItemSlot itemSlot)
        {
            if (itemSlot is null)
            {
                return;
            }
            
            if (_draggedItem is null)
            {
                #region 從當前點擊到的格子獲取要被拖曳的物品
                    _sourceSlot = itemSlot;
                    _sourceSlot.GetItem(out var item);
                    
                    if (item is null)
                    {
                        _sourceSlot = null;
                    }
                    else
                    {
                        AudioSystem.Instance.UISFX.PlayOneShot(_itemDragSFX);
                        
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
                        if (_destinationSlot.AddItem(_draggedItem))
                        {
                            AudioSystem.Instance.UISFX.PlayOneShot(_itemDragSFX);
                            
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
                        if (_destinationSlot.ChangeItem(_draggedItem, out var item))
                        {
                            AudioSystem.Instance.UISFX.PlayOneShot(_itemDragSFX);
                            
                            _draggedItem = item;
                            
                            PlayerUISystem.RequireItemDragUI(true, _draggedItem);
                            
                            _sourceSlot = _destinationSlot;
                            _destinationSlot = null;
                        }
                    #endregion
                }
            }
        }
    }
}