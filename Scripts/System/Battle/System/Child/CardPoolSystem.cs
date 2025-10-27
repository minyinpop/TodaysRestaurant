using System.Battle.Object.Card_Slot;
using System.Battle.Object.Card.Base;
using System.Battle.Object.Card.Type.Battle;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data.Animation.DOTween.Basic;
using Data.General;
using Data.General.Enum;
using Data.Player.Child;
using Data.Player.Child.Deck;
using Data.Player.Main;
using DG.Tweening;
using UnityEngine;

namespace System.Battle.System.Child
{
    internal sealed class CardPoolSystem : MonoBehaviour
    {
        [field: Header("Parent")]
        [field: SerializeField] private Transform SpawnParent;
        
        [field: Header("Card Slot")]
        [field: SerializeField] private CardSlot[] CardSlots;
        
        [field: Header("Data")]
        [field: SerializeField] private PlayerSO PlayerData;

        private readonly DeckSO DeckData = new();
        
        private IEnumerator SortCor;
        private IEnumerator RefillCor;
        private IEnumerator RecycleCor;
        
        private void Start()
        {
            PlayerData.GetCardPrefabs(out var cardPrefabs);
            DeckData.Set(cardPrefabs);
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
                    if (!DeckData.GetRandomCard(out var cardPrefab))
                    {
                        onComplete?.Invoke();
                        yield break;
                    }

                    var card = Instantiate(cardPrefab, SpawnParent);
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
                                DeckData.Remove(targetType);
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