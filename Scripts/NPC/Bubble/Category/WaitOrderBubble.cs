using DataBase.Customer.Wait;
using NPC.Customer;
using UnityEngine;
using UnityEngine.UI;

namespace NPC.Bubble.Category
{
    public class WaitOrderBubble : CharacterBubble
    {
        [Header("組件"), Tooltip("用於顯示耐心剩下多少的遮罩的圖片組件。"), SerializeField]
        private Image maskImage;
        
        //
        private CustomerOrderBubble _customerOrderBubble;
        
        //
        private float _targetWaitTime;

        private void Update()
        {
            maskImage.fillAmount = Time.deltaTime / _targetWaitTime;
        }
        
        /// <summary>
        /// 用於 Button 組件的 On Click() 的方法。
        /// </summary>
        public void OnClick()
        {}

        /// <summary>
        /// 用於初始化氣泡的方法。
        /// </summary>
        /// <param name="targetWaitTime"> 新傳入的等待時間。 </param>
        /// <param name="newCustomerOrderBubble"> 新傳入的 CustomerOrderBubble 的組件，用於回傳資訊用。 </param>
        public void InitBubble(float targetWaitTime, CustomerOrderBubble newCustomerOrderBubble)
        {
            _customerOrderBubble = newCustomerOrderBubble;
            _targetWaitTime = targetWaitTime;
        }
    }
}
