using System.Collections;
using BATTLE.CARD.MANAGER;
using UnityEngine;

namespace BATTLE.CARD.SYSTEM
{
    internal class CardSortingSystem : MonoBehaviour, ICardSystemHandler
    {
        [field: Header("Main Component")]
        [field: SerializeField] private CardMainSystem CardMainSystem { get; set; }

        private bool IsFirstCard { get; set; }
        private bool IsLastCard { get; set; }
        
        private void OnDisable()
        {
            if (DragCoroutine is null) return;
            StopCoroutine(DragCoroutine);
            DragCoroutine = null;
        }

        public void BeginDrag(Card draggedCard)
        {
            DragCoroutine = DragProcess();
            StartCoroutine(DragCoroutine);
        }
        
        public void Drag()
        {
            
        }

        public void EndDrag()
        {
            if (DragCoroutine is null) return;
            StopCoroutine(DragCoroutine);
            DragCoroutine = null;
            
            IsFirstCard = false;
            IsLastCard = false;
        }
        
        private IEnumerator DragCoroutine { get; set; }

        private IEnumerator DragProcess()
        {
            while (true)
            {
                var draggedCard = CardMainSystem.DraggedCard;
                var draggedCardSlotIndex = CardMainSystem.Slots.IndexOf(draggedCard.transform.parent.gameObject);
                if (draggedCardSlotIndex == 0)
                    IsFirstCard = true;
                if (draggedCardSlotIndex == CardMainSystem.Slots.Count - 1)
                    IsLastCard = true;
                var draggedCardPosX = draggedCard.transform.position.x;

                if (!IsFirstCard)
                {
                    var leftCardPosX = CardMainSystem.Slots[draggedCardSlotIndex - 1].transform.position.x;
                    if (draggedCardPosX < leftCardPosX)
                    {
                        (CardMainSystem.Slots[draggedCardSlotIndex], CardMainSystem.Slots[draggedCardSlotIndex - 1]) =
                            (CardMainSystem.Slots[draggedCardSlotIndex - 1],
                                CardMainSystem.Slots[draggedCardSlotIndex]);
                    }
                }

                if (!IsLastCard)
                {
                    var rightCardPosX = CardMainSystem.Slots[draggedCardSlotIndex + 1].transform.position.x;
                    if (draggedCardPosX > rightCardPosX)
                    {
                        (CardMainSystem.Slots[draggedCardSlotIndex], CardMainSystem.Slots[draggedCardSlotIndex + 1]) =
                            (CardMainSystem.Slots[draggedCardSlotIndex + 1],
                                CardMainSystem.Slots[draggedCardSlotIndex]);
                    }
                }

                for (var i = 0; i < CardMainSystem.Slots.Count; i++)
                    CardMainSystem.Slots[i].transform.SetSiblingIndex(i);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}