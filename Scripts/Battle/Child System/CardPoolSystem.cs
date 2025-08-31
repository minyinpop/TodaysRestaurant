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
        [field: Header("Slot")]
        [field: SerializeField] private List<CardSlot> CardSlotList;
        
        [field: Header("Card")]
        [field: SerializeField] private Transform CardSpawnParent;
        
        [field: Header("Data")]
        [field: SerializeField] private PlayerBattleCardDeckSO PlayerBattleCardDeckSO;

        private const float RefillDuration = .15f;

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

        #region Refill
            public void Refill(Action OnComplete = null)
            {
                SortCoroutine = SortProcess();
                RefillCoroutine = RefillProcess(OnComplete);
                StartCoroutine(SortCoroutine);
            }

            private IEnumerator SortProcess()
            {
                List<GameObject> RemainingCardList = new List<GameObject>();
                foreach (CardSlot Slot in CardSlotList)
                {
                    if (Slot.IsEmpty()) continue;
                    Slot.Get(out GameObject Card);
                    RemainingCardList.Add(Card);
                }

                switch (RemainingCardList.Count)
                {
                    case > 0:
                    {
                        for (int i = 0; i < RemainingCardList.Count; i++)
                        {
                            CardSlot Slot = CardSlotList[i];
                            if (!Slot.IsEmpty()) continue;
                            GameObject Card = RemainingCardList[i];
                            Slot.Add(Card);
                            int CurrentIndex = i;
                            Card.GetComponent<ICard>().MoveToParent(Slot.transform, new DoAnchorPosValue(Vector2.zero, .25f, true, Ease.OutQuart), () =>
                            {
                                if (CurrentIndex != RemainingCardList.Count - 1) return;
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
                for (int i = 0; i < CardSlotList.Count; i++)
                {
                    CardSlot Slot = CardSlotList[i];
                    if (!Slot.IsEmpty()) continue;
                    PlayerBattleCardDeckSO.DrawCard(out GameObject CardPrefab);
                    GameObject Card = Instantiate(CardPrefab, CardSpawnParent);
                    Slot.Add(Card);
                    int CurrentIndex = i;
                    Card.GetComponent<ICard>().MoveToParent(Slot.transform, new DoAnchorPosValue(Vector2.zero, .25f, true, Ease.OutQuart), () =>
                    {
                        if (CurrentIndex != CardSlotList.Count - 1) return;
                        OnComplete?.Invoke();
                        RefillCoroutine = null;
                    });
                    yield return new WaitForSeconds(RefillDuration);
                }
            }
        #endregion
    }
}