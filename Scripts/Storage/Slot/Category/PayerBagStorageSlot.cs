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
        
        [Header("物品顯示組件"), Tooltip("用來顯示物品圖片的圖片組件。"), SerializeField]
        private Image itemImage;
        
        [Tooltip("用來顯示物品數量的文字組件。"), SerializeField]
        private TextMeshProUGUI itemQuantityTMP;
        
        [Header("資料庫"), Tooltip("該儲物格的物品資料的數據暫存。"), SerializeField]
        private StorageSlotData slotData;
        
        /// <summary>
        /// 用來刷新儲物格的介面的
        /// </summary>
        /// <param name="newSlotData"></param>
        public override void Refresh(StorageSlotData newSlotData)
        {
            // 如果該儲物格是鎖起來的，就取消刷新該儲物格。
            if (slotData.isLocked)
            {
                if (newSlotData.isLocked)
                    return;

                slotData = newSlotData;
                
                // TODO: 依照 newSlotData 來更新儲物格的介面。
            }
        }
    }
}
