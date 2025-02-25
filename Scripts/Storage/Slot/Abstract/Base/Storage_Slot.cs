using Storage.Root.Backend.Struct;
using UnityEngine;

namespace Storage.Slot.Abstract.Base
{
    public abstract class StorageSlot : MonoBehaviour
    {
        /// <summary>
        /// 解鎖儲物格。
        /// </summary>
        public virtual void UnLock()
        {
        }

        /// <summary>
        /// 刷新儲物格的顯示。
        /// </summary>
        /// <param name="newData"> 儲物格的資料。 </param>
        public virtual void Refresh(StorageSlotData newData)
        {
        }
    }
}