using NPC.Customer;
using UnityEngine;
using UnityEngine.UI;

namespace NPC.Bubble.Order.Category
{
    /// <summary>
    /// 用於管理等待服務員的氣泡的類。
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class WaitOrderBubble : OrderBubble
    {
        [Header("組件"), Tooltip("用於顯示耐心剩下多少的遮罩的圖片組件。"), SerializeField]
        private Image maskImage;
        
        // 顧客的 CustomerOrder 組件，用於回傳氣泡的狀況使用。
        private CustomerOrder _customerOrder;
        
        // 顧客有多久的耐心可以等待。
        private float _targetWaitTime;
        
        // 自身的 Button 組件，用於切換是否可以點擊氣泡。
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }
        
        private void Update()
        {
            maskImage.fillAmount += Time.deltaTime / _targetWaitTime;

            // 判斷顧客在等待服務員時，有沒有等待太久而失去耐心。
            if (maskImage.fillAmount >= 1)
            {
                // TODO: 顧客失去耐心。
            }
        }

        /// <summary>
        /// 用於 Button 組件的 On Click() 的方法。
        /// </summary>
        public void OnClick()
        {
            _customerOrder.OnStateChange(CustomerOrder.State.WaitingCuisine);
        }

        /// <summary>
        /// 用於初始化氣泡的方法。
        /// </summary>
        /// <param name="newCustomerOrder"> 新傳入的 CustomerOrderBubble 的組件，用於回傳資訊用。 </param>
        /// <param name="targetWaitTime"> 新傳入的等待時間。 </param>
        public void InitBubble(CustomerOrder newCustomerOrder, float targetWaitTime)
        {
            _customerOrder = newCustomerOrder;
            _targetWaitTime = targetWaitTime;
        }
        
        /// <summary>
        /// 用於切換按鈕是否可以互動的類。
        /// </summary>
        /// <param name="interactable"> 是否可以互動。 </param>
        public override void ChangeButtonInteractable(bool interactable)
        {
            _button.interactable = interactable;
        }
    }
}
