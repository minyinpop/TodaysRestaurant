using System.Collections.Generic;
using UnityEngine;

namespace Storage.Data.Core
{
    [CreateAssetMenu(menuName = "Minyinpop/New Config/Storage Data", fileName = "New Storage Data", order = 3)]
    public class StorageData : ScriptableObject
    {
        [field: Tooltip("儲存格的資訊陣列\n\n目標 UI 上有多少個儲存格\n這裡就創建多少位置")]
        public List<StorageSlotData> storageSlotDataList;
    }
}
