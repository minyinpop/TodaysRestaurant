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
            // 用剩餘數量來判斷物品是否需要繼續添加。
        }
    }
}