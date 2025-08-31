using System;
using System.Collections;
using System.Collections.Generic;
using Battle_System.Object.Card_Slot;
using Battle_System.Object.Card.Base;
using Data.DOTween_Values;
using DG.Tweening;
using Player.Data.Battle_Card_Deck;
using UnityEngine;

namespace Battle_System.Child_System
{
    internal sealed class CardPoolSystem : MonoBehaviour
    {
        [field: SerializeField] private Transform SpawnParent;
        [field: SerializeField] private BattleCardDeckSO BattleCardDeckSO;
        [field: SerializeField] private List<CardSlot> CardSlots;
        
        private IEnumerator SortCoroutine;
        private IEnumerator RefillCoroutine;

        private void OnDisable()
        {
            if (SortCoroutine is not null)
            {
                StopCoroutine(SortCoroutine);
                SortCoroutine = null;
            }

            if (RefillCoroutine is not null)
            {
                StopCoroutine(RefillCoroutine);
                RefillCoroutine = null;
            }
        }

        #region Refill
            public void Refill(Action OnComplete = null)
            {
                SortCoroutine = SortProcess();
                RefillCoroutine = RefillProcess(OnComplete);
                
                StartCoroutine(SortCoroutine);
            }

            private IEnumerator SortProcess()
            {
                List<GameObject> RemainingCards = new List<GameObject>();
                
                foreach (CardSlot Slot in CardSlots)
                {
                    if (Slot.IsEmpty()) continue;
                    
                    Slot.Get(out GameObject OutCard);
                    RemainingCards.Add(OutCard);
                }

                if (RemainingCards.Count > 0)
                {
                    for (int i = 0; i < RemainingCards.Count; i++)
                    {
                        CardSlot Slot = CardSlots[i];
                        GameObject Card = RemainingCards[i];
                        int RemainingCardIndex = i;
                        
                        Slot.Add(Card);
                        Card.GetComponent<ICard>().MoveToParent(Slot.transform, new AnchorPosValue(Vector2.zero, .5f, true, Ease.OutQuart), () =>
                        {
                            if (RemainingCardIndex != RemainingCards.Count - 1) return;

                            StartCoroutine(RefillCoroutine);
                            RefillCoroutine = null;
                        });
                        
                        yield return new WaitForSeconds(.5f);
                    }
                }
                else if (RemainingCards.Count == 0)
                {
                    StartCoroutine(RefillCoroutine);
                    RefillCoroutine = null;
                }
            }

            private IEnumerator RefillProcess(Action OnComplete = null)
            {
                for (int i = 0; i < CardSlots.Count; i++)
                {
                    CardSlot Slot = CardSlots[i];

                    if (!Slot.IsEmpty()) continue;
                    
                    BattleCardDeckSO.GetRandomCard(out GameObject CardPrefab);
                    GameObject Card = Instantiate(CardPrefab, SpawnParent);
                    int CardSlotIndex = i;
                    
                    Slot.Add(Card);
                    Card.GetComponent<ICard>().MoveToParent(Slot.transform, new AnchorPosValue(Vector2.zero, .5f, true, Ease.OutQuart), () =>
                    {
                        if (CardSlotIndex != CardSlots.Count - 1) return;
                        
                        OnComplete?.Invoke();
                        RefillCoroutine = null;
                    });
                    
                    yield return new WaitForSeconds(.5f);
                }
            }
        #endregion
    }
}