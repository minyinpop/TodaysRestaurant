using System.Collections;
using DataBase.Customer.Wait;
using UnityEngine;
using UnityEngine.UI;

namespace NPC.Customer.Bubble.Category
{
    /// <summary>
    /// 用於 NPC 點餐時，頭上所顯示的氣泡狀態，會告訴玩家當前 NPC 的狀態是甚麼。
    /// 繼承了 ThinkBubble 這個抽象類。
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class OrderThinkBubble : ThinkBubble
    {
        // 顧客的等待時間的資料庫。
        private CustomerWaitTimeData _waitTimeData;
        
        // 用來判斷顧客是否開始消耗耐心來等待事件。
        private bool _startCountdown;
        
        // 用來當作距離沒耐心的閥值得暫存。
        private float _targetWaitTime;
        
        
        
        [Header("圖片顯示組件"), Tooltip("用來顯示點餐前思考的圖片組件，用於 UI。"), SerializeField]
        private Image thinkingImage;
        
        [Tooltip("用來顯示要點餐時的圖片組件，用於 UI。"), SerializeField]
        private Image orderImage;

        [Tooltip("用來顯示要甚麼餐點的圖片組件，用於 UI。"), SerializeField]
        private Image cuisineImage;
        
        [Tooltip("用來顯示要結帳的圖片組件，用於 UI。"), SerializeField]
        private Image checkoutImage;
        
        [Tooltip("用來顯示當前 NPC 還剩下多少耐心的遮罩的圖片組件，用於 UI。"), SerializeField]
        private Image maskImage;
        
        
        
        [Header("心情組件"), Tooltip("用來顯示顧客表情的圖片組件。"), SerializeField]
        private Image emotionImage;
        
        [Tooltip("顧客開心的表情的圖片。"), SerializeField]
        private Sprite happySprite;

        [Tooltip("顧客生氣的表情的圖片。"), SerializeField]
        private Sprite angrySprite;
        
        
        
        // 自身的 Button 組件，用來開關是否可以互動。
        private Button _button;
        
        
        
        // 當作思考氣泡的異步協程的暫存。
        private IEnumerator _thinkingProcess;

        // 當前角色的點餐狀況。
        private State _bubbleState = State.Thinking;
        private enum State
        {
            Thinking,
            Ordering,
            WaitingForCuisine,
            Checkout,
            Happy,
            Angry
        };

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Update()
        {
            if (_startCountdown)
                maskImage.fillAmount += Time.deltaTime / _targetWaitTime;
        }
        
        private void OnDisable()
        {
            if (_thinkingProcess is not null)
            {
                StopCoroutine(_thinkingProcess);
                _thinkingProcess = null;
            }
        }

        /// <summary>
        /// 初始化點餐氣泡用的方法。
        /// </summary>
        /// <param name="newWaitTimeData"> 新傳入的顧客的等待資料。 </param>
        public override void InitBubble(CustomerWaitTimeData newWaitTimeData)
        {
            _waitTimeData = newWaitTimeData;
            
            _thinkingProcess = ThinkingProcess();
            StartCoroutine(_thinkingProcess);
        }
        
        /// <summary>
        /// 用來執行點餐氣泡的異步協程的類。
        /// </summary>
        /// <returns> 回傳異步協成的資訊回系統。 </returns>
        private IEnumerator ThinkingProcess()
        {
            switch (_bubbleState)
            {
                case State.Thinking:
                {
                    yield return new WaitForSeconds(Random.Range(_waitTimeData.MinThinkTime, _waitTimeData.MaxThinkTime));
                    
                    if (thinkingImage is not null)
                        thinkingImage.gameObject.SetActive(false);
                    
                    _bubbleState = State.Ordering;
                    
                    if (orderImage is not null)
                        orderImage.gameObject.SetActive(true);

                    _button.interactable = true;
                    
                    _startCountdown = true;
                    _targetWaitTime = Random.Range(_waitTimeData.MinWaitOrderTime, _waitTimeData.MaxWaitOrderTime);
                    
                    break;
                }
                case State.Ordering:
                {
                    if (orderImage is not null)
                        orderImage.gameObject.SetActive(false);

                    _bubbleState = State.WaitingForCuisine;
                    
                    if (cuisineImage is not null)
                        cuisineImage.gameObject.SetActive(true);
                    
                    _button.interactable = false;
                    
                    maskImage.fillAmount = 0;
                    _targetWaitTime = Random.Range(_waitTimeData.MinWaitCuisineTime, _waitTimeData.MaxWaitCuisineTime);
                    
                    break;
                }
            }
        }

        /// <summary>
        /// 用來重新執行 ThinkingProcess 這個異步同步的類的方法。
        /// 用於 Button 組件的 On Click() 做使用。
        /// </summary>
        public void OnClick()
        {
            if (_thinkingProcess is not null)
            {
                StopCoroutine(_thinkingProcess);
                _thinkingProcess = null;
            }
            
            _thinkingProcess = ThinkingProcess();
            StartCoroutine(_thinkingProcess);
        }
    }
}
