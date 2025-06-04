using System.Collections.Generic;
using Item.Base;
using Storage_Slot.Base;
using UnityEngine;

namespace Storage.Base
{
    [CreateAssetMenu(menuName = "Today's Restaurant/Storage/Storage", fileName = "New Data", order = 3)]
    internal class StorageBase : ScriptableObject
    {
        [field: Header("儲物格資料")]
        [field: SerializeField] internal List<StorageSlotData> SlotsData { get; private set; }

        /// <summary>
        /// 添加一個物品
        /// </summary>
        /// <param name="item"> 物品的資料 </param>
        /// <returns> 返回是否添加成功 </returns>
        internal bool AddItem(ItemBase item)
        {
            if (item is null) return false;
            foreach (var slotData in SlotsData)
            {
                
            }
        }

        /// <summary>
        /// 以數量來添加物品
        /// </summary>
        /// <param name="item"> 物品的資料 </param>
        /// <param name="quantity"> 物品的數量 </param>
        /// <returns> 返回是否添加成功 </returns>
        internal bool AddItem(ItemBase item, int quantity)
        {
            if (item is null) return false;
        }
    }
}