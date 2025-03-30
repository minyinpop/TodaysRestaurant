using System.Collections;
using System.Collections.Generic;
using DataBase.Customer.Wait;
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
        [Header("資料庫"), Tooltip("顧客等待的時間的資料庫。"), SerializeField]
        private CustomerWaitTimeData waitTimeData;
         
        [Tooltip("本日上架的主菜的資料庫。"), SerializeField]
        private PlayerChooseCuisineData mainCourseData;
        
        [Tooltip("本日上架的飲料的資料庫。"), SerializeField]
        private PlayerChooseCuisineData drinkCuisineData;
        
        
        
        // 自身的 CustomerOrderBubble 的組件，用於顧客氣泡的生成的類。
        private CustomerOrderBubble _customerOrderBubble;
        
        
        
        // 執行顧客用餐的異步協程的暫存。
        private IEnumerator _eatingCuisineCoroutine;
        
        
        
        // 顧客當前選擇的料理的暫存。
        private Cuisine _targetCuisine;
        
        // 判斷顧客是否點了食物。
        private bool _isOrderMainCourse;
        
        // 判斷顧客是否點了飲料。
        private bool _isOrderDrink;
        
        
        
        // 顧客選擇的菜品的陣列的暫存。
        // 此陣列可以在結帳算錢使用。
        private List<Cuisine> _chooseCuisineList = new List<Cuisine>();
        
        
        
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

        private void OnDisable()
        {
            if (_eatingCuisineCoroutine is not null)
            {
                StopCoroutine(_eatingCuisineCoroutine);
                _eatingCuisineCoroutine = null;
            }
        }

        /// <summary>
        /// 切換顧客的狀態的類。
        /// 在切換狀態的同時，可以同步執行其它事件，像是生成下一個狀態氣泡等等。
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
                    _customerOrderBubble.InitWaitOrderBubble();
                    break;
                }
                case State.WaitingCuisine:
                {
                    if (!_isOrderMainCourse)
                        _customerOrderBubble.InitWaitCuisineBubble(ChooseCuisine(mainCourseData));
                    else if (!_isOrderDrink)
                        _customerOrderBubble.InitWaitCuisineBubble(ChooseCuisine(drinkCuisineData));
                    break;
                }
                case State.EatingCuisine:
                {
                    if (_eatingCuisineCoroutine is not null)
                    {
                        StopCoroutine(_eatingCuisineCoroutine);
                        _eatingCuisineCoroutine = null;
                    }
                    
                    _eatingCuisineCoroutine = EatingCuisineCoroutine();
                    StartCoroutine(_eatingCuisineCoroutine);
                    break;
                }
            }
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
            foreach (var slotData in cuisineData.slotDataList)
            {
                if (slotData.cuisineData is not null)
                    canChooseCuisineList.Add(slotData.cuisineData);
            }
            
            _targetCuisine = canChooseCuisineList[Random.Range(0, canChooseCuisineList.Count)];

            _chooseCuisineList.Add(_targetCuisine);
            return _targetCuisine;
        }

        /// <summary>
        /// 用來執行顧客獲得料理的方法。
        /// </summary>
        /// <param name="cuisineData"> 獲得到的料理。 </param>
        public void GetCuisine(Cuisine cuisineData)
        {
            // 如果顧客還沒有收到主菜，先傳入進來的料理自動歸類為主菜。
            // 並且以主菜的邏輯去判斷。
            if (!_isOrderMainCourse)
            {
                // 如果玩家給予的料理與顧客當時點的一樣，顧客就很開心 :D
                // 反之顧客則不開心 :<
                _customerOrderBubble.InitMoodleBubble(_targetCuisine.Equals(cuisineData));
                
                _isOrderMainCourse = true;
                OnStateChange(State.EatingCuisine);
            }
            else if (!_isOrderDrink)
            {
                // TODO: 顧客收到飲料後的邏輯。
            }
        }
        
        /// <summary>
        /// 用來執行顧客使用餐點的異步協程的方法。
        /// 當顧客使用餐點，會判斷要不要繼續點餐，或是直接結帳。
        /// </summary>
        /// <returns></returns>
        private IEnumerator EatingCuisineCoroutine()
        {
            var minTime = waitTimeData.MinEatTime + _targetCuisine.EatTime;
            var maxTime = waitTimeData.MaxEatTime + _targetCuisine.EatTime;
            var targetTime = Random.Range(minTime, maxTime);
            
            yield return new WaitForSeconds(targetTime);

            _targetCuisine = null;
            
            // 如果顧客還沒有點飲料，就在吃完主菜後，等待玩家來幫忙點餐。
            if (!_isOrderDrink)
                OnStateChange(State.WaitingOrder);
        }
    }
}