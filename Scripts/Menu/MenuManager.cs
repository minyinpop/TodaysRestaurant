using Item.Category.Cuisine;
using Menu.Page;
using Menu.Page.Category;
using Menu.Slot;
using Player.Menu;
using UnityEngine;

namespace Menu
{
    /// <summary>
    /// 菜單頁面的總管理器，負責所有相關的類的調度。
    /// </summary>
    [RequireComponent(typeof(MenuUnlockCuisineMenuPage))]
    [RequireComponent(typeof(MenuChooseCuisineMenuPage))]
    public class MenuManager : MonoBehaviour
    {
        [Header("菜單設定"), Tooltip("當玩家打開菜單後，預設顯示的頁面的資料。"), SerializeField]
        private MenuData defaultMenuData;
        
        // 已解鎖的菜品的頁面暫存。
        private MenuPageBase _unlockCuisineMenuPage;
        
        // 被選擇的蔡品的頁面暫存。
        private MenuPageBase _chooseCuisineMenuPage;
        
        // 被選擇的菜品的格子的暫存陣列。
        private GameObject _chooseCuisineSlotList;
        
        // 當前玩家選擇的蔡品種類的資料暫存。
        private MenuData _menuData;

        private void Awake()
        {
            _unlockCuisineMenuPage = GetComponent<MenuUnlockCuisineMenuPage>();
            _chooseCuisineMenuPage = GetComponent<MenuChooseCuisineMenuPage>();
        }
        
        private void OnEnable()
        {
            _menuData = defaultMenuData;
            
            // 初始化顯示所有頁面。
            _unlockCuisineMenuPage.InitPage(_menuData);
            _chooseCuisineMenuPage.InitPage(_menuData);
            
            MenuUnlockCuisineSlot.onClick += OnUnlockCuisineSlotClick;
            MenuChooseCuisineSlot.onClick += OnChooseCuisineSlotClick;
        }

        private void OnDisable()
        {
            MenuUnlockCuisineSlot.onClick -= OnUnlockCuisineSlotClick;
            MenuChooseCuisineSlot.onClick -= OnChooseCuisineSlotClick;
        }

        /// <summary>
        /// 當已解鎖的菜品格子被點擊後所發生的事情。
        /// </summary>
        /// <param name="slotCuisineData"> 格子裡所持有的料理資料。 </param>>
        private void OnUnlockCuisineSlotClick(Cuisine slotCuisineData)
        {
            _chooseCuisineMenuPage.AddCuisineData(slotCuisineData);
        }

        /// <summary>
        /// 當已選擇的菜品的格子被點擊後所發生的事情。
        /// </summary>
        private void OnChooseCuisineSlotClick()
        {
        }
    }
}
