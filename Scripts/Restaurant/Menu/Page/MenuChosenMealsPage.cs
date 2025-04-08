using System.Collections.Generic;
using Database.Restaurant.Chosen;
using Database.Restaurant.Meals;
using Restaurant.Menu.Slot;
using UnityEngine;

namespace Restaurant.Menu.Page
{
    // ==================================================
    // 在餐廳經營時，玩家所選擇上架料理的菜單。
    // 用於顯示玩家，當前所選擇的料理類別，哪些料理被選擇上架。
    // ==================================================
    
    [RequireComponent(typeof(MenuManager))]
    public class MenuChosenMealsPage : MonoBehaviour
    {
        // ========== { 資料相關 } ==========
        
        // 玩家當前選擇的料理種類，所顯示的資料庫。
        // 該資料庫是用於讀取玩家解鎖了甚麼料理。
        private ChosenMealsTypeSO CurrentChosenMeals { get; set; }
        
        
        
        // ========== { 格子 & 生成相關 } ==========
        
        [field: Header("已選擇的料理格子"), Tooltip("上鎖的格子預製件。"), SerializeField]
        private GameObject LockedChosenMealsSlotPrefab { get; set; }
        
        [field: Tooltip("解鎖且沒有料理的格子預製件。"), SerializeField]
        private GameObject UnlockedChosenMealsSlotWithoutMealsPrefab { get; set; }
        
        [field: Tooltip("解鎖且有料理的格子預製件。"), SerializeField]
        private GameObject UnlockedChosenMealsSlotWithMealsPrefab { get; set; }
        
        [field: Tooltip("格子的生成點。"), SerializeField]
        private Transform SlotSpawnPoint { get; set; }
        
        // 用於暫存生成格子的陣列。
        private List<GameObject> ChosenMealsSlotList { get; set; } = new();
        
        
        
        private void OnEnable()
        {
            MenuUnlockedMealsSlot.OnUnlockedMealsSlotClicked += AddMeals;
            MenuChosenMealsSlot.OnChosenMealsSlotClicked += RemoveMeals;
        }
        
        
        
        private void OnDisable()
        {
            MenuUnlockedMealsSlot.OnUnlockedMealsSlotClicked -= AddMeals;
            MenuChosenMealsSlot.OnChosenMealsSlotClicked -= RemoveMeals;
        }



        /// <summary>
        /// 當玩家點擊已解鎖的料理的格子後，就會添加到被選擇料理的格子裡。
        /// 僅限訂閱 MenuUnlockedMealsSlot 的 OnChosenMealsSlotClicked 做使用。
        /// </summary>
        /// <param name="newMeals"> 新傳入的料理資料。 </param>
        private void AddMeals(MealsSO newMeals)
        {
            for (var i = 0; i < ChosenMealsSlotList.Count; i++)
            {
                if (!ChosenMealsSlotList[i].TryGetComponent<MenuChosenMealsSlot>(out var targetChosenMealsSlot))
                    continue;

                if (targetChosenMealsSlot.ChosenMealsSlot.IsLocked)
                    continue;
                
                if (targetChosenMealsSlot.ChosenMealsSlot.Meals is not null)
                    continue;

                Destroy(targetChosenMealsSlot.gameObject);
                ChosenMealsSlotList.RemoveAt(i);
                
                var newSlot = Instantiate(UnlockedChosenMealsSlotWithMealsPrefab, SlotSpawnPoint);
                newSlot.transform.SetSiblingIndex(i);
                newSlot.GetComponent<MenuChosenMealsSlot>().Refresh(new ChosenMealsSlot
                {
                    IsLocked = false,
                    Meals = newMeals,
                    Quantity = newMeals.Quantity
                });
                ChosenMealsSlotList.Insert(i, newSlot);
                return;
            }
        }



        /// <summary>
        /// 當玩家點擊已選擇料理的格子後，就會移除該格子的資訊。
        /// 僅限訂閱 MenuChosenMealsSlot 的 OnChosenMealsSlotClicked 做使用。
        /// </summary>
        /// <param name="legacySlot"> 傳入舊格子的遊戲物件。 </param>
        private void RemoveMeals(GameObject legacySlot)
        {
            for (var i = 0; i < ChosenMealsSlotList.Count; i++)
            {
                if (!ChosenMealsSlotList[i].Equals(legacySlot))
                    continue;
                
                var targetSlot = ChosenMealsSlotList[i];
                Destroy(targetSlot);
                ChosenMealsSlotList.RemoveAt(i);
                
                var newSlot = Instantiate(UnlockedChosenMealsSlotWithoutMealsPrefab, SlotSpawnPoint);
                newSlot.transform.SetSiblingIndex(i);
                ChosenMealsSlotList.Insert(i, newSlot);
                return;
            }
        }



        /// <summary>
        /// 用於儲存玩家所選擇的料理到資料庫中。
        /// </summary>
        public void SaveMeals()
        {
            for (var i = 0; i < ChosenMealsSlotList.Count; i++)
            {
                if (!ChosenMealsSlotList[i].TryGetComponent<MenuChosenMealsSlot>(out var targetChosenMealsSlot))
                    continue;
                
                CurrentChosenMeals.ChosenMealsList[i] = targetChosenMealsSlot.ChosenMealsSlot;
            }
        }
        
        
        
        /// <summary>
        /// 用於刷新玩家已選擇的料理格子。
        /// 僅限於給 MenuManager 呼叫使用。
        /// </summary>
        /// <param name="newChosenMeals"> 新傳入的已選擇的料理資料庫。 </param>
        public void Refresh(ChosenMealsTypeSO newChosenMeals)
        {
            ClearLegacySlot();
            CurrentChosenMeals = newChosenMeals;

            foreach (var targetChosenMealsSlot in CurrentChosenMeals.ChosenMealsList)
            {
                GameObject targetSpawnSlot;

                if (targetChosenMealsSlot.IsLocked)
                {
                    targetSpawnSlot = LockedChosenMealsSlotPrefab;
                }
                else
                {
                    targetSpawnSlot = targetChosenMealsSlot.Meals is null
                        ? UnlockedChosenMealsSlotWithoutMealsPrefab
                        : UnlockedChosenMealsSlotWithMealsPrefab;
                }

                var newSlot = Instantiate(targetSpawnSlot, SlotSpawnPoint);
                newSlot.GetComponent<MenuChosenMealsSlot>().Refresh(targetChosenMealsSlot);
                ChosenMealsSlotList.Add(newSlot);
            }
        }
        
        /// <summary>
        /// 用於清理上一個料理種類頁面，所遺留的格子。
        /// 通常是玩家在點擊切換料理種類的標籤後，才會執行到的方法。
        /// </summary>
        private void ClearLegacySlot()
        {
            foreach (var slot in ChosenMealsSlotList)
                Destroy(slot);
            
            ChosenMealsSlotList.Clear();
        }
    }
}