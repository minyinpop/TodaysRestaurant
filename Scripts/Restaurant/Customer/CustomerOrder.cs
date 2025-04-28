using System.Collections;
using Database.Restaurant.Customer.Attribute;
using UnityEngine;
using UnityEngine.UI;

namespace Restaurant.Customer
{
    public class CustomerOrder : MonoBehaviour
    {
        [field: Header("屬性資料")]
        [field: SerializeField] private CustomerAttributeSO Attribute { get; set; }
        
        [field: Header("氣泡的生成位置")]
        [field: SerializeField] private Transform BubbleParent { get; set; }
        
        [field: Header("各類型氣泡的預製件")]
        [field: SerializeField] private GameObject ThinkBubble { get; set; }
        [field: SerializeField] private GameObject OrderBubble { get; set; }
        [field: SerializeField] private GameObject HappyBubble { get; set; }
        [field: SerializeField] private GameObject AngryBubble { get; set; }
        
        private CustomerController CustomerController { get; set; }
        
        private IEnumerator OrderCoroutine { get; set; }
        private IEnumerator CountDownPatienceCoroutine { get; set; }

        private GameObject Bubble { get; set; }
        private bool IsBubbleClick { get; set; }

        private void Awake()
        {
            CustomerController = GetComponent<CustomerController>();
        }

        private void OnEnable()
        {
            CustomerController.OnSeat += StartOrder;
        }
        
        private void OnDisable()
        {
            CustomerController.OnSeat -= StartOrder;
            
            if (OrderCoroutine is not null)
            {
                StopCoroutine(OrderCoroutine);
                OrderCoroutine = null;
            }
            
            if (CountDownPatienceCoroutine is not null)
            {
                StopCoroutine(CountDownPatienceCoroutine);
                CountDownPatienceCoroutine = null;
            }
        }

        private void StartOrder()
        {
            OrderCoroutine = OrderProcess();
            StartCoroutine(OrderCoroutine);
        }
        
        private IEnumerator OrderProcess()
        {
            Bubble = Instantiate(ThinkBubble, BubbleParent);
            yield return new WaitForSeconds(Attribute.OrderAttribute.GetRandomThinkTime());
            
            Destroy(Bubble);
            yield return new WaitForSeconds(1f);

            Bubble = Instantiate(OrderBubble, BubbleParent);
            StartCountDownPatience();
            Bubble.GetComponent<Button>().onClick.AddListener(OnBubbleClick);
            yield return new WaitUntil(() => IsBubbleClick);

            IsBubbleClick = false;
            Destroy(Bubble);
            yield return new WaitForSeconds(1f);
            
            // TODO 生成想要的餐點
        }

        private void OnBubbleClick()
        {
            StopCoroutine(CountDownPatienceCoroutine);
            CountDownPatienceCoroutine = null;
            
            IsBubbleClick = true;
        }
        
        private void StartCountDownPatience()
        {
            CountDownPatienceCoroutine = CountDownPatienceProcess();
            StartCoroutine(CountDownPatienceCoroutine);
        }

        private IEnumerator CountDownPatienceProcess()
        {
            var image = Bubble.GetComponent<Image>();
            var targetTime = Attribute.OrderAttribute.GetRandomOrderTime();
            var currentTime = targetTime;

            while (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                image.fillAmount = currentTime / targetTime;
                yield return null;
            }

            image.fillAmount = 0;
            Debug.Log("顧客沒耐心了！");
        }

        private void NoPatience()
        {
            // TODO 顧客不開心 AngryBubble
        }
    }
}