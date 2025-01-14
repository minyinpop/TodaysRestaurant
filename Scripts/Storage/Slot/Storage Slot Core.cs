using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Storage.Slot
{
    public abstract class StorageSlotCore : MonoBehaviour
    {
        // 儲存格資訊
        protected StorageSlotInfo StorageSlotInfo;
        
        [field: Header("組件"), Tooltip("物品的圖片組件"), SerializeField]
        protected Image itemImage;

        [field: Tooltip("數量的文字組件"), SerializeField]
        protected TextMeshProUGUI itemAmountTMP;

        /// <summary>
        /// 解鎖儲存格邏輯
        /// </summary>
        /// <param name="otherInfo"></param>
        public virtual void Unlock(StorageSlotInfo otherInfo) {}

        /// <summary>
        /// 更新格子邏輯
        /// </summary>
        /// <param name="otherInfo"></param>>
        public virtual void Refresh(StorageSlotInfo otherInfo)
        {
            if (StorageSlotInfo.state is StorageSlotInfo.StorageSlotState.Locked)
                return;

            if (StorageSlotInfo.Equals(otherInfo))
                return;
            
            StorageSlotInfo = otherInfo;

            if (StorageSlotInfo.item is null)
            {
                if (itemImage is not null)
                    itemImage.gameObject.SetActive(false);
                
                if (itemAmountTMP is not null)
                    itemAmountTMP.gameObject.SetActive(false);
            }
            else
            {
                if (itemImage is not null)
                {
                    itemImage.gameObject.SetActive(true);
                    itemImage.sprite = StorageSlotInfo.item.Sprite;
                }

                if (itemAmountTMP is not null)
                {
                    itemAmountTMP.gameObject.SetActive(true);
                    itemAmountTMP.text = $"{otherInfo.itemAmount}";
                }
            }
        }
    }
}
