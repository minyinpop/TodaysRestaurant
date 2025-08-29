using System;
using System.Collections;
using System.Collections.Generic;
using Animation_Settings;
using Battle.Card_Slot;
using Battle.Card.Base;
using Player.Data.Equip_Deck;
using UnityEngine;

namespace Battle.Card_Pool
{
    internal sealed class CardPoolSystem : MonoBehaviour
    {
        [field: Header("Card")]
        [field: SerializeField] private EquipDeckSO EquipDeckData;
        [field: SerializeField] private RectTransform CardSpawnParent;
        
        [field: Header("Card Slot")]
        [field: SerializeField] private List<CardSlotSystem> CardSlots;
        
        private IEnumerator RefillCoroutine;
        private IEnumerator SpawnCoroutine;

        private void OnDisable()
        {
            if (RefillCoroutine is not null)
            {
                StopCoroutine(RefillCoroutine);
                RefillCoroutine = null;
            }

            if (SpawnCoroutine is not null)
            {
                StopCoroutine(SpawnCoroutine);
                SpawnCoroutine = null;
            }
        }
        
        public void Refill(Action OnComplete = null)
        {
            RefillCoroutine = RefillProcess();
            SpawnCoroutine = SpawnProcess(OnComplete);
            
            StartCoroutine(RefillCoroutine);
        }

        private IEnumerator RefillProcess()
        {
            List<GameObject> remainingCards = new();
            
            foreach (CardSlotSystem slot in CardSlots)
            {
                if (slot.IsEmpty()) continue;
                
                slot.GetCard(out GameObject card);
                remainingCards.Add(card);
            }

            for (int i = 0; i < remainingCards.Count; i++)
            {
                CardSlotSystem currentSlot = CardSlots[i];
                if (!currentSlot.IsEmpty()) continue;
                
                GameObject currentCard = remainingCards[i];
                currentSlot.AddCard(remainingCards[i]);
                
                DOAnchorPosSettings settings = new(Vector2.zero, .1f, true);
                int remainingCardIndex = i;
                
                currentCard.GetComponent<ICard>().MoveToParent(currentSlot.transform, settings, () =>
                {
                    if (remainingCardIndex == remainingCards.Count - 1)
                    {
                        StartCoroutine(SpawnCoroutine);
                        RefillCoroutine = null;
                    }
                });
                
                yield return new WaitForSeconds(.25f);
            }
        }
        
        private IEnumerator SpawnProcess(Action OnComplete = null)
        {
            // TODO Continue Coding About Spawn Card.
            yield return null;
        }
    }
}