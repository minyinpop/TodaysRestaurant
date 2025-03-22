using Item.Category.Cuisine;
using Menu.Slot;
using Player.Menu;
using Player.Menu.ChooseCuisine;
using UnityEngine;

namespace Menu
{
    /// <summary>
    /// 菜單頁面的總管理器，負責所有相關的類的調度。
    /// </summary>
    [RequireComponent(typeof(MenuUnlockCuisinePage))]
    [RequireComponent(typeof(MenuChooseCuisinePage))]
    public class MenuManager : MonoBehaviour
    {
        [Header("資料庫"), Tooltip("用於開啟菜單葉面後，所載入的預設的資料。"), SerializeField]
        private MenuData defaultMenuData;
        
        // 用來暫存已解鎖的菜品的頁面的類。
        private MenuUnlockCuisinePage _menuUnlockCuisinePage;
        
        // 用來暫存被選擇的菜品的頁面的類。
        private MenuChooseCuisinePage _menuChooseCuisinePage;
        
        // 用來暫存當前玩家所選擇的菜品的頁面。
        private MenuData _menuData;

        private void Awake()
        {
            _menuUnlockCuisinePage = GetComponent<MenuUnlockCuisinePage>();
            _menuChooseCuisinePage = GetComponent<MenuChooseCuisinePage>();
        }

        private void OnEnable()
        {
            _menuData = defaultMenuData;
            OnCuisineTypeButtonClick(_menuData);
            
            MenuUnlockCuisineSlot.onClick += OnUnlockCuisineButtonClick;
            MenuChooseCuisineSlot.onClick += OnChooseCuisineButtonClick;
        }

        private void OnDisable()
        {
            MenuUnlockCuisineSlot.onClick -= OnUnlockCuisineButtonClick;
            MenuChooseCuisineSlot.onClick -= OnChooseCuisineButtonClick;
        }
        
        /// <summary>
        /// 當玩家按下菜品種類的更換按鈕後，就會呼叫這個方法。
        /// 用於 Button 的 On Clicked()。
        /// </summary>
        /// <param name="menuData"> 傳入的菜單的資料庫 </param>
        public void OnCuisineTypeButtonClick(MenuData menuData)
        {
            _menuData = menuData;
            
            _menuUnlockCuisinePage.InitSlot(_menuData.UnlockCuisineData);
            _menuChooseCuisinePage.InitSlot(_menuData.ChooseCuisineData);
        }

        /// <summary>
        /// 當玩家按下已解鎖的菜品的格子後，就會呼叫這個方法。
        /// 用於更新 ChooseCuisinePage 做使用。
        /// </summary>
        /// <param name="cuisineData"></param>
        private void OnUnlockCuisineButtonClick(Cuisine cuisineData)
        {
            // 去遍歷整個菜單介面的被選擇的菜品的格子的列表。
            for (var i = 0; i < _menuData.ChooseCuisineData.SlotDataList.Length; i++)
            {
                var slotData = _menuData.ChooseCuisineData.SlotDataList[i];

                // 如果第 i 個格子是上鎖的，就直接判斷下一個。
                if (slotData.isLocked)
                    continue;
                
                // 如果第 i 個格子已經有菜品資料，就直接判斷下一個。
                if (slotData.cuisineData is not null)
                    continue;
                
                _menuData.ChooseCuisineData.AddSlotData(i, cuisineData);

                var newSlotData = new PlayerChooseCuisineSlotData
                {
                    isLocked = false,
                    cuisineData = cuisineData
                };
                _menuChooseCuisinePage.AddDataToList(i, newSlotData);

                return;
            }
        }
        
        /// <summary>
        /// 當玩家按下已選擇的蔡品的格子後，就會呼叫這個方法。
        /// </summary>
        private void OnChooseCuisineButtonClick()
        {
        }
    }
}
