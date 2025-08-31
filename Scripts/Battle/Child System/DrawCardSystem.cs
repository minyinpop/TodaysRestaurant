using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Object.Card_Slot;
using Battle.Object.Card.Base;
using Data.DOTween_Value;
using DG.Tweening;
using UnityEngine;

namespace Battle.Child_System
{
    internal sealed class DrawCardSystem : MonoBehaviour
    {
        [field: SerializeField] private GameObject SlotPrefab;
        [field: SerializeField] private Transform SpawnParent;
        
        private readonly List<GameObject> Slots = new();
        private const float DrawDuration = .5f;
        
        private readonly DoAnchorPosValue DoAnchorPosValue = new(Vector2.zero, 1, true, Ease.OutQuart);
        private readonly DoRotateValue DoRotateValue = new(Vector2.up * 180, 1, RotateMode.Fast, Ease.InOutSine);
        private readonly DoScaleValue DoScaleValue1 = new(Vector2.one * 1.25f, .5f, Ease.OutSine);
        private readonly DoScaleValue DoScaleValue2 = new(Vector2.one, .5f, Ease.InSine);

        private IEnumerator DrawCoroutine;
        
        private void OnDisable()
        {
            if (DrawCoroutine is not null)
            {
                StopCoroutine(DrawCoroutine);
                DrawCoroutine = null;
            }
        }
        
        public void DrawCardFromCardPool(List<GameObject> CardPoolSlots, int DrawNumber, Action OnComplete = null)
        {
            DrawCoroutine = DrawProcess(CardPoolSlots, DrawNumber, OnComplete);
            StartCoroutine(DrawCoroutine);
        }

        private IEnumerator DrawProcess(List<GameObject> CardPoolSlots, int DrawNumber, Action OnComplete = null)
        {
            for (var i = 0; i < DrawNumber; i++)
            {
                var CurrentSlot = Instantiate(SlotPrefab, SpawnParent);
                Slots.Add(CurrentSlot);
            }

            for (var i = 0; i < DrawNumber; i++)
            {
                // Card Pool Slot
                var CurrentCardPoolSlot = CardPoolSlots[i];
                var CurrentCardPoolSlotScript = CurrentCardPoolSlot.GetComponent<CardSlot>();
                // Slot
                var CurrentSlot = Slots[i];
                var CurrentSlotScript = CurrentSlot.GetComponent<CardSlot>();
                // Card (From Card Pool Slot)
                CurrentCardPoolSlotScript.Get(out var CurrentCard);
                var CurrentCardScript = CurrentCard.GetComponent<ICard>();
                // ===
                var CurrentIndex = i;
                CurrentSlotScript.Add(CurrentCard);
                CurrentCardScript.MoveToParent(CurrentSlot.transform, DoAnchorPosValue, () =>
                {
                    CurrentCardScript.FlipCard(new(DoRotateValue, DoScaleValue1, DoScaleValue2), () =>
                    {
                        if (CurrentIndex != DrawNumber - 1) return;
                        OnComplete?.Invoke();
                    });
                });
                yield return new WaitForSeconds(DrawDuration);
            }
        }
    }
}