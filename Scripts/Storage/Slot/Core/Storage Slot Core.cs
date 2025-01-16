using Item.Core;
using Storage.Data.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Storage.Slot.Core
{
    public abstract class StorageSlotCore : MonoBehaviour
    {
        [field: HideInInspector]
        public StorageSlotData StorageSlotData { get; private set; }
        
        [field: Tooltip("儲存格的物品顯示圖片"), SerializeField]
        private Image itemImage;

        [field: Tooltip("儲存格的物品數量文字"), SerializeField]
        private TextMeshProUGUI itemAmountTMP;

        /// <summary>
        /// 更新儲存格
        /// </summary>
        public void Refresh()
        {
            if (StorageSlotData.item is null)
            {
                itemImage.gameObject.SetActive(false);
                itemAmountTMP.gameObject.SetActive(false);
            }
            else
            {
                itemImage.gameObject.SetActive(true);
                itemImage.sprite = StorageSlotData.item.Sprite;
                
                itemAmountTMP.gameObject.SetActive(StorageSlotData.item.Stack == ItemCore.StackType.Yes);
                
                if (itemAmountTMP.gameObject.activeSelf)
                    itemAmountTMP.text = StorageSlotData.itemAmount.ToString();
            }
        }

        /// <summary>
        /// 賦予新的 " StorageSlotData " 並更新儲存格
        /// </summary>
        /// <param name="other"></param>
        public void Refresh(StorageSlotData other)
        {
            if (StorageSlotData.Equals(other))
                return;
            
            StorageSlotData = other;
            Refresh();
        }
    }
}
