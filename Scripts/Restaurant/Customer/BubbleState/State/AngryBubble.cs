using System.Collections;
using Restaurant.Customer.CustomerState.State;
using UnityEngine;

namespace Restaurant.Customer.BubbleState.State
{
    public class AngryBubble : IBubbleState
    {
        private CustomerManager Manager { get; set; }
        
        private IEnumerator CurrentCoroutine { get; set; }

        
        public void Enter(CustomerManager manager)
        {
            Manager = manager;
            
            Manager.InitAngryBubble();
            
            CurrentCoroutine = CountDownDisplay();
            Manager.StartCoroutine(CurrentCoroutine);
        }

        public void Exit()
        {
            Manager.DestroyBubble();
            
            if (CurrentCoroutine is not null)
                Manager.StopCoroutine(CurrentCoroutine);
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
            
            Manager.ChangeCustomerState(new ExitState());
            Manager.ExitBubbleState();
        }
    }
}