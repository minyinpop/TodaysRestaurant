using System.Collections;
using Database.Restaurant.Dish;
using UnityEngine;
using UnityEngine.UI;

namespace Restaurant.Kitchenware.Bubble.Category
{
    public class KitchenwareCookBubble : BubbleBase
    {
        [field: Header("自身組件")]
        [field: SerializeField] private Button Button { get; set; }
        [field: SerializeField] private Image PlayImage { get; set; }
        
        private IEnumerator MainCoroutine { get; set; }
        private IEnumerator SideCoroutine { get; set; }
        
        private KitchenwareManager KitchenwareManager { get; set; }
        private DishSO CurrentCookDish { get; set; }
        
        private bool CanPlay { get; set; }
        
        private void OnEnable() => Button.onClick.AddListener(OnClick);
        
        private void OnDisable()
        {
            Button.onClick.RemoveListener(OnClick);
            
            if (MainCoroutine is not null)
            {
                StopCoroutine(MainCoroutine);
                MainCoroutine = null;
            }
            
            if (SideCoroutine is not null)
            {
                StopCoroutine(SideCoroutine);
                SideCoroutine = null;
            }
        }

        public override void OnClick()
        {
            if (!CanPlay)
                return;
        }
        
        public override void PlayerEnter()
        {
            if (CanPlay)
                Button.interactable = true;
        }
        
        public override void PlayerLeave() => Button.interactable = false;

        public override void Init(KitchenwareManager manager, DishSO currentCookDish)
        {
            KitchenwareManager = manager;
            CurrentCookDish = currentCookDish;
            
            MainCoroutine = MainProcess();
            StartCoroutine(MainCoroutine);
        }

        private IEnumerator MainProcess()
        {
            var currentTime = CurrentCookDish.CookTime;
            
            while (currentTime > CurrentCookDish.CookTime / 2)
            {
                currentTime -= Time.deltaTime;
                yield return null;
            }

            CanPlay = true;
            PlayImage.gameObject.SetActive(true);
            
            SideCoroutine = BurnCountDownProcess();
            yield return SideCoroutine;
        }

        private IEnumerator BurnCountDownProcess()
        {
            var currentTime = CurrentCookDish.BurnTime;

            while (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                yield return null;
            }
            
            KitchenwareManager.DishBurn();
        }
    }
}