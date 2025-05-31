using Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Player.Storage.Category
{
    internal class PlayerHotbarSlot : PlayerStorageSlot
    {
        [field: Header("必要組件")]
        [field: SerializeField] private Image ItemImage { get; set; }
        [field: SerializeField] private TextMeshProUGUI ItemQuantity { get; set; }
        
        private ITem CurrentItem { get; set; }

        private void Awake()
        {
            CheckNull(!ItemImage, "物品圖片 ItemImage 未被掛載。");
            CheckNull(!ItemQuantity, "物品數量文字 ItemQuantity 未被掛載。");
        }

        private void CheckNull(bool condition, string message)
        {
            if (!condition) return;
#if UNITY_EDITOR
            Debug.LogWarning($"錯誤訊息：{message}\n遊戲物件：{name}\n錯誤組件：{GetType().Name}\n");
#endif
            enabled = false;
        }
    }
}