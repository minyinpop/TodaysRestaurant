using System;
using System.Collections.Generic;
using Common.Data_Saver.Player_Inventory_Saver.Child;
using Common.Data_Saver.Player_Inventory_Saver.Main;
using Common.Database;
using Common.Item.Data;
using UI_System.Player_UI_System.Child.Backpack_UI_System.System;
using UI_System.Player_UI_System.Child.Hotbar_UI_System.System;
using UI_System.Player_UI_System.Child.Item_Drag_UI_System.System;
using UnityEngine;

namespace UI_System.Player_UI_System.Main
{
    public sealed class PlayerUISystem : MonoBehaviour
    {
        [field: Header("自身組件")]
        [field: SerializeField] private HotbarUISystem hotbarUISystem;
                                public static HotbarUISystem HotbarUISystem;
        [field: SerializeField] private BackpackUISystem backpackUISystem;
                                public static BackpackUISystem BackpackUISystem;
        [field: SerializeField] private ItemDragUISystem itemDragUISystem;
                                public static ItemDragUISystem ItemDragUISystem;
        
        private void Awake()
        {
            #region 必要條件檢查
                if (hotbarUISystem is null)
                {
                    throw new InvalidOperationException($"{nameof(hotbarUISystem)} 沒有被掛載。");
                }
                
                if (backpackUISystem is null)
                {
                    throw new InvalidOperationException($"{nameof(backpackUISystem)} 沒有被掛載。");
                }
                
                if (itemDragUISystem is null)
                {
                    throw new InvalidOperationException($"{nameof(itemDragUISystem)} 沒有被掛載。");
                }
            #endregion
            
            HotbarUISystem = hotbarUISystem;
            BackpackUISystem = backpackUISystem;
            ItemDragUISystem = itemDragUISystem;
        }

        private void OnEnable()
        {
            #region 從本地獲取玩家的物品
                PlayerInventorySaver.LoadInventoryFromLocal(out var saveData);
                
                #region 載入快捷欄的物品
                    var hotbarSlots = HotbarUISystem.GetHotbarSlots();

                    for (var i = 0; i < saveData.HotbarSlots.Count; i++)
                    {
                        var slotData = saveData.HotbarSlots[i];
                        var item = ItemDatabase.GetItem(slotData.ItemId);

                        if (item is null)
                        {
                            continue;
                        }
                                                
                        hotbarSlots[i].AddItem(item);
                    }
                #endregion
                                    
                #region 載入背包的物品
                    var backpackSlots = BackpackUISystem.GetBackpackSlots();

                    for (var i = 0; i < saveData.BackpackSlots.Count; i++)
                    {
                        var slotData = saveData.BackpackSlots[i];
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
                    var hotbarSlots = HotbarUISystem.GetHotbarSlots();
                            
                    for (var i = 0; i < hotbarSlots.Count; i++)
                    {
                        var item = hotbarSlots[i].Item;

                        saveData.HotbarSlots.Add(new InventorySaveDataEntry
                        {
                            HaveItem = item is not null,
                            ItemId = item?.ItemID ?? 0
                        });
                    }
                #endregion
                    
                #region 背包
                    var backpackSlots = BackpackUISystem.GetBackpackSlots();
                                
                    for (var i = 0; i < backpackSlots.Count; i++)
                    {
                        var item = backpackSlots[i].Item;

                        saveData.BackpackSlots.Add(new InventorySaveDataEntry
                        {
                            HaveItem = item is not null,
                            ItemId = item?.ItemID ?? 0
                        });
                    }
                #endregion
                
                PlayerInventorySaver.SaveInventoryToLocal(saveData);
                Debug.Log("成功把玩家的物品給儲存到本地。");

                #endregion
        }

        #region 玩家控制
            public static void ClickRightButton()
            {
                HotbarUISystem.UseSelectedHotbarSlotItem();
            }
        #endregion

        #region 背包儲物
            public static void SetHotbarUI(bool isEnabled)
            {
                HotbarUISystem.SetHotbarUI(isEnabled);
            }

            public static void SetBackpackUI(bool isEnabled)
            {
                BackpackUISystem.SetBackpackUI(isEnabled);
            }

            public static void RequireBackpackUI()
            {
                BackpackUISystem.RequireBackpackUI();
            }

            public static void PerformHotbar(int hotbarIndex)
            {
                HotbarUISystem.PerformHotbar(hotbarIndex);
            }
            
            public static bool AddItem(IItem itemData)
            {
                var result = HotbarUISystem.AddItem(itemData);

                if (!result)
                {
                    result = BackpackUISystem.AddItem(itemData);
                }

                return result;
            }
            
            public static bool RemoveItem(ItemSO itemData)
            {
                return HotbarUISystem.RemoveItem(itemData);
            }

            #endregion
        
        #region 物品拖曳
            public static void RequireItemDragUI(bool isDragging, IItem item)
            {
                ItemDragUISystem.RequiresUI(isDragging, item);
            }
        #endregion
    }
}