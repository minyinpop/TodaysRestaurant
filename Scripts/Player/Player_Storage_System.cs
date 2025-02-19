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
            // 剩餘的數量。
            var remainingQuantity = newData.Quantity;

            // 遍歷所有的儲物格，並確認其儲物狀態。
            foreach (var dataList in storageDataList)
            {
                for (var i = 0; i < dataList.data.Count; i++)
                {
                    switch (dataList.AddItem(i, new StorageSlotData
                            {
                                Locked = false,
                                Item = newData.Item,
                                Quantity = remainingQuantity
                            }))
                    {
                        case Storage.Root.Backend.ScriptableObject.Storage.AddItemResult.Fail:
                        {
                            Debug.Log("Fail");
                            continue;
                        }
                        case Storage.Root.Backend.ScriptableObject.Storage.AddItemResult.Finish:
                        {
                            Debug.Log("Finish");
                            return;
                        }
                        case Storage.Root.Backend.ScriptableObject.Storage.AddItemResult.Remaining:
                        {
                            Debug.Log("Remaining");
                            continue;
                        }
                    }
                }
            }
        }
    }
}