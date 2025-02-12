using Item.Core;
using Storage.Data.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Storage.Slot.Core
{
    public abstract class StorageSlotCore : MonoBehaviour
    {
        public StorageSlotData StorageSlotData { get; private set; }
        
        [field: Tooltip("儲存格的物品顯示圖片"), SerializeField]
        private Image itemImage;

        [field: Tooltip("儲存格的物品數量文字"), SerializeField]
        private TextMeshProUGUI itemAmountTMP;

        /// <summary>
        /// 增加一個物品到儲存格
        /// </summary>
        /// <param name="other"></param>
        public void AddOneItem(StorageSlotData other)
        {
            if (StorageSlotData.item is null)
            {
                StorageSlotData = new StorageSlotData
                {
                    @lock = other.@lock,
                    item = other.item,
                    itemAmount = 1
                };
            }
            else
            {
                StorageSlotData = new StorageSlotData
                {
                    @lock = StorageSlotData.@lock,
                    item = StorageSlotData.item,
                    itemAmount = StorageSlotData.itemAmount + 1
                };
            }

            Refresh();
        }

        public void RemoveOneItem()
        {
            if (StorageSlotData.item is null)
                return;

            if (StorageSlotData.itemAmount - 1 <= 0)
                StorageSlotData = new StorageSlotData();
            else
            {
                StorageSlotData = new StorageSlotData
                {
                    @lock = StorageSlotData.@lock,
                    item = StorageSlotData.item,
                    itemAmount = StorageSlotData.itemAmount - 1
                };
            }
            
            Refresh();
        }

        /// <summary>
        /// 賦予新的 " StorageSlotData " 並更新儲存格
        /// </summary>
        /// <param name="other"></param>
        public void SetItem(StorageSlotData other)
        {
            StorageSlotData = other;
            Refresh();
        }
        
        /// <summary>
        /// 更新儲存格
        /// </summary>
        private void Refresh()
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
                
                itemAmountTMP.gameObject.SetActive(StorageSlotData.item.Stackable == ItemCore.StackType.Yes);
                
                if (itemAmountTMP.gameObject.activeSelf)
                    itemAmountTMP.text = StorageSlotData.itemAmount.ToString();
            }
        }

        /// <summary>
        /// 重置格子所有資訊
        /// </summary>
        public void Reset()
        {
            StorageSlotData = new StorageSlotData();
            itemImage.gameObject.SetActive(false);
            itemAmountTMP.gameObject.SetActive(false);
        }

        public bool CheckSlotStackable(StorageSlotData other)
        {
            if (StorageSlotData.item is null)
                return false;
            
            if (StorageSlotData.item.Stackable == ItemCore.StackType.No)
                return false;
            
            return true;
        }
    }
}
