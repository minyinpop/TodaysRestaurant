using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Storage.Slot.Category
{
    /// <summary>
    /// 用來控制玩家背包裡儲物格的類，繼承 StorageSlotUI 抽象類。
    /// </summary>
    public class PayerBagStorageSlot : StorageSlotUI
    {
        [Header("圖片"), Tooltip("儲物格解鎖後的圖片，用於 UI。"), SerializeField]
        private Sprite unlockedSprite;

        [Header("組件"), Tooltip("用來顯示儲物格圖片的圖片組件。"), SerializeField]
        private Image slotImage;
            
        [Tooltip("用來顯示物品圖片的圖片組件。"), SerializeField]
        private Image itemImage;
        
        [Tooltip("用來顯示物品數量的文字組件。"), SerializeField]
        private TextMeshProUGUI itemQuantityTMP;
        
        [Header("資料庫"), Tooltip("該儲物格的物品資料的數據暫存。")]
        private StorageSlotData _slotData;
        
        /// <summary>
        /// 用來刷新儲物格的介面的方法。
        /// </summary>
        /// <param name="newSlotData"> 新傳入的儲物格資料。 </param>
        public override void Refresh(StorageSlotData newSlotData)
        {
            _slotData = newSlotData;
            
            // 如果儲物格資訊是鎖起來的。
            if (!_slotData.isLocked)
            {
                // 如果傳入進來的儲物格資訊是解鎖的，就更換儲物格的圖片。
                slotImage.sprite = unlockedSprite;
            }

            // 如果儲物格資料庫的物品資料是空的，就把 itemImage 跟 itemQuantityTMP 給清空後隱藏。
            if (_slotData.itemData is null)
            {
                itemImage.sprite = null;
                itemImage.gameObject.SetActive(false);

                itemQuantityTMP.text = "";
                itemQuantityTMP.gameObject.SetActive(false);
            }
            // 如果儲物格資料庫的物品資料是有東西的，就把 itemImage 跟 itemQuantityTMP 給開啟並附值。
            else
            {
                itemImage.gameObject.SetActive(true);
                itemImage.sprite = _slotData.itemData.Sprite;
                
                itemQuantityTMP.gameObject.SetActive(true);
                itemQuantityTMP.text = _slotData.itemQuantity.ToString();
            }
        }
        
        /// <summary>
        /// 用來清空儲物格的介面的方法。
        /// </summary>
        public override void Clear()
        {
            Refresh(new StorageSlotData());
        }
        
        /// <summary>
        /// 用來獲取儲物格裡面的數據。
        /// </summary>
        /// <returns> 返還自己的儲物格資訊。 </returns>
        public override StorageSlotData SlotData()
        {
            return _slotData;
        }
    }
}
