using System;
using System.Collections.Generic;
using System.IO;
using Common.Data_Saver.Player_Inventory_Saver.Child;
using Common.Item.Data;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace Common.Data_Saver.Player_Inventory_Saver.Main
{
    public static class PlayerInventorySaver
    {
        private const string _keyword = "Inventory";
        private const string _localPath = "/" + _keyword + ".json";

        private static bool _initialized;

        private const int _hotbarSlotCount = 10;
        private const int _backpackSlotCount = 50;
        
        #region 本地操作
            public static bool InitializeInventoryToLocal()
            {
                #region 檢查必要條件
                    if (_initialized)
                    {
                        Debug.Log("玩家的物品資料庫已經初始化。");
                        return false;
                    }
                #endregion

                _initialized = true;
                
                var filePath = Application.persistentDataPath + _localPath;

                #region 檢查本地端是否已經有玩家物品資料了
                    if (File.Exists(filePath))
                    {
                        Debug.Log("本地端已經有玩家物品資料了。");
                        return false;
                    }
                #endregion
                
                SpawnNewInventorySaveData(out var saveData);
                
                var json = JsonUtility.ToJson(saveData);
                File.WriteAllText(filePath, json);

                Debug.Log("已在本地端創建新的玩家物品資料。");
                return true;
            }
            
            public static bool SaveInventoryToLocal(InventorySaveData saveData)
            {
                #region 必要條件檢查
                    if (!_initialized)
                    {
                        Debug.Log("無法添加物品到本地的玩家資料，因為尚未初始化。");
                        return false;
                    }
                #endregion
                
                Debug.Log("已成功把物品給存到本地的資料庫了。");
                
                var filePath = Application.persistentDataPath + _localPath;
                var json = JsonUtility.ToJson(saveData);
                
                File.WriteAllText(filePath, json);
                return true;
                
            }

            public static bool LoadInventoryFromLocal(out InventorySaveData saveData)
            {
                #region 必要條件檢查
                    if (!_initialized)
                    {
                        Debug.Log("無法讀取物品到本地的玩家資料，因為尚未初始化。");
                        saveData = null;
                        return false;
                    }
                #endregion
                
                var filePath = Application.persistentDataPath + _localPath;

                if (File.Exists(filePath))
                {
                    var json = File.ReadAllText(filePath);
                    saveData = JsonUtility.FromJson<InventorySaveData>(json);
                    Debug.Log("已在本地端獲取到玩家物品資料。");
                }
                else
                {
                    SpawnNewInventorySaveData(out saveData);
                    Debug.Log("無法在本地端找尋到玩家的物品資料，已自動創建新的資料到本地端。");
                }

                return true;
            }

            public static void AddItemToInventory(IReadOnlyList<IItem> itemsData)
            {
                #region 必要條件檢查
                    if (itemsData is null)
                    {
                        Debug.Log($"{nameof(itemsData)} 不能為空的。");
                        return;
                    }

                    if (!_initialized)
                    {
                        Debug.Log("無法讀取物品到本地的玩家資料，因為尚未初始化。");
                        return;
                    }
                #endregion
                
                #region 從本地獲取玩家的物品資料
                    if (!LoadInventoryFromLocal(out var saveData))
                    {
                        Debug.Log("無法從本地獲取玩家的物品資料。");
                        return;
                    }
                #endregion

                #region 添加物品
                    foreach (var itemData in itemsData)
                    {
                        #region 在快捷欄裡搜尋有空位的格子
                            foreach (var saveDataEntry in saveData.HotbarSlots)
                            {
                                if (saveDataEntry.HaveItem)
                                {
                                    continue;
                                }
                                
                                saveDataEntry.HaveItem = true;
                                saveDataEntry.ItemId = itemData.ItemID;
                            }
                        #endregion
                        
                        #region 在背包裡搜尋有空位的格子
                            foreach (var saveDataEntry in saveData.BackpackSlots)
                            {
                                if (saveDataEntry.HaveItem)
                                {
                                    continue;
                                }
                                
                                saveDataEntry.HaveItem = true;
                                saveDataEntry.ItemId = itemData.ItemID;
                            }
                        #endregion
                    }
                #endregion

                #region 儲存到本地的玩家物品資料
                    SaveInventoryToLocal(saveData);
                #endregion
            }
        #endregion
        
        #region 工具 function
            private static void SpawnNewInventorySaveData(out InventorySaveData saveData)
            {
                List<InventorySaveDataEntry> hotbarSlots = new();
                List<InventorySaveDataEntry> backpackSlots = new();

                for (var i = 0; i < _hotbarSlotCount; i++)
                {
                    hotbarSlots.Add(new InventorySaveDataEntry
                    {
                        HaveItem = false,
                        ItemId = 0
                    });
                }
                            
                for (var i = 0; i < _backpackSlotCount; i++)
                {
                    backpackSlots.Add(new InventorySaveDataEntry
                    {
                        HaveItem = false,
                        ItemId = 0
                    });
                }

                saveData = new InventorySaveData
                {
                    HotbarSlots = hotbarSlots,
                    BackpackSlots = backpackSlots
                };
            }
        #endregion
        
        /*
        #region PlayFab 操作
            public static void SaveInventoryToPlayFab(InventorySaveData saveData)
            {
                #region 更新本地存檔
                    var path = Application.persistentDataPath + _localPath;
                    var json = JsonUtility.ToJson(saveData);
                    
                    File.WriteAllText(path, json);
                #endregion
                
                #region 上傳到 PlayFab
                    var request = new UpdateUserDataRequest
                    {
                        Data = new Dictionary<string, string>
                        {
                            { _keyword, json }
                        }
                    };
                        
                    PlayFabClientAPI.UpdateUserData(
                        request: request,
                        resultCallback: result =>
                        {
                            #region 開發提示
                                Debug.Log("成功把玩家的物品資料給上傳到 PlayFab。");
                            #endregion
                        },
                        errorCallback: error =>
                        {
                            #region 開發提示
                                Debug.Log("無法把玩家的物品資料給上傳到 PlayFab。");
                                Debug.Log(error.GenerateErrorReport());
                            #endregion
                        });
                #endregion
            }

            public static void LoadInventoryFromPlayFab(InventorySaveData saveData)
            {
                PlayFabClientAPI.GetUserData(
                    request: new GetUserDataRequest(),
                    resultCallback: result =>
                    {
                        #region 開發提示
                            Debug.Log("成功從 PlayFab 獲取到玩家的物品資料。");
                        #endregion
                        
                        #region 從 PlayFab 獲取玩家的物品
                            var json = result.Data[_keyword].Value;
                                saveData = JsonUtility.FromJson<InventorySaveData>(json);
                        #endregion
                        
                        #region 強制更新本地資料
                            SaveInventoryToLocal(saveData);
                        #endregion
                    },
                    errorCallback: error =>
                    {
                        #region 開發提示
                            Debug.Log("無法從 PlayFab 獲取到玩家的物品資料，改從本地獲取。");
                            Debug.Log(error.GenerateErrorReport());
                        #endregion
                        
                        #region 從本地獲取資料
                            LoadInventoryFromLocal(out saveData);
                        #endregion
                    });
            }
        #endregion
        */
    }
}