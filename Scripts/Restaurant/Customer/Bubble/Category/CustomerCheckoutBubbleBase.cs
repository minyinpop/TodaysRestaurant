using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Restaurant.Customer.Bubble.Category
{
    public class CustomerCheckoutBubbleBase : BubbleBase
    {
        [field: Header("自身的組件")]
        [field: SerializeField] private Button Button { get; set; }
        [field: SerializeField] private Image PatienceImage { get; set; }
        
        [field: Header("耐心顏色變化值")]
        [field: SerializeField] private Color FullColor { get; set; }
        [field: SerializeField] private Color EmptyColor { get; set; }
        
        private IEnumerator CountDownPatienceCoroutine { get; set; }

        private void OnEnable()
        {
            Button.onClick.AddListener(OnClickBubble);
        }

        private void OnDisable()
        {
            Button.onClick.RemoveListener(OnClickBubble);
            
            if (CountDownPatienceCoroutine is not null)
            {
                StopCoroutine(CountDownPatienceCoroutine);
                CountDownPatienceCoroutine = null;
            }
        }

        public override void Init(float time)
        {
            CountDownPatienceCoroutine = CountDownPatienceProcess(time);
            StartCoroutine(CountDownPatienceCoroutine);
        }

        private IEnumerator CountDownPatienceProcess(float time)
        {
            var totalTime = time;

            while (totalTime > 0)
            {
                totalTime -= Time.deltaTime;
                PatienceImage.fillAmount = totalTime / time;
                PatienceImage.color = Color.Lerp(EmptyColor, FullColor, totalTime / time);
                yield return null;
            }
            
            Destroy(gameObject);
            OnCustomerHaveNoPatience();
        }
    }
}