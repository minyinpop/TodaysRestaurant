using System.Collections;
using DataBase.Customer.Wait;
using NPC.Bubble.Category;
using UnityEngine;

namespace NPC.Customer
{
    /// <summary>
    /// 用來管理顧客點餐的氣泡的類。
    /// 需要 CustomerManager 與 CustomerOrder 這兩個類的支援。
    /// </summary>
    [RequireComponent(typeof(CustomerManager))]
    [RequireComponent(typeof(CustomerOrder))]
    public class CustomerOrderBubble : MonoBehaviour
    {
        [Header("資料庫"), Tooltip("顧客等待的時間的資料庫。"), SerializeField]
        private CustomerWaitTimeData waitTimeData;
        
        
        
        [Header("位置"), Tooltip("用於生成氣泡的位置。"), SerializeField]
        private Transform bubbleSpawnPoint;
        
        [Header("預製件"), Tooltip("用於顯示角色在思考的氣泡的預製件。"), SerializeField]
        private GameObject thinkingBubblePrefab;
        
        [Tooltip("用於顯示角色在等待服務員的氣泡的預製件。"), SerializeField]
        private GameObject waitOrderBubblePrefab;
        
        [Tooltip("用於顯示角色在等待餐點的氣泡的預製件"), SerializeField]
        private GameObject waitCuisineBubblePrefab;
        
        
        
        // 當前的氣泡的遊戲物件的暫存。
        private GameObject _bubble;
        
        // 自身的 CustomerOrder 的組件，當作顧客點餐的類。
        private CustomerOrder _customerOrder;
        
        // 當前氣泡的異步協程的暫存。
        private IEnumerator _currentCoroutine;

        private void Awake()
        {
            _customerOrder = GetComponent<CustomerOrder>();
        }

        private void OnDisable()
        {
            if (_currentCoroutine is not null)
                StopCoroutine(_currentCoroutine);
        }

        
        
        /// <summary>
        /// 用於生成思考的氣泡的類。
        /// </summary>
        public void InitThinkingBubble()
        {
            if (_currentCoroutine is not null)
            {
                StopCoroutine(_currentCoroutine);
                _currentCoroutine = null;
            }

            _currentCoroutine = ThinkingBubbleCoroutine();
            StartCoroutine(_currentCoroutine);
        }
        
        private IEnumerator ThinkingBubbleCoroutine()
        {
            if (_bubble is not null)
            {
                Destroy(_bubble);
                _bubble = null;
            }

            _bubble = Instantiate(thinkingBubblePrefab, bubbleSpawnPoint);
            
            yield return new WaitForSeconds(Random.Range(waitTimeData.MinThinkTime, waitTimeData.MaxThinkTime));

            InitWaitOrderBubble();
        }

        
        


        /// <summary>
        /// 用於生成等待服務員的氣泡的類。
        /// </summary>
        private void InitWaitOrderBubble()
        {
            if (_currentCoroutine is not null)
            {
                StopCoroutine(_currentCoroutine);
                _currentCoroutine = null;
            }

            _currentCoroutine = WaitOrderBubbleCoroutine();
            StartCoroutine(_currentCoroutine);
        }
        
        private IEnumerator WaitOrderBubbleCoroutine()
        {
            if (_bubble is not null)
            {
                Destroy(_bubble);
                _bubble = null;
            }

            var targetWaitTime = Random.Range(waitTimeData.MinWaitOrderTime, waitTimeData.MaxWaitOrderTime);

            _bubble = Instantiate(waitOrderBubblePrefab, bubbleSpawnPoint);
            _bubble.GetComponent<WaitOrderBubble>().InitBubble(targetWaitTime, this);

            yield return new WaitForSeconds(targetWaitTime);
        }
        
        
        
        
        
        /// <summary>
        /// 用於生成等待餐點的氣泡的類。
        /// </summary>
        public void InitWaitCuisineBubble()
        {
        }

        private IEnumerator WaitCuisineBubbleCoroutine()
        {
            yield return new WaitForSeconds(1);
        }
    }
}
