using System;
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
        }

        private IEnumerator DragCoroutine { get; set; }

        private IEnumerator DragProcess()
        {
            while (true)
            {
                CardMainSystem.DraggedCard.transform.position = CardMainSystem.MousePosition;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}