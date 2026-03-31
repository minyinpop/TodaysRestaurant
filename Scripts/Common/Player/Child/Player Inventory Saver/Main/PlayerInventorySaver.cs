using System.Collections.Generic;
using System.IO;
using Common.Player.Child.Player_Inventory_Saver.Child;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace Common.Player.Child.Player_Inventory_Saver.Main
{
    public static class PlayerInventorySaver
    {
        private const string Keyword = "Inventory";
        
        private const string LocalPath = "/Inventory.json";
        
        #region 本地操作
            public static void SaveInventoryToLocal(InventorySaveData saveData)
            {
                var path = Application.persistentDataPath + LocalPath;
                var json = JsonUtility.ToJson(saveData);
                
                File.WriteAllText(path, json);
            }

            public static InventorySaveData LoadInventoryFromLocal()
            {
                var path = Application.persistentDataPath + LocalPath;
                var json = File.ReadAllText(path);

                return JsonUtility.FromJson<InventorySaveData>(json);
            }
        #endregion
        
        #region PlayFab 操作
            public static void SaveInventoryToPlayFab(InventorySaveData saveData)
            {
                #region 更新本地存檔
                    var path = Application.persistentDataPath + LocalPath;
                    var json = JsonUtility.ToJson(saveData);
                    
                    File.WriteAllText(path, json);
                #endregion
                
                #region 上傳到 PlayFab
                    var request = new UpdateUserDataRequest
                    {
                        Data = new Dictionary<string, string>
                        {
                            { Keyword, json }
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

            public static InventorySaveData LoadInventoryFromPlayFab()
            {
                var data = new InventorySaveData();
                
                PlayFabClientAPI.GetUserData(
                    request: new GetUserDataRequest(),
                    resultCallback: result =>
                    {
                        #region 開發提示
                            Debug.Log("成功從 PlayFab 獲取到玩家的物品資料。");
                        #endregion
                        
                        #region 從 PlayFab 獲取玩家的物品
                            var json = result.Data[Keyword].Value;
                                data = JsonUtility.FromJson<InventorySaveData>(json);
                        #endregion
                        
                        #region 強制更新本地資料
                            SaveInventoryToLocal(data);
                        #endregion
                    },
                    errorCallback: error =>
                    {
                        #region 開發提示
                            Debug.Log("無法從 PlayFab 獲取到玩家的物品資料，改從本地獲取。");
                            Debug.Log(error.GenerateErrorReport());
                        #endregion
                        
                        #region 從本地獲取資料
                            data = LoadInventoryFromLocal();
                        #endregion
                    });
                return data;
            }
        #endregion
    }
}