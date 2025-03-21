using Item.Category.Cuisine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Slot
{
    /// <summary>
    /// 用於顯示當前玩家所選擇的菜品的格子。
    /// </summary>
    public class MenuChooseCuisineSlot : MonoBehaviour
    {
        [Header("組件"), Tooltip("用來顯示菜品的圖片組件。"), SerializeField]
        private Image cuisineImage;

        [Tooltip("用來顯示菜品名稱與數量的文字組件。"), SerializeField]
        private TextMeshProUGUI cuisineAmountTMP;

        // 當前格子所儲存的菜品資料。
        private Cuisine _cuisineData;
    }
}
