using System.Collections.Generic;
using DataBase.Item.Category.Cuisine;
using DataBase.Menu.ChooseCuisine;
using UnityEngine;

namespace NPC.Customer
{
    /// <summary>
    /// 用來管理顧客點餐的類。
    /// 需要 CustomerManager 這個類的支援。
    /// </summary>
    [RequireComponent(typeof(CustomerManager))]
    public class CustomerOrder : MonoBehaviour
    {
        [Header("資料庫"), Tooltip("本日上架的主菜的資料庫。"), SerializeField]
        private PlayerChooseCuisineData mainCourseData;
        
        [Tooltip("本日上架的飲料的資料庫。"), SerializeField]
        private PlayerChooseCuisineData drinkCuisineData;
        
        // 顧客選擇的菜品的陣列的暫存。
        private List<Cuisine> _chooseCuisineList = new List<Cuisine>();
        
        // 自身的 CustomerOrderBubble 的組件，用於顧客氣泡的生成的類。
        private CustomerOrderBubble _customerOrderBubble;

        // 顧客的狀態的暫存。
        public State CustomerState { get; private set; } = State.NotOnSeat;
        public enum State
        {
            NotOnSeat,
            Thinking,
            WaitingOrder,
            WaitingCuisine,
            EatingCuisine
        }

        private void Awake()
        {
            _customerOrderBubble = GetComponent<CustomerOrderBubble>();
        }

        /// <summary>
        /// 切換顧客的狀態的類。
        /// </summary>
        /// <param name="nextState"> 新傳入的狀態。 </param>
        public void OnStateChange(State nextState)
        {
            CustomerState = nextState;
            
            switch (CustomerState)
            {
                case State.NotOnSeat:
                {
                    break;
                }
                case State.Thinking:
                {
                    _customerOrderBubble.InitThinkingBubble();
                    break;
                }
                case State.WaitingOrder:
                {
                    break;
                }
                case State.WaitingCuisine:
                {
                    break;
                }
                case State.EatingCuisine:
                {
                    break;
                }
            }
        }
        
        /// <summary>
        /// 用來當作顧客選擇主菜的類。
        /// </summary>
        public void ChooseMainCourse()
        {
            ChooseCuisine(mainCourseData);
        }

        /// <summary>
        /// 用來當作顧客選擇飲品的類。
        /// </summary>
        public void ChooseDrink()
        {
            ChooseCuisine(drinkCuisineData);
        }

        /// <summary>
        /// 用來執行顧客選擇菜品的類，需傳入 PlayerChooseCuisineData。
        /// </summary>
        /// <param name="cuisineData"> 當日玩家所選擇的菜品的資料。 </param>
        /// <returns> 回傳顧客所選擇的菜品。 </returns>
        private Cuisine ChooseCuisine(PlayerChooseCuisineData cuisineData)
        {
            var canChooseCuisineList = new List<Cuisine>();

            // 遍歷目標資料的所有的上架菜品，並儲存進暫存陣列。
            foreach (var slotData in cuisineData.SlotDataList)
            {
                if (slotData.cuisineData is not null)
                    canChooseCuisineList.Add(slotData.cuisineData);
            }
            
            var targetCuisine = canChooseCuisineList[Random.Range(0, canChooseCuisineList.Count)];

            _chooseCuisineList.Add(targetCuisine);
            return targetCuisine;
        }
    }
}