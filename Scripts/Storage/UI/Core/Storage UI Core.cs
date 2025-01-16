using System.Collections.Generic;
using Item.Core;
using Storage.Data.Core;
using Storage.Slot.Core;
using UnityEngine;

namespace Storage.UI.Core
{
    public abstract class StorageUICore : MonoBehaviour
    {
        protected readonly List<StorageSlotCore> StorageSlotCoreList = new();

        [field: Header("儲存格資料"), Tooltip("這個介面所使用的儲存格資料"), SerializeField]
        public StorageData StorageData { get; private set; }

        /// <summary>
        /// 添加單個物品
        /// </summary>
        /// <param name="item"></param>
        public abstract void AddItem(ItemCore item);
    }
}
