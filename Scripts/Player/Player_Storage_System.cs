using System.Collections.Generic;
using Storage.Root.Backend.Struct;
using UnityEngine;

namespace Player
{
    public class PlayerStorageSystem : MonoBehaviour
    {
        [Tooltip("儲物介面的遊戲物件。")]
        [SerializeField]
        private List<Storage.Root.Frontend.Storage> storageUIList;
        
        [Tooltip("儲物介面的資料庫。")]
        [SerializeField]
        private List<Storage.Root.Backend.ScriptableObject.Storage> storageDataList;

        public void AddItem(StorageSlotData newData)
        {
            for (var i = 0; i < storageDataList.Count; i++)
            {
                var remainingQuantity = storageDataList[i].AddItem(newData);
                
                // 表示物品添加完畢。
                if (remainingQuantity == 0)
                {
                    Debug.Log("物品添加完畢。");
                    return;
                }
                // 表示第 i 個資料庫的儲物格皆跑過一次，但無法添加完該物品。
                if (remainingQuantity > 0)
                {
                    Debug.Log("物品添加不完，切換成下一個資料庫做添加。");
                }
                // 表示某段程式碼發生錯誤。
                if (remainingQuantity < 0)
                {
                    Debug.LogWarning("物品數量不能為負數，此情況可能發生錯誤 !");
                }
            }
            // TODO: 把 UI 更新下。
        }
    }
}