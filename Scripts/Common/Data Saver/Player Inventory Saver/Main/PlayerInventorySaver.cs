using System.Collections.Generic;
using System.IO;
using Common.Data_Saver.Player_Inventory_Saver.Child;
using Common.Item.Data;
using UnityEngine;

namespace Common.Data_Saver.Player_Inventory_Saver.Main
{
    public static class PlayerInventorySaver
    {
        private const string _keyword = "Inventory";

        private static string _folderPath;
        private static string _filePath;

        private const int _hotbarSlotCount = 10;
        private const int _backpackSlotCount = 50;
            
        public static void SaveInventoryToLocal(InventorySaveData saveData)
        {
            CheckPath();
            CheckFolder();
            CheckFile();

            var json = JsonUtility.ToJson(saveData);
            File.WriteAllText(_filePath, json);
        }

        public static void LoadInventoryFromLocal(out InventorySaveData saveData)
        {
            CheckPath();
            CheckFolder();
            CheckFile();
            
            var json = File.ReadAllText(_filePath);
            saveData = JsonUtility.FromJson<InventorySaveData>(json);
        }

        public static void AddItemToInventory(IReadOnlyList<IItem> itemsData)
        {
            if (itemsData is null)
            {
                Debug.Log($"{nameof(itemsData)} 不能傳入空值。");
                return;
            }
            
            CheckPath();
            CheckFolder();
            CheckFile();
            
            LoadInventoryFromLocal(out var saveData);

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

            SaveInventoryToLocal(saveData);
        }
        
        private static void CheckPath()
        {
            if (string.IsNullOrEmpty(_folderPath))
            {
                _folderPath = $"{Application.persistentDataPath}/SaveData";
            }

            if (string.IsNullOrEmpty(_filePath))
            {
                _filePath = $"{_folderPath}/{_keyword}.json";
            }
        }

        private static void CheckFolder()
        {
            if (Directory.Exists(_folderPath))
            {
                return;
            }

            Directory.CreateDirectory(_folderPath);
            Debug.Log($"找不到 SaveData，已自動創建，位置：{_folderPath}。");
        }

        private static void CheckFile()
        {
            if (File.Exists(_filePath))
            {
                return;
            }

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

            var saveData = new InventorySaveData
            {
                HotbarSlots = hotbarSlots,
                BackpackSlots = backpackSlots
            };

            var json = JsonUtility.ToJson(saveData);
            File.WriteAllText(_filePath, json);
            
            Debug.Log($"找不到 {_keyword}.json，已自動創建，位置：{_filePath}。");
        }
        
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