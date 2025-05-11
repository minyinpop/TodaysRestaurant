using System.Collections;

namespace Restaurant.Customer.BubbleState.State
{
    public class ThinkBubble : IBubbleState
    {
        private CustomerManager Manager { get; set; }
        
        private IEnumerator CurrentCoroutine { get; set; }
        
        public void Enter(CustomerManager manager)
        {
            Manager = manager;
            
            Manager.InitThinkBubble();
            
            CurrentCoroutine = CountDownPatience();
            Manager.StartCoroutine(CurrentCoroutine);
        }

        public void Exit()
        {
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

        private IEnumerator CountDownPatience()
        {
            yield return null;
        }
    }
}