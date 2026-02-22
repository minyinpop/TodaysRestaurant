using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Animation_System.DOTween.Basic;
using Battle_System.Object.Card_Slot;
using Battle_System.Object.Card;
using Battle_System.Object.Card.Battle;
using Common.Player.Child.Player_Deck;
using Common.Value.Type;
using DG.Tweening;
using UnityEngine;

namespace Battle_System.System.Child
{
    internal sealed class CardPoolSystem : MonoBehaviour
    {
        [field: Header("Parent")]
        [field: SerializeField] private Transform SpawnParent;
        
        [field: Header("Card Slot")]
        [field: SerializeField] private CardSlot[] CardSlots;
        
        [field: Header("Data")]
        [field: SerializeField] private PlayerDeckSO playerDeckData;

        private List<GameObject> CurrentDeck = new();
        
        private IEnumerator SortCor;
        private IEnumerator RefillCor;
        private IEnumerator RecycleCor;

        private void Awake()
        {
            playerDeckData.Get(out var deck);
            CurrentDeck = deck.ToList();
        }

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
            
            if (RecycleCor is not null)
            {
                StopCoroutine(RecycleCor);
                RecycleCor = null;
            }
        }
        
        #region Refill
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

                switch (remainingCards.Count)
                {
                    case 0:
                    {
                        StartCoroutine(RefillCor);
                        SortCor = null;
                        break;
                    }
                    case > 0:
                    {
                        for (var i = 0; i < remainingCards.Count; i++)
                        {
                            var index = i;
                            var slot = CardSlots[index];
                            var card = remainingCards[index];
                            slot.Set(card);
                            card.Move(slot.transform, new(Vector3.zero, .5f, true, Ease.OutExpo),
                                onComplete: () =>
                                {
                                    if (index != remainingCards.Count - 1) return;
                                    StartCoroutine(RefillCor);
                                    SortCor = null;
                                });
                            yield return new WaitForSeconds(.2f);
                        }

                        break;
                    }
                }
            }
            
            private IEnumerator RefillCoroutine(Action onComplete)
            {
                for (var i = 0; i < CardSlots.Length; i++)
                {
                    var index = i;
                    var slot = CardSlots[index];
                    if (!slot.IsEmpty()) continue;
                    if (CurrentDeck.Count == 0) { onComplete?.Invoke(); yield break; }
                    
                    var randomCardPrefab = CurrentDeck[UnityEngine.Random.Range(0, CurrentDeck.Count)];
                    var card = Instantiate(randomCardPrefab, SpawnParent);
                    var battleCard = card.GetComponent<BattleCard>();
                    slot.Set(battleCard);
                    battleCard.Move(slot.transform, new DoAnchorPos(Vector3.zero, .5f, true, Ease.OutExpo),
                        onComplete: () =>
                        {
                            if (index != CardSlots.Length - 1) return;
                            onComplete?.Invoke();
                            RefillCor = null;
                        });
                    yield return new WaitForSeconds(.2f);
                }
            }
        #endregion
        
        #region Draw
            public void DrawCard(int number, out List<ICard> cards)
            {
                number = Mathf.Clamp(number, 0, CardSlots.Length);
                cards = new List<ICard>();
                for (var i = 0; i < number; i++)
                {
                    var slot = CardSlots[i];
                    slot.Get(out var card);
                    cards.Add(card);
                }
            }
        #endregion

        public void RecycleCard(CardType targetType, Action onComplete)
        {
            RecycleCor = RecycleCardCoroutine();
            StartCoroutine(RecycleCor);
            return;
            
            IEnumerator RecycleCardCoroutine()
            {
                var completes = new List<bool>();
                foreach (var slot in CardSlots)
                {
                    slot.Get(out var card);
                    card.GetCardType(out var type);
                    if (type == targetType)
                    {
                        completes.Add(false);
                        var index = completes.Count - 1;
                        card.DestroyCard(
                            onComplete: () =>
                            {
                                for (var i = 0; i < CurrentDeck.Count; i++)
                                {
                                    var cardPrefab = CurrentDeck[i];
                                    cardPrefab.GetComponent<ICard>().GetCardType(out var cardType);
                                    if (cardType != targetType) continue;
                                    CurrentDeck.Remove(cardPrefab);
                                }

                                completes[index] = true;
                            });
                    }
                    else
                        slot.Set(card);
                }
                
                yield return new WaitUntil(() => completes.All(c => c));
                onComplete?.Invoke();
            }
        }
    }
}