using System.Collections;
using Database.Restaurant.Dish;
using Restaurant.Kitchenware.Bubble;
using UnityEngine;

namespace Restaurant.Kitchenware
{
    [RequireComponent(typeof(KitchenwareDetector))]
    [RequireComponent(typeof(KitchenwareCookMenu))]
    [RequireComponent(typeof(KitchenwareGame))]
    public class KitchenwareManager : MonoBehaviour
    {
        [field: Header("氣泡的生成位置")]
        [field: SerializeField] private Transform BubbleParent { get; set; }
        
        [field: Header("各類型氣泡的預製件")]
        [field: SerializeField] private GameObject EmptyBubblePrefab { get; set; }
        [field: SerializeField] private GameObject CookBubblePrefab { get; set; }
        [field: SerializeField] private GameObject BurnBubblePrefab { get; set; }
        private GameObject CurrentBubbleObj { get; set; }
        public BubbleBase CurrentBubbleBase { get; private set; }
        
        private IEnumerator MainCoroutine { get; set; }
        private IEnumerator SideCoroutine { get; set; }

        private KitchenwareCookMenu KitchenwareCookMenu { get; set; }
        private bool IsClickBubble { get; set; }
        private DishSO CurrentCookDish { get; set; }

        private void Awake() => KitchenwareCookMenu = GetComponent<KitchenwareCookMenu>();
        
        private void OnEnable()
        {
            MainCoroutine = MainProcess();
            StartCoroutine(MainCoroutine);
        }

        private void OnDestroy()
        {
            if (CurrentBubbleBase is not null)
                CurrentBubbleBase.OnClickEvent -= OnClickBubble;
            
            if (MainCoroutine is not null)
                StopCoroutine(MainCoroutine);
            
            if (SideCoroutine is not null)
                StopCoroutine(SideCoroutine);
        }

        private IEnumerator MainProcess()
        {
            SideCoroutine = EmptyProcess();
            yield return SideCoroutine;
        }

        private IEnumerator EmptyProcess()
        {
            CurrentBubbleObj = Instantiate(EmptyBubblePrefab, BubbleParent);
            CurrentBubbleBase = CurrentBubbleObj.GetComponent<BubbleBase>();
            CurrentBubbleBase.OnClickEvent += OnClickBubble;

            while (CurrentCookDish is null)
            {
                yield return new WaitUntil(() => IsClickBubble || CurrentCookDish is not null);
                IsClickBubble = false;

                if (CurrentCookDish is not null)
                    break;
                
                KitchenwareCookMenu.OpenCookMenu();
            }
        }

        private void OnClickBubble() => IsClickBubble = true;
    }
}