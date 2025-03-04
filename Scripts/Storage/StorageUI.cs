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
        [Header("資料庫"), Tooltip("用來當作儲物介面的資料庫。"), SerializeField]
        private StorageData storageData;
        
        [Header("儲物格介面"), Tooltip("- 儲物介面的所有儲物格的陣列。\n- 用來刷新所有物品。"), SerializeField]
        private List<StorageSlotUI> storageSlotList;

        public void Refresh()
        {
            foreach (var storageSlot in storageSlotList)
            {
                // TODO: storageSlot.Refresh();
            }
        }
    }
}
