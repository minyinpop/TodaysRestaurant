using System;
using System.Collections;
using System.Collections.Generic;
using Battle_Management.Card_Slot;
using Battle_Management.Card.Base;
using Player.Data.Equip_Deck;
using UnityEngine;

namespace Battle_Management.Card_Pool
{
    internal class CardPool : MonoBehaviour
    {
        [field: Header("Equip Deck Data")]
        [field: SerializeField] private EquipDeckSO EquipDeckData;
        
        [field: Header("Position")]
        [field: SerializeField] private RectTransform SpawnPos;
        
        [field: Header("Card Slots")]
        [field: SerializeField] private List<CardSlot> CardSlots;
        
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
                List<GameObject> tempCards = new();

                foreach (var slot in CardSlots)
                {
                    var slotScript = slot.GetComponent<CardSlot>();
                    if (slotScript.IsEmpty()) continue;
                    slotScript.GetCard(out var card);
                    tempCards.Add(card);
                }

                switch (tempCards.Count)
                {
                    case > 0:
                    {
                        for (var i = 0; i < tempCards.Count; i++)
                        {
                            var slot = CardSlots[i];
                            var slotScript = slot.GetComponent<CardSlot>();
                            var card = tempCards[i];
                            var cardScript = card.GetComponent<ICard>();
                            slotScript.AddCard(card);
                            var cardIndex = i;
                            cardScript.MoveToParent(slot.transform, .5f, () =>
                            {
                                if (cardIndex != tempCards.Count - 1) return;
                                StartCoroutine(SpawnCoroutine);
                            });
                            yield return new WaitForSeconds(.25f);
                        }

                        break;
                    }
                    case 0:
                    {
                        StartCoroutine(SpawnCoroutine);
                        break;
                    }
                }

                RefillCoroutine = null;
            }

            private IEnumerator SpawnProcess(Action OnComplete = null)
            {
                for (var i = 0; i < CardSlots.Count; i++)
                {
                    var slot = CardSlots[i];
                    var slotScript = slot.GetComponent<CardSlot>();
                    if (!slotScript.IsEmpty()) continue;
                    EquipDeckData.GetRandomCard(out var cardPrefab);
                    var card = Instantiate(cardPrefab, SpawnPos);
                    slotScript.AddCard(card);
                    var cardScript = card.GetComponent<ICard>();
                    var slotIndex = i;
                    cardScript.MoveToParent(slot.transform, .5f, () =>
                    {
                        if (slotIndex != CardSlots.Count - 1) return;
                        OnComplete?.Invoke();
                    });
                    yield return new WaitForSeconds(.25f);
                }

                SpawnCoroutine = null;
            }
        #endregion
    }
}