using System.Collections;
using Restaurant.Customer.CustomerState.State;
using UnityEngine;

namespace Restaurant.Customer.BubbleState.State
{
    internal class HappyBubble : IBubbleState
    {
        private CustomerManager Manager { get; set; }
        
        private IEnumerator CurrentCoroutine { get; set; }
        
        public void Enter(CustomerManager manager)
        {
            Manager = manager;
            
            Manager.InitHappyBubble();
            
            CurrentCoroutine = CountDownDisplay();
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
            
        }

        public void PlayerLeave()
        {
            
        }

        public void OnClick()
        {
            
        }
        
        private IEnumerator CountDownDisplay()
        {
            var remainingTime = 3f;
            
            while (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
                yield return null;
            }
            
            Manager.DestroyBubble();
            yield return new WaitForSeconds(1);
            Manager.SetCustomerState(new AngryToLeave());
            Manager.ExitBubbleState();
        }
    }
}