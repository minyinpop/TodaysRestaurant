using Player.Menu.ChooseCuisine;
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
        [Header("圖片"), Tooltip("格子解鎖時的底圖 (未有料理)"), SerializeField]
        private Sprite unlockedSlotWithoutCuisineSprite;
        
        [Tooltip("格子解鎖時的底圖 (有料理)"), SerializeField]
        private Sprite unlockedSlotWithCuisineSprite;
        
        [Header("組件"), Tooltip("用來顯示格子底圖的圖片組件。"), SerializeField]
        private Image slotImage;
        
        [Tooltip("用來顯示菜品的圖片組件。"), SerializeField]
        private Image cuisineImage;

        [Tooltip("用來顯示菜品名稱與數量的文字組件。"), SerializeField]
        private TextMeshProUGUI cuisineAmountTMP;
        
        // 當前格子的資料。
        private PlayerChooseCuisineSlotData _slotData;
        
        // 當玩家按下該格子後，所使用的事件廣播。
        // 目前為 MenuManager 訂閱該廣播。
        public static event System.Action onClick;

        /// <summary>
        /// 當玩家點擊 MenuUnlockCuisineSlot 後，所更新此格子用的方法。
        /// </summary>
        public void Refresh(PlayerChooseCuisineSlotData newSlotData)
        {
            SlotRefresh(newSlotData);
        }

        /// <summary>
        /// 當玩家點擊該格子，就清空該格子裡的資料。
        /// 給予 Button 組件裡的 On Click() 做使用。
        /// </summary>
        public void OnClick()
        {
            SlotRefresh(new PlayerChooseCuisineSlotData());
            onClick?.Invoke();
        }

        /// <summary>
        /// 用來執行格子顯示邏輯的方法。
        /// </summary>
        /// <param name="newSlotData"></param>
        private void SlotRefresh(PlayerChooseCuisineSlotData newSlotData)
        {
            _slotData = newSlotData;
            
            // 如果該格子是上鎖的，那就直接取消下面的判斷。
            if (newSlotData.isLocked)
                return;
            
            // 如果該格子裡是沒有菜品的，就把格子的圖片給改成 unlockedSlotWithoutCuisineSprite，
            // 並把 cuisineImage 與 cuisineAmountTMP 給取消顯示，
            if (_slotData.cuisineData is null)
            {
                cuisineImage.sprite = unlockedSlotWithoutCuisineSprite;
                
                if (cuisineImage.gameObject.activeSelf)
                {
                    cuisineImage.sprite = null;
                    cuisineImage.gameObject.SetActive(false);
                }

                if (cuisineAmountTMP.gameObject.activeSelf)
                {
                    cuisineAmountTMP.text = "";
                    cuisineAmountTMP.gameObject.SetActive(false);
                }
            }
            // 如果該格子裡是有菜品的，就把格子的圖片給改成 unlockedSlotWithCuisineSprite，
            // 並顯示 cuisineImage 與 cuisineAmountTMP。
            else
            {
                slotImage.sprite = unlockedSlotWithCuisineSprite;
                
                cuisineImage.gameObject.SetActive(true);
                cuisineImage.sprite = _slotData.cuisineData.Sprite;
                
                cuisineAmountTMP.gameObject.SetActive(true);
                cuisineAmountTMP.text = $"{_slotData.cuisineData.Name} x{_slotData.cuisineData.MenuQuantity}";
            }
        }
    }
}
