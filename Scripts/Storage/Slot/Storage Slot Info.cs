using System;
using Item;
using UnityEngine;

namespace Storage.Slot
{
    [System.Serializable]
    public struct StorageSlotInfo : IEquatable<StorageSlotInfo>
    {
        [field: Header("資訊"), Tooltip("儲存格是否解鎖 ?")]
        public StorageSlotState state;
        public enum StorageSlotState
        {
            Locked,
            Unlocked
        }
        
        [field: Tooltip("物品資料")]
        public ItemCore item;

        [field: Tooltip("物品數量")]
        public int itemAmount;

        /// <summary>
        /// 檢查外部傳入的資訊是否跟本地一樣邏輯
        /// </summary>
        /// <param name="otherInfo"></param>
        /// <returns></returns>
        public bool Equals(StorageSlotInfo otherInfo)
        {
            return state == otherInfo.state && Equals(item, otherInfo.item) && itemAmount == otherInfo.itemAmount;
        }
    }
}
