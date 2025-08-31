using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Object.Card_Slot;
using Battle.Object.Card.Base;
using Data.DOTween_Value;
using DG.Tweening;
using Player.Data.Battle.Card_Deck;
using UnityEngine;

namespace Battle.Child_System
{
    internal sealed class CardPoolSystem : MonoBehaviour
    {
        [field: SerializeField] private List<GameObject> Slots;
        [field: SerializeField] private Transform SpawnParent;
        [field: SerializeField] private PlayerBattleCardDeckSO PlayerBattleCardDeckSO;

        private readonly DoAnchorPosValue DoAnchorPosValue = new(Vector2.zero, .2f, true, Ease.OutQuart);
        private const float RefillDuration = .2f;
        
        private IEnumerator SortCoroutine;
        private IEnumerator RefillCoroutine;

        private void OnDisable()
        {
            if (SortCoroutine is not null)
            {
                StopCoroutine(SortCoroutine);
                SortCoroutine = null;
            }
            
            if (RefillCoroutine is not null)
            {
                StopCoroutine(RefillCoroutine);
                RefillCoroutine = null;
            }
        }

        public void GetAllSlots(out List<GameObject> Slots)
        {
            Slots = this.Slots;
        }

        #region Refill
            public void Refill(Action OnComplete = null)
            {
                SortCoroutine = SortProcess();
                RefillCoroutine = RefillProcess(OnComplete);
                StartCoroutine(SortCoroutine);
            }

            private IEnumerator SortProcess()
            {
                var RemainingCards = new List<GameObject>();
                foreach (var CurrentSlot in Slots)
                {
                    var CurrentSlotScript = CurrentSlot.GetComponent<CardSlot>();
                    if (CurrentSlotScript.IsEmpty()) continue;
                    CurrentSlotScript.Get(out var Card);
                    RemainingCards.Add(Card);
                }

                switch (RemainingCards.Count)
                {
                    case > 0:
                    {
                        for (var i = 0; i < RemainingCards.Count; i++)
                        {
                            var CurrentSlot = Slots[i];
                            var CurrentSlotScript = CurrentSlot.GetComponent<CardSlot>();
                            var CurrentCard = RemainingCards[i];
                            var CurrentCardScript = CurrentCard.GetComponent<ICard>();
                            var CurrentIndex = i;
                            CurrentSlotScript.Add(CurrentCard);
                            CurrentCardScript.MoveToParent(CurrentSlot.transform, DoAnchorPosValue, () =>
                            {
                                if (CurrentIndex != RemainingCards.Count - 1) return;
                                StartCoroutine(RefillCoroutine);
                                SortCoroutine = null;
                            });
                            yield return new WaitForSeconds(RefillDuration);
                        }

                        yield break;
                    }
                    case 0:
                    {
                        StartCoroutine(RefillCoroutine);
                        SortCoroutine = null;
                        yield break;
                    }
                }
            }

            private IEnumerator RefillProcess(Action OnComplete = null)
            {
                for (var i = 0; i < Slots.Count; i++)
                {
                    var CurrentSlot = Slots[i];
                    var CurrentSlotScript = CurrentSlot.GetComponent<CardSlot>();
                    if (!CurrentSlotScript.IsEmpty()) continue;
                    PlayerBattleCardDeckSO.GetRandomCardPrefab(out var CardPrefab);
                    var CurrentCard = Instantiate(CardPrefab, SpawnParent);
                    var CurrentCardScript = CurrentCard.GetComponent<ICard>();
                    var CurrentIndex = i;
                    CurrentSlotScript.Add(CurrentCard);
                    CurrentCardScript.MoveToParent(CurrentSlot.transform, DoAnchorPosValue, () =>
                    {
                        if (CurrentIndex != Slots.Count - 1) return;
                        OnComplete?.Invoke();
                        RefillCoroutine = null;
                    });
                    yield return new WaitForSeconds(RefillDuration);
                }
            }
        #endregion
    }
}