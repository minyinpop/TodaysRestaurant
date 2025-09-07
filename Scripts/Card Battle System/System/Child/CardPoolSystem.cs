using System;
using System.Collections;
using System.Collections.Generic;
using Card_Battle_System.Object.Card_Slot;
using Card_Battle_System.Object.Card.Base;
using Data.DOTween;
using DG.Tweening;
using UnityEngine;

namespace Card_Battle_System.System.Child
{
    internal sealed class CardPoolSystem : MonoBehaviour
    {
        [field: Header("Card Slot")]
        [field: SerializeField] private CardSlot[] CardSlots;
        
        private IEnumerator SortCor;
        private IEnumerator RefillCor;

        private const float RefillDuration = .25f;

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
        
        public void Refill(Action onComplete = null)
        {
            SortCor = SortCoroutine();
            RefillCor = RefillCoroutine(onComplete);
            
            StartCoroutine(SortCor);
        }

        private IEnumerator SortCoroutine()
        {
            var remainingCards = new List<ICard>();

            foreach (var slot in CardSlots)
            {
                if (!slot.Get(out var card)) continue;
                remainingCards.Add(card);
            }

            for (var i = 0; i < remainingCards.Count; i++)
            {
                var index = i;
                var slot = CardSlots[index];
                var card = remainingCards[index];

                card.MoveToParent(slot.transform, new DoAnchorPos(slot.transform.position, .5f, true, Ease.OutExpo),
                    () =>
                    {
                        if (index != remainingCards.Count - 1) return;
                        StartCoroutine(RefillCoroutine());
                        SortCor = null;
                    });
                
                yield return new WaitForSeconds(RefillDuration);
            }
        }
        
        private IEnumerator RefillCoroutine(Action onComplete = null)
        {
            yield break;
        }
    }
}