using System;
using Item.Core;
using UnityEngine;

namespace Storage.Data.Core
{
    [Serializable]
    public struct StorageSlotData : IEquatable<ItemCore>
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
        /// 比較傳入的物品是否與本地的物品相符
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ItemCore other) => other == item;
    }
}
