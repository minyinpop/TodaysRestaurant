using System.Collections;
using System.Collections.Generic;
using BATTLE.CARD_SYSTEM.CARD;
using BATTLE.CARD_SYSTEM.MANAGER.CARD_POOL_SYSTEM.PLAYER_DECK;
using UnityEngine;

namespace BATTLE.CARD_SYSTEM.MANAGER.CARD_POOL_SYSTEM
{
    internal class CardPoolSystem : MonoBehaviour
    {
        [field: Header("Slots")]
        [field: SerializeField] private List<CardSlot> SlotList;
        
        [field: Header("Point")]
        [field: SerializeField] private RectTransform SpawnPoint;
        
        [field: Header("Player Deck")]
        [field: SerializeField] private PlayerDeckSO PlayerDeckSO;
        
        private IEnumerator RefillCoroutine;

        private void OnDisable()
        {
            if (RefillCoroutine is not null)
            {
                StopCoroutine(RefillCoroutine);
                RefillCoroutine = null;
            }
        }

        #region Refill Card Pool
            public void RefillCardPool(System.Action OnComplete)
            {
                RefillCoroutine = RefillCardPoolCoroutine(OnComplete);
                StartCoroutine(RefillCoroutine);
            }
            
            private IEnumerator RefillCardPoolCoroutine(System.Action OnComplete)
            {
                foreach (var slot in SlotList)
                {
                    SpawnCardAndMoveToSlotPosition(slot, out var isEmpty);
                    if (!isEmpty) continue;
                    
                    yield return new WaitForSeconds(.2f);
                }

                OnComplete?.Invoke();
                RefillCoroutine = null;
            }
        #endregion
        
        #region Get Card
            public void GetCard(out GameObject card)
            {
                SlotList[0].GetCard(out card);
                Refill();
            }

            private void Refill()
            {
                var tempCardList = new List<GameObject>();
                
                foreach (var slot in SlotList)
                {
                    if (slot.IsEmpty()) continue;
                    
                    slot.GetCard(out var card);
                    tempCardList.Add(card);
                }

                for (var i = 0; i < SlotList.Count; i++)
                {
                    if (i >= tempCardList.Count)
                        SpawnCardAndMoveToSlotPosition(SlotList[i], out _);
                    else if (i < tempCardList.Count)
                        SlotList[i].AddCard(tempCardList[i]);
                }
            }
        #endregion

        private void SpawnCardAndMoveToSlotPosition(CardSlot slot, out bool isEmpty)
        {
            isEmpty = slot.IsEmpty();
            if (!isEmpty) return;
            
            var randomCard = PlayerDeckSO.GetRandomCard();
            var card = Instantiate(randomCard, SpawnPoint);
            
            slot.AddCard(card);
            card.GetComponent<ICard>().OnSpawnInCardPool(slot);
        }
    }
}