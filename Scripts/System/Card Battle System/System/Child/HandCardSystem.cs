using System.Card_Battle_System.Object.Card_Slot;
using System.Card_Battle_System.Object.Card.Base;
using System.Collections;
using System.Collections.Generic;
using Data.DOTween.Basic;
using DG.Tweening;
using UnityEngine;

namespace System.Card_Battle_System.System.Child
{
    internal sealed class HandCardSystem : MonoBehaviour
    {
        [field: Header("Card Slot")]
        [field: SerializeField] private Transform SpawnParent;
        [field: SerializeField] private GameObject SlotPrefab;
        
        private readonly List<CardSlot> CardSlots = new();
        
        private IEnumerator AddCor;

        public static event Func<ICard, bool> TryAddCardToSelected;

        private void OnEnable()
        {
            SelectedCardSystem.ReturnCardToHand += Add;
        }

        private void OnDisable()
        {
            SelectedCardSystem.ReturnCardToHand -= Add;
            if (AddCor is not null)
            {
                StopCoroutine(AddCor);
                AddCor = null;
            }
        }
        
        public void SetCardsInteractable(bool interactable)
        {
            foreach (var cardSlot in CardSlots)
                cardSlot.SetInteractable(interactable);
        }

        private void OnCardClicked(ICard card)
        {
            if (TryAddCardToSelected?.Invoke(card) == false) return;
            card.OnClick -= OnCardClicked;
            for (var i = 0; i < CardSlots.Count; i++)
            {
                var slot = CardSlots[i];
                if (!slot.Compare(card)) continue;
                CardSlots.Remove(slot);
                Destroy(slot.gameObject);
                return;
            }
        }

        #region Add
            private void Add(ICard card)
            {
                var slot = Instantiate(SlotPrefab, SpawnParent);
                var slotScript = slot.GetComponent<CardSlot>();
                CardSlots.Add(slotScript);
                slotScript.Set(card);
                card.Move(slot.transform, new DoAnchorPos(Vector2.zero, .5f, true, Ease.OutExpo));
                card.OnClick += OnCardClicked;
            }

            public void Add(List<ICard> cards, Action onComplete = null)
            {
                AddCor = AddCoroutine(cards, onComplete);
                StartCoroutine(AddCor);
            }

            private IEnumerator AddCoroutine(List<ICard> cards, Action onComplete = null)
            {
                for (var i = 0; i < cards.Count; i++)
                {
                    var index = i;
                    var slot = Instantiate(SlotPrefab, SpawnParent);
                    var slotScript = slot.GetComponent<CardSlot>();
                    var card = cards[index];
                    CardSlots.Add(slotScript);
                    slotScript.Set(card);
                    card.Move(slot.transform, new DoAnchorPos(Vector2.zero, .5f, true, Ease.OutExpo), () =>
                    {
                        if (index != cards.Count - 1) return;
                        onComplete?.Invoke();
                        AddCor = null;
                    });
                    card.OnClick += OnCardClicked;
                    yield return new WaitForSeconds(.25f);
                }
            }
        #endregion
    }
}