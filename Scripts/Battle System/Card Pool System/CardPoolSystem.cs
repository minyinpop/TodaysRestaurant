using System;
using System.Collections;
using System.Collections.Generic;
using Battle_System.Card_Slot_System;
using Battle_System.Card_System.Base;
using DG.Tweening;
using DoTween_Settings;
using Player_System.Data.Battle.Equip_Deck;
using UnityEngine;

namespace Battle_System.Card_Pool_System
{
    internal sealed class CardPoolSystem : MonoBehaviour
    {
        [field: Header("Card")]
        [field: SerializeField] private Transform SpawnParent;
        [field: SerializeField] private EquipDeckSO EquipDeckData;
        
        [field: Header("Slot")]
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

        #region Refill
            public void Refill(Action OnComplete = null)
            {
                RefillCoroutine = RefillProcess();
                SpawnCoroutine = SpawnProcess(OnComplete);

                StartCoroutine(RefillCoroutine);
            }

            private IEnumerator RefillProcess()
            {
                List<GameObject> RemainingCards = new List<GameObject>();
                foreach (CardSlotSystem CardSlot in CardSlots)
                {
                    if (CardSlot.IsEmpty()) continue;
                    
                    CardSlot.GetCard(out GameObject Card);
                    RemainingCards.Add(Card);
                }

                switch (RemainingCards.Count)
                {
                    case 0:
                    {
                        StartCoroutine(SpawnProcess());
                        yield break;
                    }
                    case > 0:
                    {
                        for (int i = 0; i < RemainingCards.Count; i++)
                        {
                            CardSlotSystem CurrentSlot = CardSlots[i];
                            GameObject CurrentCard = RemainingCards[i];

                            int RemainingCardIndex = i;
                            DoAnchorPosSettings Settings = new DoAnchorPosSettings(Vector2.zero, .25f, true, Ease.OutExpo);
                        
                            CurrentSlot.AddCard(CurrentCard);
                            CurrentCard.GetComponent<ICard>().MoveToParent(CurrentSlot.transform, Settings, () =>
                            {
                                if (RemainingCardIndex != RemainingCards.Count - 1) return;
                                StartCoroutine(SpawnProcess());
                            });
                        
                            yield return new WaitForSeconds(.25f);
                        }

                        yield break;
                    }
                }
            }

            private IEnumerator SpawnProcess(Action OnComplete = null)
            {
                for (int i = 0; i < CardSlots.Count; i++)
                {
                    CardSlotSystem CardSlot = CardSlots[i];
                    if (!CardSlot.IsEmpty()) continue;
                    
                    EquipDeckData.GetRandomCard(out GameObject CardPrefab);
                    GameObject Card = Instantiate(CardPrefab, SpawnParent);

                    int CardSlotIndex = i;
                    DoAnchorPosSettings Settings = new DoAnchorPosSettings(Vector2.zero, .25f, true, Ease.OutExpo);
                    
                    CardSlot.AddCard(Card);
                    Card.GetComponent<ICard>().MoveToParent(CardSlot.transform, Settings, () =>
                    {
                        if (CardSlotIndex != CardSlots.Count - 1) return;
                        OnComplete?.Invoke();
                    });
                    
                    yield return new WaitForSeconds(.25f);
                }
            }
        #endregion
    }
}