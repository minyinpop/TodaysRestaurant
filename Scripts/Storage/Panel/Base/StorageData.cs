using System.Collections.Generic;
using Item.Base;
using Storage_Slot.Base;
using UnityEngine;

namespace Storage.Base
{
    [CreateAssetMenu(menuName = "Today's Restaurant/Storage/Storage", fileName = "New Data", order = 3)]
    internal class StorageData : ScriptableObject
    {
        [field: Header("儲物格資料")]
        [field: SerializeField] internal List<StorageSlotData> SlotsData { get; private set; }

        /// <summary>
        /// 添加 1 個物品
        /// </summary>
        /// <param name="item"> 物品的資料 </param>
        /// <returns> 返回添加是否成功 </returns>
        internal bool AddItem(ItemBase item)
        {
            if (item is null) return false;
            foreach (var slotData in SlotsData)
            {
                if (slotData.AddItem(item)) return true;
                continue;
            }
            // TODO 沒有可用的儲存格可以添加物品了
            return false;
        }
    }
}