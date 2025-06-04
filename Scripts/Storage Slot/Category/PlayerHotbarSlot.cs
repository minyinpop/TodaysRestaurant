using Item.Base;
using Storage_Slot.Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Storage_Slot.Category
{
    internal class PlayerHotbarSlot : StorageSlotBase
    {
        [field: SerializeField] private Image ItemImage { get; set; }
        [field: SerializeField] private TextMeshProUGUI ItemQuantityTMP { get; set; }
        private StorageSlotData SlotData { get; set; }

        /// <summary>
        /// 添加 1 個物品，並且會返回是否添加成功
        /// </summary>
        /// <param name="item"> 物品的資料 </param>
        /// <returns> 添加是否成功 </returns>
        internal override bool AddItem(ItemBase item) => item is not null && SlotData.AddItem(item);
        /// <summary>
        /// 添加數個物品，並且會返回是否添加成功
        /// </summary>
        /// <param name="item"> 物品的資料 </param>
        /// <param name="quantity"> 物品的數量 </param>
        /// /// <returns> 添加是否成功 </returns>
        internal override bool AddItem(ItemBase item, int quantity)
        {
            // TODO
            return false;
        }
    }
}