using System.Collections;
using BATTLE.CARD.MANAGER;
using UnityEngine;

namespace BATTLE.CARD.SYSTEM
{
    internal class CardDragSystem : MonoBehaviour, ICardSystemHandler
    {
        [field: Header("Main Component")]
        [field: SerializeField] private CardMainSystem CardMainSystem { get; set; }
        
        private void OnDisable()
        {
            if (DragCoroutine is null) return;
            StopCoroutine(DragCoroutine);
            DragCoroutine = null;
        }

        private IEnumerator DragCoroutine { get; set; }

        public void BeginDrag(Card draggedCard)
        {
            DragCoroutine = DragProcess(draggedCard.gameObject);
            StartCoroutine(DragCoroutine);
        }
        
        public void EndDrag()
        {
            if (DragCoroutine is null) return;
            StopCoroutine(DragCoroutine);
            DragCoroutine = null;
        }

        private IEnumerator DragProcess(GameObject draggedCard)
        {
            while (true)
            {
                draggedCard.transform.position = CardMainSystem.MousePosition;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}