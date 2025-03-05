using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Storage.Slot.Category
{
    /// <summary>
    /// 用來控制玩家快捷攔裡儲物格的類，繼承 StorageSlotUI 抽象類。
    /// </summary>
    public class PlayerInventoryStorageSlot : StorageSlotUI
    {
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
            // 如果新的儲物格資訊是鎖起來的，就直接退出更新 UI。
            if (newSlotData.isLocked)
                return;
            
            slotData = newSlotData;

            // 如果儲物格資料庫的物品資料是空的，就把 itemImage 跟 itemQuantityTMP 給清空後隱藏。
            if (slotData.itemData is null)
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
                itemImage.sprite = slotData.itemData.Sprite;
                
                itemQuantityTMP.gameObject.SetActive(true);
                itemQuantityTMP.text = slotData.itemQuantity.ToString();
            }
        }
    }
}
