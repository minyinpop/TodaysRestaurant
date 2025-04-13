using System.Collections.Generic;
using Database.Restaurant.Chosen;
using Database.Restaurant.Meals;
using UnityEngine;

namespace Restaurant.Customer
{
    // ==================================================
    // 用來管理顧客狀態的程式碼。
    // ==================================================
    
    [RequireComponent(typeof(CustomerPathfinding))]
    [RequireComponent(typeof(CustomerAnimator))]
    [RequireComponent(typeof(CustomerOrder))]
    public class CustomerManager : MonoBehaviour
    {
        // ========== { 料理相關 } ==========
        
        [field: Header("資料庫"), Tooltip("已選擇的主菜資料。"), SerializeField]
        private ChosenMealsTypeSO ChosenMainCourse { get; set; }
        
        [field: Tooltip("已選擇的飲料資料。"), SerializeField]
        private ChosenMealsTypeSO ChosenDrinks { get; set; }
        
        // 顧客選擇過的料理陣列。
        private List<MealsSO> ChosenMealsList { get; set; } = new();
        
        // 顧客選擇的主菜。
        private MealsSO ChosenMainCourseMeal { get; set; }
        
        // 顧客選擇的飲料。
        private MealsSO ChosenDrinksMeal { get; set; }
        
        
        
        // ========== { 狀態相關 } ==========
        
        // 顧客在餐廳裡的狀態陣列。
        public CustomerStateEnum CustomerState { get; private set; } = CustomerStateEnum.SearchingForTheSeat;

        public enum CustomerStateEnum
        {
            SearchingForTheSeat,
            OnSeat,
            Leaving
        }



        // ========== { 自身組件 } ==========
        
        // 自身的 CustomerAnimator 組件。
        private CustomerAnimator CustomerAnimator { get; set; }
        
        // 自身的 CustomerBubble 組件。
        private CustomerOrder CustomerOrder { get; set; }
        
        
        
        public void Awake()
        {
            CustomerAnimator = GetComponent<CustomerAnimator>();
            CustomerOrder = GetComponent<CustomerOrder>();
        }
        
        
        
        /// <summary>
        /// 變更顧客狀態的方法。
        /// </summary>
        /// <param name="newCustomerState"> 新傳入的狀態。 </param>
        public void ChangeState(CustomerStateEnum newCustomerState)
        {
            CustomerState = newCustomerState;
            
            switch (CustomerState)
            {
                case CustomerStateEnum.SearchingForTheSeat:
                {
                    break;
                }
                case CustomerStateEnum.OnSeat:
                {
                    CustomerAnimator.PlaySitAnimation();
                    CustomerOrder.InitThinkingBubble();
                    break;
                }
                case CustomerStateEnum.Leaving:
                {
                    break;
                }
            }
        }
        
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns> 回傳被選中的料理資料。 </returns>
        public MealsSO ChooseMeals()
        {
            if (ChosenMainCourseMeal is null)
            {
                var chosenMeals = ChosenMainCourse.ReduceRandomMealsQuantity();
                
                ChosenMainCourseMeal = chosenMeals;
                ChosenMealsList.Add(chosenMeals);
                
                return chosenMeals;
            }
            else if (ChosenDrinksMeal is null)
            {
                // TODO 顧客選擇飲料的邏輯 ......
            }

            return null;
        }
    }
}