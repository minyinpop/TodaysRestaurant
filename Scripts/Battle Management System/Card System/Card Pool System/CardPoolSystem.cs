using System;
using System.Collections;
using System.Collections.Generic;
using Battle_Management_System.Card_Slot_System;
using Battle_Management_System.Card_System.Card_System;
using Battle_Management_System.Card_System.Player_Deck_Data;
using UnityEngine;

namespace Battle_Management_System.Card_System.Card_Pool_System
{
    internal class CardPoolSystem : MonoBehaviour
    {
        [field: Header("Card Slot")]
        [field: SerializeField] private List<CardSlotSystem> CardSlotList;
        
        [field: Header("Player Deck Data")]
        [field: SerializeField] private PlayerDeckSO PlayerDeckData;
        
        [field: Header("Point")]
        [field: SerializeField] private RectTransform SpawnPoint;

        private IEnumerator RefillCardCoroutine;

        private void OnDisable()
        {
            if (RefillCardCoroutine is not null)
            {
                StopCoroutine(RefillCardCoroutine);
                RefillCardCoroutine = null;
            }
        }

        #region Refill Card
            public void RefillCard(Action onComplete = null)
            {
                RefillCardCoroutine = StartRefillCardCoroutine(onComplete);
                StartCoroutine(RefillCardCoroutine);
            }

            private IEnumerator StartRefillCardCoroutine(Action onComplete)
            {
                for (var i = 0; i < CardSlotList.Count; i ++)
                {
                    var slot = CardSlotList[i];
                    
                    if (!slot.IsEmpty()) continue;
                    
                    var card = Instantiate(PlayerDeckData.GetRandomCard(), SpawnPoint);
                    slot.AddCard(card);

                    var i1 = i;
                    card.GetComponent<ICard>().MoveCardToSlot(slot.transform, () =>
                    {
                        if (i1 == CardSlotList.Count - 1)
                            onComplete?.Invoke();
                    });
                    
                    yield return new WaitForSeconds(.25f);
                }
                
                RefillCardCoroutine = null;
            }
        #endregion
        
        #region Card Slot List
            public void GetCard(int number, out List<GameObject> cardList)
            {
                var tempCardList = new List<GameObject>();
                
                for (var i = 0; i < number; i++)
                {
                    CardSlotList[i].GetCard(out var card);
                    tempCardList.Add(card);
                }
                
                cardList = tempCardList;
            }
        #endregion
    }
}