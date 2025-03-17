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
        
        /// <summary>
        /// 用來刷新儲物格的介面的方法。
        /// </summary>
        /// <param name="newSlotData"> 新傳入的儲物格資料。 </param>
        public override void Refresh(StorageSlotData newSlotData)
        {
            // 如果新的儲物格資訊是鎖起來的，就直接退出更新 UI。
            if (newSlotData.isLocked)
                return;

            for (var i = 0; i < StorageUI.StorageSlotList.Count; i++)
            {
                // 如果 StorageUI 裡第 i 個儲物格的遊戲物件，與此儲物格一樣，就進入判斷。
                if (!StorageUI.StorageSlotList[i].Equals(this))
                    continue;
                
                // 即時更改儲物介面裡指定的儲物格的資料。
                StorageUI.StorageData.SlotDataList[i] = newSlotData;
                    
                // 如果儲物格資料庫的物品資料是空的，就把 itemImage 跟 itemQuantityTMP 給清空後隱藏。
                if (newSlotData.itemData is null)
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
                    itemImage.sprite = newSlotData.itemData.Sprite;
                        
                    itemQuantityTMP.gameObject.SetActive(true);
                    itemQuantityTMP.text = newSlotData.itemQuantity.ToString();
                }
            }
        }
    }
}
