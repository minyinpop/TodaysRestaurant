using UnityEngine;
using UnityEngine.UI;

namespace Storage.Slot
{
    public class PlayerBagSlot : StorageSlotCore
    {
        [field: Tooltip("儲存格的底圖組件"), SerializeField]
        private Image storageSlotImage;
        
        [field: Header("格子狀態圖片"), Tooltip("格子解鎖的圖片"), SerializeField]
        private Sprite slotUnlockedSprite;

        /// <summary>
        /// 解鎖儲存格邏輯
        /// </summary>
        /// <param name="otherInfo"></param>
        public override void Unlock(StorageSlotInfo otherInfo)
        {
            if (storageSlotImage is not null && slotUnlockedSprite is not null)
                storageSlotImage.sprite = slotUnlockedSprite;
            
            Refresh(otherInfo);
        }

        /// <summary>
        /// 更新格子邏輯
        /// </summary>
        /// <param name="otherInfo"></param>>
        public override void Refresh(StorageSlotInfo otherInfo)
        {
            if (StorageSlotInfo.state is StorageSlotInfo.StorageSlotState.Locked)
                return;
            
            if (StorageSlotInfo.Equals(otherInfo))
                return;
            
            StorageSlotInfo = otherInfo;
            storageSlotImage.sprite = slotUnlockedSprite;

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
