using System.Collections;
using UnityEngine;

namespace Restaurant.Customer.BubbleState.State
{
    internal class OrderBubble : IBubbleState
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
            if (CurrentCoroutine is not null)
            {
                Manager.StopCoroutine(CurrentCoroutine);
                CurrentCoroutine = null;
            }
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
            if (CurrentCoroutine is not null)
            {
                Manager.StopCoroutine(CurrentCoroutine);
                CurrentCoroutine = null;
            }
            
            CurrentCoroutine = OnClickProcess();
            Manager.StartCoroutine(CurrentCoroutine);
        }

        private IEnumerator OnClickProcess()
        {
            Manager.DestroyBubble();
            yield return new WaitForSeconds(1);
            Manager.ChangeBubbleState(new WaitDishBubble());
        }
        
        private IEnumerator CountDownPatience()
        {
            var selectTime = Manager.Attribute.OrderAttribute.GetRandomOrderTime();
            var remainingTime = selectTime;
            var countDownImage = Manager.CustomerBubble.CurrentBubbleCountDownImage;

            while (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
                countDownImage.fillAmount = remainingTime / selectTime;
                yield return null;
            }
            
            Manager.DestroyBubble();
            yield return new WaitForSeconds(1);
            Manager.ChangeBubbleState(new AngryBubble());
        }
    }
}