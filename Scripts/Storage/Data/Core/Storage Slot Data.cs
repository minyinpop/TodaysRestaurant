using System;
using Item.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Storage.Data.Core
{
    [Serializable]
    public struct StorageSlotData : IEquatable<StorageSlotData>
    {
        [field: Tooltip("儲存格的上鎖狀態")]
        public LockState @lock;
        public enum LockState
        {
            Yes,
            No
        }

        [field: Tooltip("物品的資訊")]
        public ItemCore item;

        [field: Tooltip("物品的數量")]
        public int itemAmount;

        /// <summary>
        /// 比較 " other " 裡的資訊是否與本身的資訊相符
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(StorageSlotData other)
        {
            return @lock == other.@lock && Equals(item, other.item) && itemAmount == other.itemAmount;
        }

        public override bool Equals(object obj)
        {
            return obj is StorageSlotData other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int)@lock, item, itemAmount);
        }
    }
}
