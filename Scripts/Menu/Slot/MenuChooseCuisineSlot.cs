using Item.Category.Cuisine;
using Player.Menu.ChooseCuisine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Slot
{
    /// <summary>
    /// 用於顯示當前玩家所選擇的料理的格子。
    /// </summary>
    public class MenuChooseCuisineSlot : MonoBehaviour
    {
        [Header("組件"), Tooltip("- 格子底圖的圖片組件。\n- 用來顯示當前格子的狀態。"), SerializeField]
        private Image slotBG;

        [Tooltip("- 料理的圖片組件。\n- 用來顯示當格所儲存的玩家選擇的料理。"), SerializeField]
        private Image cuisineImage;

        [Tooltip("- 料理的數量文字組件\n- 用來顯示當格料理的名稱與數量。"), SerializeField]
        private TextMeshProUGUI cuisineQuantityTMP;

        [Header("圖片"), Tooltip("- 格子解鎖，但是沒有菜品時的圖片。\n- 用於格子的底圖。"), SerializeField]
        private Sprite unlockedWithoutCuisineSprite;
        
        [Tooltip("- 格子解鎖，但是有菜品時的圖片。\n- 用於格子的底圖。"), SerializeField]
        private Sprite unlockedWithCuisineSprite;
        
        
        
        // 當格子被點擊後，所使用的廣播。
        // 目前為 MenuManager 做訂閱。
        public static event System.Action onClick;

        /// <summary>
        /// 用來執行 Button 的 On Click() 邏輯。
        /// 掛載在 Button 組件裡的 On Click() 做使用。
        /// </summary>
        public void OnClick()
        {
            onClick?.Invoke();
        }
        
        
        
        // 玩家所選擇的菜品的格子資料。
        // 裡面包含了格子是否上鎖以及菜品的資料。
        private PlayerChooseCuisineSlotData _slotData;

        /// <summary>
        /// 刷新格子的顯示。
        /// </summary>
        public void Refresh(PlayerChooseCuisineSlotData newSlotData)
        {
            _slotData = newSlotData;

            // 如果格子是上鎖的，就直接結束邏輯。
            if (_slotData.isLocked)
                return;

            slotBG.sprite = unlockedWithoutCuisineSprite;
            
            // 如果格子裡還沒有菜品的資料，就直接結束邏輯。
            if (_slotData.cuisineData is null)
                return;
            
            // TODO: 撰寫如果格子裡有菜品時，所執行的邏輯。
        }
    }
}
