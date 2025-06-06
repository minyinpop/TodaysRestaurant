using System.Collections;
using System.Collections.Generic;
using BATTLE.CARD.MANAGER;
using UnityEngine;

namespace BATTLE.CARD.SYSTEM
{
    internal class CardSortingSystem : MonoBehaviour, ICardSystemHandler
    {
        [field: Header("Card Slot List")]
        [field: SerializeField] private List<GameObject> Slots { get; set; }
        
        public void BeginDrag(Card draggedCard)
        {
            
        }

        public void EndDrag()
        {
            
        }

        private IEnumerator SortingCoroutine { get; set; }

        private IEnumerator SortingProcess()
        {
            yield return new WaitForEndOfFrame();
        }
    }
}