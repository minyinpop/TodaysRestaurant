using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Storage.Slot.Category
{
    public class PlayerItemDragPreviewSlot : StorageSlotUI
    {
        [Header("物品顯示組件"), Tooltip("用來顯示物品圖片的圖片組件。"), SerializeField]
        private Image itemImage;
        
        [Tooltip("用來顯示物品數量的文字組件。"), SerializeField]
        private TextMeshProUGUI itemQuantityTMP;
        
        // 儲物格的資料庫暫存。
        private StorageSlotData _slotData;
        
        /// <summary>
        /// 用來刷新儲物格的介面的方法。
        /// </summary>
        /// <param name="newSlotData"> 新傳入的儲物格資料。 </param>
        public override void Refresh(StorageSlotData newSlotData)
        {
            _slotData = newSlotData;
            
            // 如果圖片組件存在的話，就顯示物品的圖片。
            if (itemImage is not null)
                itemImage.sprite = _slotData.itemData.Sprite;

            // 如果文字組件存在的話，就顯示物品數量的文字。
            if (itemQuantityTMP is not null)
                itemQuantityTMP.text = _slotData.itemQuantity.ToString();
        }
    }
}
