using System;
using System.Collections;
using System.Collections.Generic;
using Animation_System.DOTween.Basic;
using Animation_System.DOTween.Combine;
using Battle_System.Object.Card_Slot;
using Battle_System.Object.Card;
using DG.Tweening;
using UnityEngine;

namespace Battle_System.System.Child
{
    internal sealed class ShowCardSystem : MonoBehaviour
    {
        [field: Header("Card Slot")]
        [field: SerializeField] private Transform SpawnParent;
        [field: SerializeField] private GameObject SlotPrefab;
        
        private readonly List<CardSlot> CardSlots = new();
        
        private IEnumerator ShowCor;

        private void OnDisable()
        {
            if (ShowCor is not null)
            {
                StopCoroutine(ShowCor);
                ShowCor = null;
            }
        }
        
        public void ShowCard(List<ICard> cards, Action onComplete = null)
        {
            ShowCor = ShowCardCoroutine(cards, onComplete);
            StartCoroutine(ShowCor);
        }

        private IEnumerator ShowCardCoroutine(List<ICard> cards, Action onComplete = null)
        {
            foreach (var slot in CardSlots)
                Destroy(slot.gameObject);
            CardSlots.Clear();
            for (var i = 0; i < cards.Count; i++)
            {
                var slot = Instantiate(SlotPrefab, SpawnParent);
                var slotScript = slot.GetComponent<CardSlot>();
                CardSlots.Add(slotScript);
            }

            var AnchorPosSettings = new DoAnchorPos(Vector3.zero, .5f, true, Ease.OutExpo);
            var FlipSettings = new DoFlip(
                new DoRotate(Vector2.down * 180, 1, RotateMode.Fast, Ease.Linear),
                new DoScale(Vector2.one * 1.25f, .5f, Ease.InSine),
                new DoScale(Vector2.one, .5f, Ease.OutSine));
            for (var i = 0; i < cards.Count; i++)
            {
                var index = i;
                var slot = CardSlots[index];
                var card = cards[index];
                slot.Set(card);
                card.MoveAndFlip(slot.transform, AnchorPosSettings, FlipSettings, () =>
                {
                    if (index != cards.Count - 1) return;
                    onComplete?.Invoke();
                    ShowCor = null;
                });
                yield return new WaitForSeconds(.25f);
            }
        }

        public void GetShowCards(out List<ICard> cards)
        {
            cards = new List<ICard>();
            foreach (var slot in CardSlots)
            {
                slot.Get(out var card);
                cards.Add(card);
            }
        }
    }
}