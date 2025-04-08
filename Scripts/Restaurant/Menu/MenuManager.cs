using Database.Restaurant.Menu;
using Restaurant.Menu.Page;
using UnityEngine;

namespace Restaurant.Menu
{
    // ==================================================
    // 在餐廳經營時，玩家所選擇上架料理的菜單。
    // ==================================================
    
    [RequireComponent(typeof(MenuUnlockedMealsPage))]
    [RequireComponent(typeof(MenuChosenMealsPage))]
    public class MenuManager : MonoBehaviour
    {
        // ========== { 資料相關 } ==========
        
        [field: Header("料理種類資料庫"), Tooltip("預設料理種類的頁面資料庫。"), SerializeField]
        public MenuPageSO DefaultMenuPage { get; private set; }
        
        
        
        // ========== { 頁面相關 } ==========
        
        // 用於顯示玩家，當前所選擇的料理類別，哪些料理可以上架。
        private MenuUnlockedMealsPage UnlockedMealsPage { get; set; }
        
        // 用於顯示玩家，當前所選擇的料理類別，哪些料理被選擇上架。
        private MenuChosenMealsPage ChosenMealsPage { get; set; }



        private void Awake()
        {
            UnlockedMealsPage = GetComponent<MenuUnlockedMealsPage>();
            ChosenMealsPage = GetComponent<MenuChosenMealsPage>();
        }
        
        
        
        /// <summary>
        /// 當菜單開啟按鈕被按下時，所執行的方法。
        /// 預設打開顯示為主菜的頁面。
        /// 僅限於給 Button 組件的 On Click() 做訂閱。
        /// </summary>
        public void OnOpenButtonPressed()
        {
            RefreshPage(DefaultMenuPage);
        }
        
        
        
        /// <summary>
        /// 當菜單關閉按鈕被按下時，所執行的方法。
        /// 在關閉前，會把被選擇的料理資料給儲存進資料庫。
        /// 僅限於給 Button 組件的 On Click() 做訂閱。 
        /// </summary>
        public void OnCloseButtonPressed()
        {
            ChosenMealsPage.SaveMeals();
            Destroy(gameObject);
        }



        /// <summary>
        /// 當玩家按下切換料理種類的按鈕後，所執行的方法。
        /// 僅限於給 Button 組件的 On Click() 做訂閱。
        /// </summary>
        /// <param name="newPage"> 新傳入的頁面資料庫。 </param>
        public void OnChangeMealsTypeButtonPressed(MenuPageSO newPage)
        {
            RefreshPage(newPage);
        }

        
        
        /// <summary>
        /// 用於刷新頁面的方法。
        /// 僅限於給 MenuManager 自己做使用。
        /// </summary>
        /// <param name="newPage"> 新傳入的頁面資料庫。 </param>
        private void RefreshPage(MenuPageSO newPage)
        {
            ChosenMealsPage.SaveMeals();
            
            UnlockedMealsPage.Refresh(newPage.UnlockedMeals);
            ChosenMealsPage.Refresh(newPage.ChosenMeals);
        }
    }
}