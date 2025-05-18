using System.Collections;
using UnityEngine;

namespace Restaurant.Kitchenware.BubbleState.State
{
    internal class GameBubble : IBubbleState
    {
        private KitchenwareManager Manager { get; set; }
        
        private IEnumerator MainCoroutine { get; set; }
        
        public void Enter(KitchenwareManager manager)
        {
            Manager = manager;
            Manager.InitGameBubble();
            
            MainCoroutine = CountDownCookTime();
            Manager.StartCoroutine(MainCoroutine);
        }

        public void Exit()
        {
            Manager.StopCoroutine(MainCoroutine);
            MainCoroutine = null;
            
            Manager.OnGameCancel();
            Manager.DestroyBubble();
        }

        public void PlayerEnter()
        {
            Manager.SetBubbleInteractable(true);
        }

        public void PlayerLeave()
        {
            Manager.SetBubbleInteractable(false);
            Manager.OnGameCancel();
        }

        public void OnClick()
        {
            Manager.OnGameStart();
        }
        
        private IEnumerator CountDownCookTime()
        {
            var currentTime = Manager.CurrentCookDish.BurnTime;
            
            while (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                Manager.CurrentBubbleCountDownImage.fillAmount = currentTime / Manager.CurrentCookDish.BurnTime;
                yield return null;
            }

            Manager.ChangeState(new BurnBubble());
        }
    }
}