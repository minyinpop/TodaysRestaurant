using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Storage.Slot.Category
{
    /// <summary>
    /// 玩家快捷欄的儲物格，繼承了儲物格抽象類。
    /// </summary>
    public class PlayerInventoryStorageSlot : StorageSlot
    {
        [Header("顯示用組件"), Tooltip("顯示當前物品圖片的組件。"), SerializeField]
        private Image itemImage;

        [Tooltip("顯示當前物品數量的組件。"), SerializeField]
        private TextMeshProUGUI itemQuantityTMP;
    }
}
