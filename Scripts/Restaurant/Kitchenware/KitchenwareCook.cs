using System.Collections;
using Database.Restaurant.Dish;
using Restaurant.Kitchenware.Bubble;
using Restaurant.Kitchenware.Bubble.Category;
using UnityEngine;

namespace Restaurant.Kitchenware
{
    public class KitchenwareCook : MonoBehaviour
    {
        [field: Header("氣泡的生成位置")]
        [field: SerializeField] private Transform BubbleParent { get; set; }
        
        [field: Header("氣泡的預製件")]
        [field: SerializeField] private GameObject EmptyBubblePrefab { get; set; }
        [field: SerializeField] private GameObject CookBubblePrefab { get; set; }
        [field: SerializeField] private GameObject BurnBubblePrefab { get; set; }
        
        private KitchenwareManager KitchenwareManager { get; set; }
        
        private GameObject CurrentBubble { get; set; }
        private BubbleBase CurrentBubbleScript { get; set; }
        private bool ClickBubble { get; set; }
        
        private IEnumerator MainCoroutine { get; set; }
        private IEnumerator SideCoroutine { get; set; }
        
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
            
            if (SideCoroutine is not null)
            {
                StopCoroutine(SideCoroutine);
                SideCoroutine = null;
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
        
        public void DishBurn()
        {
            StopCoroutine(MainCoroutine);
            MainCoroutine = null;

            StopCoroutine(SideCoroutine);
            SideCoroutine = null;
            
            Destroy(CurrentBubble);

            CurrentBubble = null;
            CurrentBubbleScript = null;
            
            SideCoroutine = BurnProcess();
            StartCoroutine(SideCoroutine);
        }
        
        private IEnumerator MainProcess()
        {
            SideCoroutine = EmptyProcess();
            yield return SideCoroutine;

            SideCoroutine = CookProcess();
            yield return SideCoroutine;
        }

        private IEnumerator EmptyProcess()
        {
            SpawnBubble(EmptyBubblePrefab);
            CurrentBubbleScript.Init(KitchenwareManager);
            
            yield return new WaitUntil(() => CurrentCookDish is not null);
            Destroy(CurrentBubble);

            CurrentBubble = null;
            CurrentBubbleScript = null;
        }

        private IEnumerator CookProcess()
        {
            SpawnBubble(CookBubblePrefab);
            CurrentBubbleScript.Init(KitchenwareManager, CurrentCookDish);
            
            yield return new WaitUntil(() => CurrentCookDish is null);
            // TODO 玩家拿取烹飪完成的料理後，重新執行 MainProcess
        }

        private IEnumerator BurnProcess()
        {
            SpawnBubble(BurnBubblePrefab);
            CurrentBubbleScript.Init(this);
            
            yield return new WaitUntil(() => ClickBubble);
            Destroy(CurrentBubble);
        }
        
        public void OnClickBubble() => ClickBubble = true;

        private void SpawnBubble(GameObject bubble)
        {
            CurrentBubble = Instantiate(bubble, BubbleParent);
            CurrentBubbleScript = CurrentBubble.GetComponent<BubbleBase>();
        }
    }
}