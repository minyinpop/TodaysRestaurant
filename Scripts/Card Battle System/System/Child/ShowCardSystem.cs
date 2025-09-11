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
        
        private const float DrawDuration = .25f;
        
        private IEnumerator ShowCor;

        private void OnDisable()
        {
            if (ShowCor is not null)
            {
                StopCoroutine(ShowCor);
                ShowCor = null;
            }
        }
        
        /// <summary>
        /// 展示從卡池裡抽到的卡片
        /// </summary>
        /// <param name="cards">被抽到的卡片</param>
        /// <param name="onComplete">完成後的回傳</param>
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

            for (var i = 0; i < cards.Count; i++)
            {
                var index = i;
                var slot = CardSlots[index];
                var card = cards[index];
                
                slot.Set(card);
                card.MoveToShowPoint(slot.transform, AnchorPosSettings, FlipSettings, () =>
                {
                    if (index != cards.Count - 1) return;
                    onComplete?.Invoke();
                    ShowCor = null;
                });
                
                yield return new WaitForSeconds(DrawDuration);
            }
        }

        /// <summary>
        /// 獲取展示中的所有卡片
        /// </summary>
        /// <param name="cards">展示中的卡片</param>
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