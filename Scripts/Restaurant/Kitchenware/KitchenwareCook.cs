using System.Collections;
using Database.Restaurant.Dish;
using Restaurant.Kitchenware.Bubble;
using UnityEngine;

namespace Restaurant.Kitchenware
{
    public class KitchenwareCook : MonoBehaviour
    {
        [field: Header("氣泡的生成位置")]
        [field: SerializeField] private Transform BubbleParent { get; set; }
        
        [field: Header("氣泡的預製件")]
        [field: SerializeField] private GameObject EmptyBubblePrefab { get; set; }
        
        private KitchenwareManager KitchenwareManager { get; set; }
        
        private GameObject CurrentBubble { get; set; }
        private BubbleBase CurrentBubbleScript { get; set; }
        private bool IsBubbleFinish { get; set; }
        
        private IEnumerator MainCoroutine { get; set; }
        private IEnumerator CurrentCoroutine { get; set; }
        
        private DishSO CurrentCookDish { get; set; }

        private void Awake() => KitchenwareManager = GetComponent<KitchenwareManager>();
        
        private void Start()
        {
            MainCoroutine = MainProcess();
            StartCoroutine(MainCoroutine);
        }

        private void OnDisable()
        {
            if (MainCoroutine is not null)
            {
                StopCoroutine(MainCoroutine);
                MainCoroutine = null;
            }
            
            if (CurrentCoroutine is not null)
            {
                StopCoroutine(CurrentCoroutine);
                CurrentCoroutine = null;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;

            CurrentBubbleScript.PlayerEnter();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;

            CurrentBubbleScript.PlayerLeave();
        }

        public void StartCook(DishSO selectDish) => CurrentCookDish = selectDish;

        private IEnumerator MainProcess()
        {
            while (true)
            {
                Debug.Log("Start Main Process");

                CurrentCoroutine = EmptyProcess();
                yield return CurrentCoroutine;

                Debug.Log("Start Cook Process");
                
                // TODO 開始撰寫烹飪料理的邏輯

                CurrentCoroutine = CookProcess();
                yield return CurrentCoroutine;
            }
        }

        private IEnumerator EmptyProcess()
        {
            CurrentBubble = Instantiate(EmptyBubblePrefab, BubbleParent);
            CurrentBubbleScript = CurrentBubble.GetComponent<BubbleBase>();
            CurrentBubbleScript.Init(KitchenwareManager);
            
            yield return new WaitUntil(() => CurrentCookDish is not null);
            Destroy(CurrentBubble);

            CurrentBubble = null;
            CurrentBubbleScript = null;
        }

        private IEnumerator CookProcess()
        {
            var currentTime = CurrentCookDish.CookTime;
            
            while (currentTime > CurrentCookDish.CookTime / 2)
            {
                currentTime -= Time.deltaTime;
                yield return null;
            }
            
            yield return new WaitUntil(() => IsBubbleFinish);
        }
        
        public void OnBubbleFinish() => IsBubbleFinish = true;
    }
}