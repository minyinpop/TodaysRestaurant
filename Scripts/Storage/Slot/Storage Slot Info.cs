using Item;
using UnityEngine;

namespace Storage.Slot
{
    [System.Serializable]
    public struct StorageSlotInfo
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
    }
}
