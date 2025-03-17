using System.Collections.Generic;
using Storage.Slot;
using UnityEngine;

namespace Storage
{
    /// <summary>
    /// 用來控制儲物介面的類。
    /// </summary>
    public class StorageUI : MonoBehaviour
    {
        [field: Header("資料庫"), Tooltip("用來當作儲物介面的資料庫。"), SerializeField]
        public StorageData StorageData { get; private set; }
        
        [field: Header("儲物格介面"), Tooltip("- 儲物介面的所有儲物格的陣列。\n- 用來刷新所有物品。"), SerializeField]
        public List<StorageSlotUI> StorageSlotList { get; private set; }
        
        /// <summary>
        /// 用於更新整個儲物介面。
        /// </summary>
        public void Refresh()
        {
            // 以儲物格的介面當作數量參考，來判斷要抓取多少的儲物格資料。
            for (var i = 0; i < StorageSlotList.Count; i++)
            {
                StorageSlotList[i].Refresh(StorageData.SlotDataList[i]);
            }
        }
    }
}
