using UnityEngine;

namespace Storage.Slot
{
    /// <summary>
    /// 用來整合所有有關於到儲物格介面的抽象類。
    /// </summary>
    public abstract class StorageSlotUI : MonoBehaviour
    {
        [field: Header("儲物介面"), Tooltip("此儲物格所在的儲物介面的遊戲物件。"), SerializeField]
        public StorageUI StorageUI { get; protected set; }
        
        /// <summary>
        /// 用來刷新儲物格的介面的方法。
        /// </summary>
        /// <param name="newSlotData"> 新傳入的儲物格資料。 </param>
        public abstract void Refresh(StorageSlotData newSlotData);

        /// <summary>
        /// 用來清空儲物格的介面的方法。
        /// </summary>
        public void Clear()
        {
            Refresh(new StorageSlotData());
        }

        /// <summary>
        /// 用來獲取儲物格裡面的數據。
        /// </summary>
        /// <returns> 返還自己的儲物格資訊。 </returns>
        public StorageSlotData SlotData()
        {
            for (var i = 0; i < StorageUI.StorageSlotList.Count; i++)
            {
                if (StorageUI.StorageSlotList[i].Equals(this))
                    return StorageUI.StorageData.SlotDataList[i];
            }

            Debug.LogError($"{StorageUI.name} 裡沒有對應的儲物格，是否忘記添加 ?");
            return new StorageSlotData();
        }
    }
}
