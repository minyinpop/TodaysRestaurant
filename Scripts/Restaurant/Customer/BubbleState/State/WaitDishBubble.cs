using System.Collections;
using Database.Restaurant.Dish;
using UnityEngine;

namespace Restaurant.Customer.BubbleState.State
{
    internal class WaitDishBubble : IBubbleState
    {
        private CustomerManager Manager { get; set; }
        
        private IEnumerator CurrentCoroutine { get; set; }
        
        private DishSO SelectDish { get; set; }
        
        public void Enter(CustomerManager manager)
        {
            Manager = manager;
            manager.InitWaitDishBubble();
            
            SelectDish = Manager.TryOrderDish();

            if (SelectDish is null)
                Manager.ChangeBubbleState(new HappyBubble());
            else
                Manager.CustomerBubble.CurrentBubbleDishImage.sprite = SelectDish.Sprite;
            
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
            if (Manager.DishDeliver.CheckDishExist())
                Manager.SetBubbleInteractable(true);
        }

        public void PlayerLeave()
        {
            Manager.SetBubbleInteractable(false);
        }

        public void OnClick()
        {
            var takeDish = Manager.TryTakeDish();

            if (takeDish is null)
                return;

            if (CurrentCoroutine is not null)
            {
                Manager.StopCoroutine(CurrentCoroutine);
                CurrentCoroutine = null;
            }
            
            CurrentCoroutine = OnClickProcess(takeDish);
            Manager.StartCoroutine(CurrentCoroutine);
        }
        
        private IEnumerator CountDownPatience()
        {
            var selectTime = SelectDish.CookTime + Manager.Attribute.OrderAttribute.GetRandomWaitDishTime();
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

        private IEnumerator OnClickProcess(DishSO takeDish)
        {
            Manager.DestroyBubble();
            yield return new WaitForSeconds(1);
            
            if (takeDish != Manager.CustomerOrder.CurrentOrderDish)
                Manager.ChangeBubbleState(new AngryBubble());
            else
                Manager.ChangeBubbleState(new ThinkBubble());
        }
    }
}