using System;
using System.Collections.Generic;
using Common.Item.Data;
using Common.Player.Child.Player_Inventory_Saver.Child;
using Common.Player.Child.Player_Inventory_Saver.Main;
using UI_System.Player_UI_System.Child.Backpack_UI_System.System;
using UI_System.Player_UI_System.Child.Hotbar_UI_System.System;
using UI_System.Player_UI_System.Child.Item_Drag_UI_System.System;
using UnityEngine;

namespace UI_System.Player_UI_System.Main
{
    public sealed class PlayerUISystem : MonoBehaviour
    {
        [field: Header("Components")]
        [field: SerializeField] private HotbarUISystem hotbarUISystem;
                                private static HotbarUISystem _hotbarUISystem;
        [field: SerializeField] private BackpackUISystem backpackUISystem;
                                private static BackpackUISystem _backpackUISystem;
        [field: SerializeField] private ItemDragUISystem itemDragUISystem;
                                private static ItemDragUISystem _itemDragUISystem;

        private void Awake()
        {
            #region 必要條件檢查
                if (hotbarUISystem is null)
                {
                    throw new InvalidOperationException(nameof(hotbarUISystem));
                }
                
                if (backpackUISystem is null)
                {
                    throw new InvalidOperationException(nameof(backpackUISystem));
                }
                
                if (itemDragUISystem is null)
                {
                    throw new InvalidOperationException(nameof(itemDragUISystem));
                }
            #endregion
            
            _hotbarUISystem = hotbarUISystem;
            _backpackUISystem = backpackUISystem;
            _itemDragUISystem = itemDragUISystem;
        }

        private void OnEnable()
        {
            #region 從本地獲取玩家的物品
                var data = PlayerInventorySaver.LoadInventoryFromLocal();
                
                #region 載入快捷欄的物品
                    var hotbarSlots = _hotbarUISystem.GetHotbarSlots();

                    for (var i = 0; i < data.HotbarSlots.Count; i++)
                    {
                        var slotData = data.HotbarSlots[i];
                        var item = ItemDatabase.GetItem(slotData.ItemId);

                        if (item is null)
                        {
                            continue;
                        }
                                                
                        hotbarSlots[i].AddItem(item);
                    }
                #endregion
                                    
                #region 載入背包的物品
                    var backpackSlots = _backpackUISystem.GetBackpackSlots();

                    for (var i = 0; i < data.BackpackSlots.Count; i++)
                    {
                        var slotData = data.BackpackSlots[i];
                        var item = ItemDatabase.GetItem(slotData.ItemId);
                                                
                        if (item is null)
                        {
                            continue;
                        }
                                                
                        backpackSlots[i].AddItem(item);
                    }
                #endregion
            #endregion
        }

        private void OnDisable()
        {
            #region 儲存玩家的物品到本地
                var saveData = new InventorySaveData
                {
                    HotbarSlots = new List<InventorySaveDataEntry>(),
                    BackpackSlots = new List<InventorySaveDataEntry>()
                };
                    
                #region 快捷欄
                var hotbarSlots = _hotbarUISystem.GetHotbarSlots();
                        
                for (var i = 0; i < hotbarSlots.Count; i++)
                {
                    var item = hotbarSlots[i].Item;

                    saveData.HotbarSlots.Add(new InventorySaveDataEntry
                    {
                        SlotIndex = i,
                        ItemId = item?.ItemID ?? 0
                    });
                }
                #endregion
                    
                #region 背包
                var backpackSlots = _backpackUISystem.GetBackpackSlots();
                            
                for (var i = 0; i < backpackSlots.Count; i++)
                {
                    var item = backpackSlots[i].Item;

                    saveData.BackpackSlots.Add(new InventorySaveDataEntry
                    {
                        SlotIndex = i,
                        ItemId = item?.ItemID ?? 0
                    });
                }
                #endregion
                    
                PlayerInventorySaver.SaveInventoryToLocal(saveData);
            #endregion
        }

        #region 玩家控制
            public static void ClickRightButton()
            {
                _hotbarUISystem.UseSelectedHotbarSlotItem();
            }
        #endregion

        #region 背包儲物
            public static void SetHotbarUI(bool isEnabled)
            {
                _hotbarUISystem.SetHotbarUI(isEnabled);
            }

            public static void SetBackpackUI(bool isEnabled)
            {
                _backpackUISystem.SetBackpackUI(isEnabled);
            }

            public static void RequireBackpackUI()
            {
                _backpackUISystem.RequireBackpackUI();
            }

            public static void PerformHotbar(int hotbarIndex)
            {
                _hotbarUISystem.PerformHotbar(hotbarIndex);
            }
            
            public static bool AddItem(IItem itemData)
            {
                var result = _hotbarUISystem.AddItem(itemData);

                if (!result)
                {
                    result = _backpackUISystem.AddItem(itemData);
                }

                return result;
            }
            
            public static bool RemoveItem(ItemSO itemData)
            {
                return _hotbarUISystem.RemoveItem(itemData);
            }

            #endregion
        
        #region 物品拖曳
            public static void RequireItemDragUI(bool isDragging, IItem item)
            {
                _itemDragUISystem.RequiresUI(isDragging, item);
            }
        #endregion
    }
}