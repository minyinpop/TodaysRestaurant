using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Storage.Slot
{
    /// <summary>
    /// 用來整合所有有關於到儲物格介面的抽象類。
    /// </summary>
    public abstract class StorageSlotUI : MonoBehaviour
    {
        [Header("物品顯示組件"), Tooltip("用來顯示物品圖片的圖片組件。"), SerializeField]
        private Image itemImage;
        
        [Tooltip("用來顯示物品數量的文字組件。"), SerializeField]
        private TextMeshProUGUI itemQuantityTMP;
        
        // 該儲物格的物品資料的數據暫存。
        // TODO: 建立 struct 來儲存 slot 的數據。
    }
}
