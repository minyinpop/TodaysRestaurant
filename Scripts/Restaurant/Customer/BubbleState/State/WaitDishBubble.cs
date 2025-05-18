using System.Collections;
using Database.Restaurant.Dish;
using UnityEngine;

namespace Restaurant.Customer.BubbleState.State
{
    public class WaitDishBubble : IBubbleState
    {
        private CustomerManager Manager { get; set; }
        
        private IEnumerator CurrentCoroutine { get; set; }
        
        private DishSO SelectDish { get; set; }
        
        public void Enter(CustomerManager manager)
        {
            Manager = manager;
            
            manager.InitWaitDishBubble();
            
            Manager.GetDish(out var selectDish);
            SelectDish = selectDish;
            
            Debug.Log(SelectDish);
            // BUG: SelectDish 是 null，檢查 out property
            
            CurrentCoroutine = CountDownPatience();
            Manager.StartCoroutine(CurrentCoroutine);
        }

        public void Exit()
        {
            // TODO
        }

        public void PlayerEnter()
        {
            // TODO
        }

        public void PlayerLeave()
        {
            // TODO
        }

        public void OnClick()
        {
            // TODO
        }
        
        private IEnumerator CountDownPatience()
        {
            var selectTime = Manager.Attribute.OrderAttribute.GetRandomWaitDishTime();
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