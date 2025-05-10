using System.Collections;
using UnityEngine;

namespace Restaurant.Kitchenware.StateMachine.BubbleState
{
    public class CookBubble : IBubbleState
    {
        private KitchenwareManager Manager { get; set; }

        private IEnumerator MainCoroutine { get; set; }
        
        public void Enter(KitchenwareManager manager)
        {
            Manager = manager;
            Manager.InitCookBubble();
            
            MainCoroutine = CountDownCookTime();
            Manager.StartCoroutine(MainCoroutine);
        }

        public void Exit()
        {
            Manager.StopCoroutine(MainCoroutine);
            MainCoroutine = null;
            
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

        private IEnumerator CountDownCookTime()
        {
            if (Mathf.Approximately(Manager.RemainingCookTime, Manager.CurrentCookDish.CookTime))
            {
                while (Manager.RemainingCookTime > Manager.CurrentCookDish.CookTime / 2)
                {
                    Manager.RemainingCookTime -= Time.deltaTime;
                    yield return null;
                }
                
                Manager.ChangeState(new GameBubble());
            }
            else
            {
                while (Manager.RemainingCookTime > 0)
                {
                    Manager.RemainingCookTime -= Time.deltaTime;
                    yield return null;
                }

                Manager.ChangeState(new FinishBubble());
            }
        }
    }
}