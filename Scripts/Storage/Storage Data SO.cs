using System.Collections.Generic;
using Storage.Slot;
using UnityEngine;

namespace Storage
{
    [CreateAssetMenu(menuName = "Minyinpop/Storage Data", fileName = "New Storage Data", order = 2)]
    public class StorageDataSO : ScriptableObject
    {
        [field: Header("物品資料"), Tooltip("儲存格裡面的物品資料，數量表示可使用的格子的總數")]
        public List<StorageSlotInfo> storageSlotInfos;
    }
}
