using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Battle_Management_System.Card_System.Card_Slot_System;
using Battle_Management_System.Card_System.Card_System;
using UnityEngine;

namespace Battle_Management_System.Card_System.Show_Card_System
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
        
        private List<GameObject> CardList;
        
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

        public void ShowCard(List<GameObject> cardList, Action onGetComplete, Action onShowComplete)
        {
            StartShowCardCoroutine = StartShowCardProcess(cardList);
            EndShowCardCoroutine = EndShowCardProcess(onGetComplete, onShowComplete);
            
            StartCoroutine(StartShowCardCoroutine);
        }
        
        private void ShowCard(CardSlotSystem slot, GameObject card, Action onShowComplete = null)
        {
            slot.AddCard(card);
            card.GetComponent<ICard>().MoveCardToSlotAndFlip(slot.transform, onShowComplete);
        }

        private IEnumerator StartShowCardProcess(List<GameObject> cardList)
        {
            CardList = cardList.ToList();
            
            switch (CardList.Count)
            {
                case 1:
                {
                    ShowCard(MiddlePoint, CardList[0], () => StartCoroutine(EndShowCardCoroutine));
                    break;
                }
                case 2:
                {
                    ShowCard(RightPoint01, CardList[0]);
                    yield return new WaitForSeconds(DrawDuration);
                    ShowCard(LeftPoint02, CardList[1], () => StartCoroutine(EndShowCardCoroutine));
                    break;
                }
                case 3:
                {
                    ShowCard(RightPoint01, CardList[0]);
                    yield return new WaitForSeconds(DrawDuration);
                    ShowCard(MiddlePoint, CardList[1]);
                    yield return new WaitForSeconds(DrawDuration);
                    ShowCard(LeftPoint02, CardList[2], () => StartCoroutine(EndShowCardCoroutine));
                    break;
                }
                case 4:
                {
                    ShowCard(RightPoint02, CardList[0]);
                    yield return new WaitForSeconds(DrawDuration);
                    ShowCard(RightPoint01, CardList[1]);
                    yield return new WaitForSeconds(DrawDuration);
                    ShowCard(LeftPoint02, CardList[2]);
                    yield return new WaitForSeconds(DrawDuration);
                    ShowCard(LeftPoint01, CardList[3], () => StartCoroutine(EndShowCardCoroutine));
                    break;
                }
                case 5:
                {
                    ShowCard(RightPoint02, CardList[0]);
                    yield return new WaitForSeconds(DrawDuration);
                    ShowCard(RightPoint01, CardList[1]);
                    yield return new WaitForSeconds(DrawDuration);
                    ShowCard(MiddlePoint, CardList[2]);
                    yield return new WaitForSeconds(DrawDuration);
                    ShowCard(LeftPoint02, CardList[3]);
                    yield return new WaitForSeconds(DrawDuration);
                    ShowCard(LeftPoint01, CardList[4], () => StartCoroutine(EndShowCardCoroutine));
                    break;
                }
            }
        }

        private IEnumerator EndShowCardProcess(Action onGetComplete, Action onShowComplete)
        {
            onGetComplete?.Invoke();
            yield return new WaitForSeconds(ShowDuration);
            onShowComplete?.Invoke();
        }

        public void GetCards(out List<GameObject> cardList)
        {
            var tempCardList = CardList.ToList();
            cardList = tempCardList;
            CardList.Clear();
        }
    }
}