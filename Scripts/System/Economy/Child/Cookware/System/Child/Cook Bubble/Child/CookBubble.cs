using System.Collections;
using System.Economy.Child.Cookware.System.Child.Cook_Bubble.Main;
using UnityEngine;

namespace System.Economy.Child.Cookware.System.Child.Cook_Bubble.Child
{
    internal sealed class CookBubble : Bubble
    {
        private void OnDisable()
        {
            if (CoutDownCor is not null)
            {
                StopCoroutine(CoutDownCor);
                CoutDownCor = null;
            }
        }
        
        private IEnumerator CoutDownCor;
        public override void CoutDown(float time, Action onComplete)
        {
            CoutDownCor = CoutDownCoroutine();
            StartCoroutine(CoutDownCor);
            return;

            IEnumerator CoutDownCoroutine()
            {
                yield return new WaitForSeconds(time);
                onComplete?.Invoke();
            }
        }
    }
}