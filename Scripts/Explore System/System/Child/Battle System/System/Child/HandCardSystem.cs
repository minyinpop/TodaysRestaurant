using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Animation_System.DOTween.Basic;
using Common.Value.Type;
using DG.Tweening;
using Explore_System.System.Child.Battle_System.Object.Card_Slot;
using Explore_System.System.Child.Battle_System.Object.Card;
using Explore_System.System.Child.Battle_System.Object.Card.Battle;
using Explore_System.System.Child.Battle_System.System.Child.Selected_Card_System.System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Explore_System.System.Child.Battle_System.System.Child
{
    internal sealed class HandCardSystem : MonoBehaviour
    {
        [field: Header("卡片格子")]
        [field: SerializeField, FormerlySerializedAs("SpawnParent")] private Transform spawnParent;
        [field: SerializeField, FormerlySerializedAs("SlotPrefab")] private GameObject slotPrefab;
        
        private readonly List<CardSlot> cardSlots = new();
        
        private IEnumerator _addCoroutine;
        private IEnumerator _recycleCoroutine;
        
        public event Func<Card, bool> TryAddCardToSelected;
        
        public event Action<Card> OnHoverCardEvent;
        public event Action<Card> OnHoverExitEvent;
        
        private void Awake()
        {
            SelectedCardSystem.OnOpen += EnableAllCards;
            SelectedCardSystem.OnConfirm += DisableAllCards;
        }

        private void OnDisable()
        {
            if (_addCoroutine is not null)
            {
                StopCoroutine(_addCoroutine);
                _addCoroutine = null;
            }
            
            if (_recycleCoroutine is not null)
            {
                StopCoroutine(_recycleCoroutine);
                _recycleCoroutine = null;
            }
        }

        private void OnDestroy()
        {
            SelectedCardSystem.OnOpen -= EnableAllCards;
            SelectedCardSystem.OnConfirm -= DisableAllCards;
        }
        
        private void EnableAllCards()
        {
            foreach (var cardSlot in cardSlots)
            {
                cardSlot.Get(out var card);
                
                if (card is null)
                {
                    continue;
                }
                
                card.Interactable = true;
                cardSlot.Set(card);
            }
        }
        
        private void DisableAllCards()
        {
            foreach (var cardSlot in cardSlots)
            {
                cardSlot.Get(out var card);
                
                if (card is null)
                {
                    continue;
                }
                
                card.Interactable = false;
                cardSlot.Set(card);
            }
        }
        
        private void OnHoverCard(Card card)
        {
            if (OnHoverCardEvent is null)
            {
                Debug.Log($"{OnHoverCardEvent} 沒有被其它 class 訂閱。");
                return;
            }

            OnHoverCardEvent.Invoke(card);
        }

        private void OnHoverExit(Card card)
        {
            if (OnHoverExitEvent is null)
            {
                Debug.Log($"{OnHoverExitEvent} 沒有被其它 class 訂閱。");
                return;
            }

            OnHoverExitEvent.Invoke(card);
        }

        private void OnClickCard(Card card)
        {
            if (TryAddCardToSelected?.Invoke(card) == false)
            {
                return;
            }

            card.OnHover -= OnHoverCard;
            card.OnHoverExit -= OnHoverExit;
            card.OnClick -= OnClickCard;
            
            for (var i = 0; i < cardSlots.Count; i++)
            {
                if (!cardSlots[i].Compare(card))
                {
                    continue;
                }

                var cardSlot = cardSlots[i];
                
                cardSlots.Remove(cardSlot);
                Destroy(cardSlot.gameObject);
                
                return;
            }
        }

        #region Add
            public void Add(Card card)
            {
                var slot = Instantiate(slotPrefab, spawnParent);
                var slotScript = slot.GetComponent<CardSlot>();
                
                cardSlots.Add(slotScript);
                slotScript.Set(card);
                
                if (card is BattleCard battleCard)
                {
                    battleCard.Move(slot.transform, new DoAnchorPos(Vector2.zero, .5f, true, Ease.OutExpo));
                    
                    battleCard.OnHover += OnHoverCard;
                    battleCard.OnHoverExit += OnHoverExit;
                    battleCard.OnClick += OnClickCard;
                }
                else
                {
                    throw new InvalidOperationException($"{card.name} 是未在 {nameof(HandCardSystem)} 裡面登記的卡片類型，請聯絡團隊添加。");
                }
            }

            public void Add(List<Card> cards, Action onComplete)
            {
                _addCoroutine = AddCoroutine();
                StartCoroutine(_addCoroutine);
                return;

                IEnumerator AddCoroutine()
                {
                    var completes = new List<bool>();
                    
                    for (var i = 0; i < cards.Count; i++)
                    {
                        completes.Add(false);
                        
                        var index = i;
                        var slot = Instantiate(slotPrefab, spawnParent);
                        var slotScript = slot.GetComponent<CardSlot>();
                        var card = cards[index];

                        if (card is BattleCard battleCard)
                        {
                            cardSlots.Add(slotScript);
                            slotScript.Set(battleCard);
                            battleCard.Move(slot.transform, new DoAnchorPos(Vector2.zero, .5f, true, Ease.OutExpo), 
                                onComplete: () =>
                                {
                                    completes[index] = true;
                                });
                            
                            battleCard.OnHover += OnHoverCard;
                            battleCard.OnHoverExit += OnHoverExit;
                            battleCard.OnClick += OnClickCard;
                        }
                        else
                        {
                            throw new InvalidOperationException($"{card.name} 是未在 {nameof(HandCardSystem)} 裡面登記的卡片類型，請聯絡團隊添加。");
                        }
                        
                        yield return new WaitForSeconds(.2f);
                    }
                    
                    yield return new WaitUntil(() => completes.All(c => c));
                    
                    onComplete?.Invoke();
                    _addCoroutine = null;
                }
            }
        #endregion
        
        public void RecycleCard(BattleCardType targetType, Action onComplete)
        {
            _recycleCoroutine = RecycleCardCoroutine();
            StartCoroutine(_recycleCoroutine);
            return;
            
            IEnumerator RecycleCardCoroutine()
            {
                var completes = new Dictionary<BattleCard, bool>();
                
                foreach (var slot in cardSlots)
                {
                    slot.Get(out var card);
                    
                    if (card is not BattleCard battleCard)
                    {
                        slot.Set(card);
                        continue;
                    }
                    
                    if (battleCard.BattleCardData.BattleCardType != targetType)
                    {
                        slot.Set(card);
                        continue;
                    }
                    
                    completes.Add(battleCard, false);
                    
                    card.DestroyCard(
                        onComplete: () =>
                        {
                            cardSlots.Remove(slot);
                            Destroy(slot.gameObject);
                            
                            completes[battleCard] = true;
                        });
                }
                
                yield return new WaitUntil(() => completes.Values.All(value => value));
                
                onComplete.Invoke();
            }
        }
    }
}