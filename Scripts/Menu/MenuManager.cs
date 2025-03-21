using Player.Menu;
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

        private void Awake()
        {
            _menuUnlockCuisinePage = GetComponent<MenuUnlockCuisinePage>();
            _menuChooseCuisinePage = GetComponent<MenuChooseCuisinePage>();
        }

        private void OnEnable()
        {
            OnCuisineTypeButtonClick(defaultMenuData);
        }
        
        /// <summary>
        /// 當玩家按下菜品種類的更換按鈕後，就會呼叫這個方法。
        /// 用於 Button 的 On Clicked()。
        /// </summary>
        /// <param name="menuData"> 傳入的菜單的資料庫 </param>
        public void OnCuisineTypeButtonClick(MenuData menuData)
        {
            _menuUnlockCuisinePage.InitSlot(menuData.UnlockCuisineData);
        }
    }
}
