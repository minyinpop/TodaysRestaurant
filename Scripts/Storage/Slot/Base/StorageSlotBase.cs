using Item.Base;
using UnityEngine;

namespace Storage_Slot.Base
{
    internal abstract class StorageSlotBase : MonoBehaviour
    {
        /// <summary>
        /// 添加 1 個物品，並且會返回是否添加成功
        /// </summary>
        /// <param name="item"> 物品的資料 </param>
        /// <returns> 添加是否成功 </returns>
        internal virtual bool AddItem(ItemBase item) => false;
    }
}