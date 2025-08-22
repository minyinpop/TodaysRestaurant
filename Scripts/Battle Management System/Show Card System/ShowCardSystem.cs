using System;
using System.Collections;
using System.Collections.Generic;
using Battle_Management_System.Card_Slot_System;
using Battle_Management_System.Card_System;
using UnityEngine;

namespace Battle_Management_System.Show_Card_System
{
    internal class ShowCardSystem : MonoBehaviour
    {
        [field: Header("Show Point")]
        [field: SerializeField] private CardSlotSystem LeftPoint01;
        [field: SerializeField] private CardSlotSystem LeftPoint02;
        [field: SerializeField] private CardSlotSystem MiddlePoint;
        [field: SerializeField] private CardSlotSystem RightPoint01;
        [field: SerializeField] private CardSlotSystem RightPoint02;
        
        [field: Header("Settings")]
        [field: SerializeField] private float DrawDuration;
        [field: SerializeField] private float ShowDuration;
        
        private IEnumerator StartShowCardCoroutine;
        private IEnumerator EndShowCardCoroutine;

        private void OnDisable()
        {
            if (StartShowCardCoroutine is not null)
            {
                StopCoroutine(StartShowCardCoroutine);
                StartShowCardCoroutine = null;
            }
            
            if (EndShowCardCoroutine is not null)
            {
                StopCoroutine(EndShowCardCoroutine);
                EndShowCardCoroutine = null;
            }
        }

        public void ShowCard(List<GameObject> cardList, Action onComplete)
        {
            StartShowCardCoroutine = StartShowCardProcess(cardList);
            EndShowCardCoroutine = EndShowCardProcess(onComplete);
            
            StartCoroutine(StartShowCardCoroutine);
        }
        
        private void ShowCard(CardSlotSystem slot, GameObject card, Action onComplete = null)
        {
            slot.AddCard(card);
            card.GetComponent<ICard>().MoveCardToSlotAndFlip(slot.transform, onComplete);
        }

        private IEnumerator StartShowCardProcess(List<GameObject> cardList)
        {
            switch (cardList.Count)
            {
                case 1:
                {
                    ShowCard(MiddlePoint, cardList[0], () => StartCoroutine(EndShowCardCoroutine));
                    break;
                }
                case 2:
                {
                    ShowCard(RightPoint01, cardList[0]);
                    yield return new WaitForSeconds(DrawDuration);
                    ShowCard(LeftPoint02, cardList[1], () => StartCoroutine(EndShowCardCoroutine));
                    break;
                }
                case 3:
                {
                    ShowCard(RightPoint01, cardList[0]);
                    yield return new WaitForSeconds(DrawDuration);
                    ShowCard(MiddlePoint, cardList[1]);
                    yield return new WaitForSeconds(DrawDuration);
                    ShowCard(LeftPoint02, cardList[2], () => StartCoroutine(EndShowCardCoroutine));
                    break;
                }
                case 4:
                {
                    ShowCard(RightPoint02, cardList[0]);
                    yield return new WaitForSeconds(DrawDuration);
                    ShowCard(RightPoint01, cardList[1]);
                    yield return new WaitForSeconds(DrawDuration);
                    ShowCard(LeftPoint02, cardList[2]);
                    yield return new WaitForSeconds(DrawDuration);
                    ShowCard(LeftPoint01, cardList[3], () => StartCoroutine(EndShowCardCoroutine));
                    break;
                }
                case 5:
                {
                    ShowCard(RightPoint02, cardList[0]);
                    yield return new WaitForSeconds(DrawDuration);
                    ShowCard(RightPoint01, cardList[1]);
                    yield return new WaitForSeconds(DrawDuration);
                    ShowCard(MiddlePoint, cardList[2]);
                    yield return new WaitForSeconds(DrawDuration);
                    ShowCard(LeftPoint02, cardList[3]);
                    yield return new WaitForSeconds(DrawDuration);
                    ShowCard(LeftPoint01, cardList[4], () => StartCoroutine(EndShowCardCoroutine));
                    break;
                }
            }
        }

        private IEnumerator EndShowCardProcess(Action onComplete)
        {
            yield return new WaitForSeconds(ShowDuration);
            onComplete?.Invoke();
        }
    }
}