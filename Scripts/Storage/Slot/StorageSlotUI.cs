using UnityEngine;

namespace Storage.Slot
{
    /// <summary>
    /// 用來整合所有有關於到儲物格介面的抽象類。
    /// </summary>
    public abstract class StorageSlotUI : MonoBehaviour
    {
        /// <summary>
        /// 用來刷新儲物格的介面的方法。
        /// </summary>
        /// <param name="newSlotData"> 新傳入的儲物格資料。 </param>
        public abstract void Refresh(StorageSlotData newSlotData);

        /// <summary>
        /// 用來獲取儲物格裡面的數據。
        /// </summary>
        /// <returns> 返還自己的儲物格資訊。 </returns>
        public virtual StorageSlotData SlotData()
        {
            return new StorageSlotData();
        }
    }
}
