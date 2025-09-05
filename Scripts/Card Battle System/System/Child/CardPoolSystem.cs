using System.Collections;
using Card_Battle_System.Object;
using UnityEngine;

namespace Card_Battle_System.System.Child
{
    internal sealed class CardPoolSystem : MonoBehaviour
    {
        [field: Header("")]
        [field: SerializeField] private CardSlot[] CardSlots;
        
        private IEnumerator SortCor;
        private IEnumerator RefillCor;

        private void OnDisable()
        {
            if (SortCor is not null)
            {
                StopCoroutine(SortCor);
                SortCor = null;
            }

            if (RefillCor is not null)
            {
                StopCoroutine(RefillCor);
                RefillCor = null;
            }
        }

        public void Refill()
        {
            SortCor = SortCoroutine();
            RefillCor = RefillCoroutine();
            
            StartCoroutine(RefillCor);
        }

        private IEnumerator SortCoroutine()
        {
            var remainingCards = new GameObject[] { };
            yield break;
        }
        
        private IEnumerator RefillCoroutine()
        {
            yield break;
        }
    }
}