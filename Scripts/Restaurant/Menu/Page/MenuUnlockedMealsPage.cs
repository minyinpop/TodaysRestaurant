using System.Collections.Generic;
using Database.Player.Meals;
using Restaurant.Menu.Slot;
using UnityEngine;

namespace Restaurant.Menu.Page
{
    // ==================================================
    // 在餐廳經營時，玩家所選擇上架料理的菜單。
    // 用於顯示玩家，當前所選擇的料理類別，哪些料理可以上架。
    // ==================================================
    
    [RequireComponent(typeof(MenuManager))]
    public class MenuUnlockedMealsPage : MonoBehaviour
    {
        // ========== { 資料相關 } ==========
        
        // 玩家當前選擇的料理種類，所顯示的資料庫。
        // 該資料庫是用於讀取玩家解鎖了甚麼料理。
        private PlayerUnlockedMealsTypeSO CurrentUnlockedMeals { get; set; }
        
        
        
        // ========== { 格子 & 生成相關 } ==========
        
        [field: Header("料理格子"), Tooltip("已解鎖料理的格子預製件。"), SerializeField]
        private GameObject UnlockedMealsSlotPrefab { get; set; }
        
        [field: Tooltip("格子的生成點。"), SerializeField]
        private Transform SlotSpawnPoint { get; set; }
        
        // 用於暫存生成格子的陣列。
        private List<GameObject> UnlockedMealsSlotList { get; set; } = new();
        
        
        
        /// <summary>
        /// 用於刷新玩家已解鎖的料理格子。
        /// 僅限於給 MenuManager 呼叫使用。
        /// </summary>
        /// <param name="newUnlockedMeals"> 新傳入的已解鎖的料理資料庫。 </param>
        public void Refresh(PlayerUnlockedMealsTypeSO newUnlockedMeals)
        {
            ClearLegacySlot();
            CurrentUnlockedMeals = newUnlockedMeals;

            foreach (var meals in CurrentUnlockedMeals.UnlockedMealsList)
            {
                var slot = Instantiate(UnlockedMealsSlotPrefab, SlotSpawnPoint);
                slot.GetComponent<MenuUnlockedMealsSlot>().Refresh(meals);
                
                UnlockedMealsSlotList.Add(slot);
            }
        }
        
        
        
        /// <summary>
        /// 用於清理上一個料理種類頁面，所遺留的格子。
        /// 通常是玩家在點擊切換料理種類的標籤後，才會執行到的方法。
        /// </summary>
        private void ClearLegacySlot()
        {
            foreach (var slot in UnlockedMealsSlotList)
                Destroy(slot);
            
            UnlockedMealsSlotList.Clear();
        }
    }
}