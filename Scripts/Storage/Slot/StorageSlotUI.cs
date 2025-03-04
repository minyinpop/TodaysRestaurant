using UnityEngine;

namespace Storage.Slot
{
    /// <summary>
    /// 用來整合所有有關於到儲物格介面的抽象類。
    /// </summary>
    public abstract class StorageSlotUI : MonoBehaviour
    {
        /// <summary>
        /// 用來刷新儲物格的介面的
        /// </summary>
        /// <param name="newSlotData"></param>
        public abstract void Refresh(StorageSlotData newSlotData);
    }
}
