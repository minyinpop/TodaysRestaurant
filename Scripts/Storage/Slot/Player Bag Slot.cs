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
        /// 解鎖儲存格
        /// </summary>
        /// <param name="otherInfo"></param>
        public override void Unlock(StorageSlotInfo otherInfo)
        {
            if (storageSlotImage is not null && slotUnlockedSprite is not null)
                storageSlotImage.sprite = slotUnlockedSprite;
            
            Refresh(otherInfo);
        }

        /// <summary>
        /// 使用外部資訊更新格子
        /// </summary>
        /// <param name="otherInfo"></param>>
        public override void Refresh(StorageSlotInfo otherInfo)
        {
            if (otherInfo.state is StorageSlotInfo.StorageSlotState.Locked)
                return;
            
            storageSlotInfo = otherInfo;
            storageSlotImage.sprite = slotUnlockedSprite;

            if (storageSlotInfo.item is null)
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
                    itemImage.sprite = storageSlotInfo.item.Sprite;
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
