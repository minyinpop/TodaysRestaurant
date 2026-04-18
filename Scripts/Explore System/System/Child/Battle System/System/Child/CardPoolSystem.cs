using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Animation_System.DOTween.Basic;
using Common.Player.Child.Player_Deck;
using Common.Value.Type;
using DG.Tweening;
using Explore_System.System.Child.Battle_System.Object.Card_Slot;
using Explore_System.System.Child.Battle_System.Object.Card;
using Explore_System.System.Child.Battle_System.Object.Card.Battle;
using UnityEngine;

namespace Explore_System.System.Child.Battle_System.System.Child
{
    internal sealed class CardPoolSystem : MonoBehaviour
    {
        [field: Header("Parent")]
        [field: SerializeField] private Transform SpawnParent;
        
        [field: Header("Card Slot")]
        [field: SerializeField] private CardSlot[] CardSlots;
        
        [field: Header("Data")]
        [field: SerializeField] private PlayerDeckSO playerDeckData;

        private List<CardSO> _currentDeck = new();
        
        private IEnumerator SortCor;
        private IEnumerator RefillCor;
        private IEnumerator RecycleCor;

        private void Awake()
        {
            _currentDeck = playerDeckData.CardsData.ToList();
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
                var remainingCards = new List<Card>();
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
                    if (_currentDeck.Count == 0) { onComplete?.Invoke(); yield break; }
                    
                    var randomCardData = _currentDeck[UnityEngine.Random.Range(0, _currentDeck.Count)];
                    var card = Instantiate(randomCardData.Card.gameObject, SpawnParent);

                    if (randomCardData.Card is not BattleCard battleCard)
                    {
                        Debug.Log($"在 {nameof(CardPoolSystem)} 裡生成了不是 {nameof(BattleCard)} 的 {nameof(Card)}。");
                        yield break;
                    }
                    
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
            public void DrawCard(int number, out List<Card> cards)
            {
                number = Mathf.Clamp(number, 0, CardSlots.Length);
                cards = new List<Card>();
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
                    if (card.CardData.CardType == targetType)
                    {
                        completes.Add(false);
                        var index = completes.Count - 1;
                        card.DestroyCard(
                            onComplete: () =>
                            {
                                for (var i = 0; i < _currentDeck.Count; i++)
                                {
                                    if (_currentDeck[i].CardType != targetType)
                                    {
                                        continue;
                                    }
                                    
                                    _currentDeck.Remove(_currentDeck[i]);
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