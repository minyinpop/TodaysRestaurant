using System.Collections;
using UnityEngine;

namespace System.Economy.Child.Customer.Child.Bubble.Type
{
    internal sealed class ThinkBubble : Bubble
    {
        private IEnumerator CountDownCor;
        
        private void OnDisable()
        {
            if (CountDownCor is not null)
            {
                StopCoroutine(CountDownCor);
                CountDownCor = null;
            }
        }
        
        public override void CountDown(float time, Action onComplete)
        {
            CountDownCor = CountDownCoroutine();
            StartCoroutine(CountDownCor);
            return;

            IEnumerator CountDownCoroutine()
            {
                yield return new WaitForSeconds(time);
                onComplete?.Invoke();
            }
        }
    }
}