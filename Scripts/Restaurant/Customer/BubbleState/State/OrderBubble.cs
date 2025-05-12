using System.Collections;
using UnityEngine;

namespace Restaurant.Customer.BubbleState.State
{
    public class OrderBubble : IBubbleState
    {
        private CustomerManager Manager { get; set; }
        
        private IEnumerator CurrentCoroutine { get; set; }

        
        public void Enter(CustomerManager manager)
        {
            Manager = manager;
            
            manager.InitOrderBubble();
            
            CurrentCoroutine = CountDownPatience();
            Manager.StartCoroutine(CurrentCoroutine);
        }

        public void Exit()
        {
            Manager.StopCoroutine(CurrentCoroutine);
            
            Manager.DestroyBubble();
        }

        public void PlayerEnter()
        {
            Manager.SetBubbleInteractable(true);
        }

        public void PlayerLeave()
        {
            Manager.SetBubbleInteractable(false);
        }

        public void OnClick()
        {
            // TODO
        }

        private IEnumerator CountDownPatience()
        {
            var selectTime = Manager.CustomerAttribute.OrderAttribute.GetRandomOrderTime();
            var remainingTime = selectTime;
            var countDownImage = Manager.CustomerBubble.CurrentBubbleCountDownImage;

            while (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
                countDownImage.fillAmount = remainingTime / selectTime;
                yield return null;
            }
            
            Manager.ChangeBubbleState(new AngryBubble());
        }
    }
}