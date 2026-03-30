using System;
using System.Collections.Generic;
using Common.Item.Data;
using PlayFab;
using PlayFab.ClientModels;
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
                    Debug.Log($"{name} > {GetType().Name} > {nameof(hotbarUISystem)} cannot be null.");
                    Destroy(gameObject);
                    return;
                }
                
                if (backpackUISystem is null)
                {
                    Debug.Log($"{name} > {GetType().Name} > {nameof(backpackUISystem)} cannot be null.");
                    Destroy(gameObject);
                    return;
                }
                
                if (itemDragUISystem is null)
                {
                    Debug.Log($"{name} > {GetType().Name} > {nameof(backpackUISystem)} cannot be null.");
                    Destroy(gameObject);
                    return;
                }
            #endregion
            
            _hotbarUISystem = hotbarUISystem;
            _backpackUISystem = backpackUISystem;
            _itemDragUISystem = itemDragUISystem;
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

            public static void SaveInventory(Action onComplete, Action onFail)
            {
                var saveData = new InventorySaveData
                {
                    hotbarSlots = new List<SlotSaveData>(),
                    backpackSlots = new List<SlotSaveData>()
                };
                
                #region 快捷欄
                    var hotbarSlots = _hotbarUISystem.GetHotbarSlots();
                    
                    for (var i = 0; i < hotbarSlots.Count; i++)
                    {
                        var item = hotbarSlots[i].Item;

                        saveData.hotbarSlots.Add(new SlotSaveData
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

                        saveData.backpackSlots.Add(new SlotSaveData
                        {
                            SlotIndex = i,
                            ItemId = item?.ItemID ?? 0
                        });
                    }
                #endregion
                
                var json = JsonUtility.ToJson(saveData);

                var request = new UpdateUserDataRequest
                {
                    Data = new Dictionary<string, string>
                    {
                        { "Inventory", json }
                    }
                };
                
                PlayFabClientAPI.UpdateUserData(
                    request: request,
                    resultCallback: result =>
                    {
                        onComplete.Invoke();
                    },
                    errorCallback: error =>
                    {
                        onFail.Invoke();
                    });
            }

            public static void LoadInventory(Action onComplete, Action onFail)
            {
                // TODO 讀取物品
            }
        #endregion
        
        #region 物品拖曳
            public static void RequireItemDragUI(bool isDragging, IItem item)
            {
                _itemDragUISystem.RequiresUI(isDragging, item);
            }
        #endregion
    }
    
    [Serializable]
    public class InventorySaveData
    {
        public List<SlotSaveData> hotbarSlots;
        public List<SlotSaveData> backpackSlots;
    }
    
    [Serializable]
    public class SlotSaveData
    {
        public int SlotIndex;
        public int ItemId;
    }
}