using System;
using System.Collections;
using System.Collections.Generic;
using Card_Battle_System.Object.Card_Slot;
using Card_Battle_System.Object.Card.Base;
using Data.DOTween.Basic;
using Data.DOTween.Combine;
using DG.Tweening;
using UnityEngine;

namespace Card_Battle_System.System.Child
{
    internal sealed class ShowCardSystem : MonoBehaviour
    {
        [field: Header("Card Slot")]
        [field: SerializeField] private Transform SpawnParent;
        [field: SerializeField] private GameObject SlotPrefab;
        
        private readonly List<CardSlot> CardSlots = new();
        
        private readonly DoAnchorPos AnchorPosSettings = new(Vector3.zero, .5f, true, Ease.OutExpo);
        private readonly DoFlip FlipSettings = new(
            new DoRotate(Vector2.up * 180, 1, RotateMode.Fast, Ease.Linear),
            new DoScale(Vector2.one * 1.25f, .5f, Ease.InSine),
            new DoScale(Vector2.one, .5f, Ease.OutSine));
        
        private const float ShowDuration = .25f;
        
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
            for (var i = 0; i < cards.Count; i++)
            {
                var index = i;
                var slot = Instantiate(SlotPrefab, SpawnParent);
                var slotScript = slot.GetComponent<CardSlot>();
                var card = cards[index];
                
                slotScript.Set(card);
                card.MoveToShowPoint(slot.transform, AnchorPosSettings, FlipSettings, () =>
                {
                    if (index != cards.Count - 1) return;
                    onComplete?.Invoke();
                    ShowCor = null;
                });
                
                yield return new WaitForSeconds(ShowDuration);
            }
        }
    }
}