using System.Collections;
using System.Collections.Generic;
using BATTLE.CARD_SYSTEM.CARD;
using BATTLE.CARD_SYSTEM.MANAGER.CARD_POOL_SYSTEM.CARD_POOL_SLOT;
using BATTLE.CARD_SYSTEM.MANAGER.CARD_POOL_SYSTEM.PLAYER_DECK;
using BATTLE.CARD_SYSTEM.MANAGER.DRAW_CARD_SYSTEM;
using UnityEngine;

namespace BATTLE.CARD_SYSTEM.MANAGER.CARD_POOL_SYSTEM
{
    internal class CardPoolSystem : MonoBehaviour
    {
        [field: Header("Slots")]
        [field: SerializeField] private List<CardPoolSlot> SlotList;
        
        [field: Header("Point")]
        [field: SerializeField] private RectTransform SpawnPoint;
        
        [field: Header("Player Deck")]
        [field: SerializeField] private PlayerDeckSO PlayerDeckSO;

        private IEnumerator CurrentCoroutine;

        public event System.Action OnRefillCardPoolComplete;

        private void OnDisable()
        {
            if (CurrentCoroutine is not null)
            {
                StopCoroutine(CurrentCoroutine);
                CurrentCoroutine = null;
            }
        }

        #region Refill Card Pool
            public void RefillCardPool()
            {
                CurrentCoroutine = RefillCardPoolCoroutine();
                StartCoroutine(CurrentCoroutine);
            }
            
            private IEnumerator RefillCardPoolCoroutine()
            {
                foreach (var slot in SlotList)
                {
                    if (!slot.IsEmpty()) continue;

                    var targetParent = slot.transform;
                    var randomCard = PlayerDeckSO.GetRandomCard();
                    
                    var card = Instantiate(randomCard, SpawnPoint);
                    card.GetComponent<ICard>().OnSpawnInCardPool(targetParent);
                    
                    slot.AddCard(card);
                    
                    yield return new WaitForSeconds(.2f);
                }

                OnRefillCardPoolComplete?.Invoke();
            }
        #endregion
    }
}