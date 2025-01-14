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
    }
}
