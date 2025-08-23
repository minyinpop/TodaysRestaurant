using System;
using System.Collections.Generic;
using Battle_Management_System.Card_System.Card_Slot_System;
using Battle_Management_System.Card_System.Card_System;
using UnityEngine;

namespace Battle_Management_System.Card_System.Hand_Card_System
{
    internal class HandCardSystem : MonoBehaviour
    {
        [field: SerializeField] private RectTransform CardSlotParent;
        [field: SerializeField] private GameObject CardSlotPrefab;

        private List<GameObject> CardSlotList = new();

        public void AddCard(List<GameObject> cardList, Action onComplete)
        {
            cardList.Reverse();
            
            for (var i = 0; i < cardList.Count; i ++)
            {
                var newSlot = Instantiate(CardSlotPrefab, CardSlotParent);
                CardSlotList.Add(newSlot);
            }
            
            for (var i = 0; i < cardList.Count; i ++)
            {
                var card = cardList[i];
                CardSlotList[i].GetComponent<CardSlotSystem>().AddCard(card);
                
                card.GetComponent<ICard>().MoveCardToSlot(CardSlotList[i].transform, () =>
                {
                    if (i == -1)
                        onComplete?.Invoke();
                });
            }
        }
    }
}