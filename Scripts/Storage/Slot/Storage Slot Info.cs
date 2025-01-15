using System;
using Item;
using UnityEngine;

namespace Storage.Slot
{
    [Serializable]
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
        /// 重置格子
        /// </summary>
        /// <returns></returns>
        public static StorageSlotInfo Reset(StorageSlotState state)
        {
            return new StorageSlotInfo
            {
                state = state,
                item = null,
                itemAmount = 0
            };
        }

        /// <summary>
        /// 比較外部資料是否一樣
        /// </summary>
        /// <param name="otherInfo"></param>
        /// <returns></returns>
        public bool Equals(StorageSlotInfo otherInfo)
        {
            return state == otherInfo.state &&
                   Equals(item, otherInfo.item) &&
                   itemAmount == otherInfo.itemAmount;
        }

        public override bool Equals(object obj)
        {
            return obj is StorageSlotInfo other &&
                   Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int)state, item, itemAmount);
        }
    }
}
