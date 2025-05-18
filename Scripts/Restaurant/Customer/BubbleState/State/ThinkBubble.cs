using System.Collections;
using UnityEngine;

namespace Restaurant.Customer.BubbleState.State
{
    internal class ThinkBubble : IBubbleState
    {
        private CustomerManager Manager { get; set; }
        
        private IEnumerator CurrentCoroutine { get; set; }
        
        public void Enter(CustomerManager manager)
        {
            Manager = manager;
            
            Manager.InitThinkBubble();
            
            CurrentCoroutine = CountDownDisplay();
            Manager.StartCoroutine(CurrentCoroutine);
        }

        public void Exit()
        {
            Manager.StopCoroutine(CurrentCoroutine);
            Manager.DestroyBubble();
        }

        public void PlayerEnter()
        {
            
        }

        public void PlayerLeave()
        {
            
        }

        public void OnClick()
        {
            
        }

        private IEnumerator CountDownDisplay()
        {
            var countDownTime = Manager.Attribute.OrderAttribute.GetRandomThinkTime();
            
            while (countDownTime > 0)
            {
                countDownTime -= Time.deltaTime;
                yield return null;
            }
            
            yield return new WaitForSeconds(1);
            Manager.ChangeBubbleState(new OrderBubble());
        }
    }
}