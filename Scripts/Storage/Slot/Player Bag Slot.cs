using UnityEngine;

namespace Storage.Slot
{
    public class PlayerBagSlot : StorageSlotCore
    {
        [field: Header("格子狀態圖片"), Tooltip("格子鎖上的圖片"), SerializeField]
        private Sprite slotLockedSprite;
        
        [field: Tooltip("格子解鎖的圖片"), SerializeField]
        private Sprite slotUnlockedSprite;
    }
}
