using System;
using System.Collections;
using Database.Restaurant.Customer.Attribute;
using Restaurant.Bubble.Customer;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Restaurant.Customer
{
    // ==================================================
    // 用於執行顧客行動氣泡的程式碼。
    // ==================================================
    
    [RequireComponent(typeof(CustomerManager))]
    public class CustomerOrder : MonoBehaviour
    {
        // ========== { 資料相關 } ==========
        
        [field: Header("玩家屬性資料"), SerializeField]
        public CustomerAttributeSO CustomerAttribute { get; private set; }
        
        
        
        // ========== { 自身組件 } ==========
        
        // 自身的 CustomerManager 組件。
        private CustomerManager CustomerManager { get; set; }
        
        
        
        // ========== { 氣泡相關 } ==========
        
        [field: Header("氣泡"), Tooltip("氣泡的生成位置。"), SerializeField]
        private Transform BubbleSpawnPoint { get; set; }
        
        [field: Tooltip("顧客在思考時的氣泡。"), SerializeField]
        private GameObject ThinkingBubble { get; set; }
        
        [field: Tooltip("顧客在點餐時的氣泡。"), SerializeField]
        private GameObject OrderingBubble { get; set; }
        
        [field: Tooltip("顧客在等待餐點時的氣泡。"), SerializeField]
        private GameObject WaitingForMealBubble { get; set; }
        
        // 當前生成的氣泡遊戲物件。
        private GameObject CurrentBubble { get; set; }
        
        // 當前生成的氣泡按鈕組件。
        private Button CurrentBubbleButton { get; set; }
        
        
        
        // ========== { 異步協程 } ==========
        
        // 當前執行的異步協程。
        private IEnumerator CurrentCoroutine { get; set; }

        

        private void Awake()
        {
            CustomerManager = GetComponent<CustomerManager>();
        }
        
        
        
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (CurrentBubbleButton is not null)
                    CurrentBubbleButton.interactable = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (CurrentBubbleButton is not null)
                    CurrentBubbleButton.interactable = false;
            }
        }
        
        
        
        private void OnDisable()
        {
            if (CurrentCoroutine is not null)
            {
                StopCoroutine(CurrentCoroutine);
                CurrentCoroutine = null;
            }
        }
        
        
        
        /// <summary>
        /// 用於生成思考氣泡的方法。
        /// 顧客在思考要點甚麼餐點。
        /// </summary>
        public void InitThinkingBubble()
        {
            CheckBubbleIsExist();
            CheckCoroutineIsExist();
        
            CurrentBubble = Instantiate(ThinkingBubble, BubbleSpawnPoint);
            
            var randomThinkingTime = Random.Range(CustomerAttribute.ThinkingTime.Min, CustomerAttribute.ThinkingTime.Max);
            CurrentBubble.GetComponent<ThinkingBubble>().OnInit(randomThinkingTime);
            
            CurrentCoroutine = InitOrderingBubble(randomThinkingTime);
            StartCoroutine(CurrentCoroutine);
        }
        
        
        
        /// <summary>
        /// 生成顧客點餐的氣泡。
        /// </summary>
        /// <param name="randomThinkingTime"> 傳入隨機的顧客思考時間。 </param>
        private IEnumerator InitOrderingBubble(float randomThinkingTime)
        {
            yield return new WaitForSeconds(randomThinkingTime + 1);
            
            CheckBubbleIsExist();
            CheckCoroutineIsExist();

            CurrentBubble = Instantiate(OrderingBubble, BubbleSpawnPoint);
            CurrentBubbleButton = CurrentBubble.GetComponent<Button>();

            var randomPatienceTime = Random.Range(CustomerAttribute.OrderingPatienceTime.Min, CustomerAttribute.OrderingPatienceTime.Max);
            CurrentBubble.GetComponent<OrderingBubble>().OnInit(this, randomPatienceTime);
        }



        /// <summary>
        /// 生成顧客想要的餐點氣泡。
        /// </summary>
        public void InitWaitingForMealBubble()
        {
            CheckBubbleIsExist();
            CheckCoroutineIsExist();

            CurrentBubble = Instantiate(WaitingForMealBubble, BubbleSpawnPoint);
            CurrentBubbleButton = CurrentBubble.GetComponent<Button>();
            
            var randomPatienceTime = Random.Range(CustomerAttribute.WaitingForMealPatienceTime.Min, CustomerAttribute.WaitingForMealPatienceTime.Max);
            CurrentBubble.GetComponent<WaitingForMealBubble>().OnInit(this, CustomerManager.ChooseMeals(), randomPatienceTime);
        }
        
        
        
        /// <summary>
        /// 判斷當前是否有氣泡，並刪除以及清空暫存氣泡。
        /// </summary>
        private void CheckBubbleIsExist()
        {
            if (CurrentBubble is null)
                return;
            
            Destroy(CurrentBubble);
            CurrentBubble = null;
        }

        
        
        /// <summary>
        /// 判斷當前是否有執行協程，並清空暫存。
        /// </summary>
        private void CheckCoroutineIsExist()
        {
            if (CurrentCoroutine is null)
                return;
            
            StopCoroutine(CurrentCoroutine);
            CurrentCoroutine = null;
        }
    }
}