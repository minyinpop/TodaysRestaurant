using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Animation_System.DOTween.Basic;
using Common.Value.Type;
using DG.Tweening;
using Explore_System.System.Child.Battle_System.Object.Card_Slot;
using Explore_System.System.Child.Battle_System.Object.Card;
using Explore_System.System.Child.Battle_System.System.Child.Selected_Card_System.Main;
using UnityEngine;

namespace Explore_System.System.Child.Battle_System.System.Child
{
    internal sealed class HandCardSystem : MonoBehaviour
    {
        [field: Header("Card Slot")]
        [field: SerializeField] private Transform SpawnParent;
        [field: SerializeField] private GameObject SlotPrefab;
        
        private readonly List<CardSlot> CardSlots = new();
        
        private IEnumerator AddCor;
        private IEnumerator RecycleCor;

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
            if (AddCor is not null)
            {
                StopCoroutine(AddCor);
                AddCor = null;
            }
            
            if (RecycleCor is not null)
            {
                StopCoroutine(RecycleCor);
                RecycleCor = null;
            }
        }

        private void OnDestroy()
        {
            SelectedCardSystem.OnOpen -= EnableAllCards;
            SelectedCardSystem.OnConfirm -= DisableAllCards;
        }

        #region 設定卡片的互動
            private void EnableAllCards()
            {
                foreach (var cardSlot in CardSlots)
                {
                    cardSlot.Get(out var card);
                    card.Interactable = true;
                    cardSlot.Set(card);
                }
            }

            private void DisableAllCards()
            {
                foreach (var cardSlot in CardSlots)
                {
                    cardSlot.Get(out var card);
                    card.Interactable = false;
                    cardSlot.Set(card);
                }
            }
        #endregion

        private void OnHoverCard(Card card)
        {
        }

        private void OnHoverExit(Card card)
        {
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
            
            for (var i = 0; i < CardSlots.Count; i++)
            {
                if (!CardSlots[i].Compare(card))
                {
                    continue;
                }
                
                CardSlots.Remove(CardSlots[i]);
                Destroy(CardSlots[i].gameObject);
                
                return;
            }
        }

        #region Add
            public void Add(Card card)
            {
                var slot = Instantiate(SlotPrefab, SpawnParent);
                var slotScript = slot.GetComponent<CardSlot>();
                
                CardSlots.Add(slotScript);
                slotScript.Set(card);
                card.Move(slot.transform, new DoAnchorPos(Vector2.zero, .5f, true, Ease.OutExpo));
                
                card.OnHover += OnHoverCard;
                card.OnHoverExit += OnHoverExit;
                card.OnClick += OnClickCard;
            }

            public void Add(List<Card> cards, Action onComplete)
            {
                AddCor = AddCoroutine();
                StartCoroutine(AddCor);
                return;

                IEnumerator AddCoroutine()
                {
                    var completes = new List<bool>();
                    for (var i = 0; i < cards.Count; i++)
                    {
                        completes.Add(false);
                        var index = i;
                        var slot = Instantiate(SlotPrefab, SpawnParent);
                        var slotScript = slot.GetComponent<CardSlot>();
                        var card = cards[index];
                        CardSlots.Add(slotScript);
                        slotScript.Set(card);
                        card.Move(slot.transform, new DoAnchorPos(Vector2.zero, .5f, true, Ease.OutExpo), 
                            onComplete: () =>
                            {
                                completes[index] = true;
                            });
                        
                        card.OnHover += OnHoverCard;
                        card.OnHoverExit += OnHoverExit;
                        card.OnClick += OnClickCard;
                        
                        yield return new WaitForSeconds(.2f);
                    }
                    
                    yield return new WaitUntil(() => completes.All(c => c));
                    onComplete?.Invoke();
                    AddCor = null;
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
                                CardSlots.Remove(slot);
                                Destroy(slot.gameObject);
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