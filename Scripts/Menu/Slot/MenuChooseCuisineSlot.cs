using DataBase.Item.Category.Cuisine;
using DataBase.Menu.ChooseCuisine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Slot
{
    /// <summary>
    /// 用於顯示當前玩家所選擇的料理的格子。
    /// </summary>
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class MenuChooseCuisineSlot : MonoBehaviour
    {
        [Header("組件"), Tooltip("自身的按鈕組件。"), SerializeField]
        private Button button;
        
        [Tooltip("- 格子底圖的圖片組件。\n- 用來顯示當前格子的狀態。"), SerializeField]
        private Image slotBG;

        [Tooltip("- 料理的圖片組件。\n- 用來顯示當格所儲存的玩家選擇的料理。"), SerializeField]
        private Image cuisineImage;

        [Tooltip("- 料理的數量文字組件\n- 用來顯示當格料理的名稱與數量。"), SerializeField]
        private TextMeshProUGUI cuisineQuantityTMP;

        [Header("圖片"), Tooltip("- 格子解鎖，但是沒有菜品時的圖片。\n- 用於格子的底圖。"), SerializeField]
        private Sprite unlockedWithoutCuisineSprite;
        
        [Tooltip("- 格子解鎖，但是有菜品時的圖片。\n- 用於格子的底圖。"), SerializeField]
        private Sprite unlockedWithCuisineSprite;
        
        // 玩家所選擇的菜品的格子資料。
        // 裡面包含了格子是否上鎖以及菜品的資料。
        public PlayerChooseCuisineSlotData SlotData { get; private set; }
        
        /// <summary>
        /// 判斷格子是否可以添加新的菜品資料，並回添加是否成功的結果。
        /// </summary>
        /// <param name="newCuisineData"></param>
        /// <returns> 菜品是否添加成功。 </returns>
        public bool AddSlotData(Cuisine newCuisineData)
        {
            // 如果格子是上鎖的，就回傳添加失敗的結果。
            if (SlotData.isLocked)
                return false;
            
            // 如果格子裡已經有菜品資料了，就回傳添加失敗的結果。
            if (SlotData.cuisineData is not null)
                return false;

            SlotData = new PlayerChooseCuisineSlotData
            {
                isLocked = SlotData.isLocked,
                cuisineData = newCuisineData,
                cuisineRemaining = newCuisineData.MenuQuantity
            };
            Refresh();
            return true;
        }

        /// <summary>
        /// 清空格子所儲存的資訊。
        /// 用於 Button 裡的 On Click() 做使用。
        /// </summary>
        public void ClearSlotData()
        {
            SlotData = new PlayerChooseCuisineSlotData
            {
                isLocked = SlotData.isLocked,
                cuisineData = null,
                cuisineRemaining = 0
            };
            Refresh();
        }

        /// <summary>
        /// 刷新格子的顯示。
        /// </summary>
        public void Refresh(PlayerChooseCuisineSlotData newSlotData)
        {
            SlotData = newSlotData;
            Refresh();
        }

        private void Refresh()
        {
            button.interactable = SlotData.cuisineData is not null;
            
            // 如果格子是上鎖的，就直接結束邏輯。
            if (SlotData.isLocked)
                return;

            slotBG.sprite = SlotData.cuisineData is null ? unlockedWithoutCuisineSprite : unlockedWithCuisineSprite;
            
            cuisineImage.gameObject.SetActive(SlotData.cuisineData is not null);
            cuisineImage.sprite = SlotData.cuisineData?.Sprite;
            
            cuisineQuantityTMP.gameObject.SetActive(SlotData.cuisineData is not null);
            cuisineQuantityTMP.text = SlotData.cuisineData is null ? "" : $"{SlotData.cuisineData.Name} x{SlotData.cuisineData.MenuQuantity}";
        }
    }
}
