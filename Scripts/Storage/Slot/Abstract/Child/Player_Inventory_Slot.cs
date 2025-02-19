using Storage.Root.Backend.Struct;
using Storage.Slot.Abstract.Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Storage.Slot.Abstract.Child
{
    public class PlayerInventorySlot : StorageSlot
    {
        [Tooltip("顯示物品圖案的圖片組件。")]
        [SerializeField]
        private Image itemImage;

        [Tooltip("顯示物品數量的文字組件。")]
        [SerializeField]
        private TextMeshProUGUI itemQuantityTMP;

        [Tooltip("儲存格的資料。")]
        [SerializeField]
        private StorageSlotData data;

        private void Awake()
        {
            // 確認 itemImage 不是空的。
            if (itemImage is null)
            {
                Debug.LogError($"{gameObject.name} 的 itemImage 是空的 !");
                return;
            }

            // 確認 itemQuantityTMP 不是空的。
            if (itemQuantityTMP is null)
                Debug.LogError($"{gameObject.name} 的 itemQuantityTMP 是空的 !");
        }
        
        public override void Refresh(StorageSlotData newData)
        {
            // 更新儲物格暫存資料。
            data = newData;
            
            // 如果傳入的物品是空的。
            if (newData.Item is null)
            {
                itemImage.gameObject.SetActive(false);
                itemQuantityTMP.gameObject.SetActive(false);
                return;
            }

            // 設定儲物格的物品圖片。
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = newData.Item.Sprite;
            
            // 設定儲物格的物品數量文字。
            itemQuantityTMP.gameObject.SetActive(true);
            itemQuantityTMP.text = newData.Quantity.ToString();
        }
    }
}