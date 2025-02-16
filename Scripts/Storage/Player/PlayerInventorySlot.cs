using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Storage.Player
{
    public class PlayerInventorySlot : StorageSlot
    {
        [field: Tooltip("用來顯示物品的圖片組件。"), SerializeField]
        private Image itemImage;

        [field: Tooltip("用來顯示物品數量的文字組件。"), SerializeField]
        private TextMeshProUGUI itemQuantityTMP;
    }
}
