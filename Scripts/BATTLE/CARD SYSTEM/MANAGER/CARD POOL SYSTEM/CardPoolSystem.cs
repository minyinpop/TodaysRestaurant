using System.Collections;
using System.Collections.Generic;
using BATTLE.CARD_SYSTEM.CARD.BATTLE_CARD.INTERFACE;
using BATTLE.CARD_SYSTEM.MANAGER.CARD_POOL_SYSTEM.CARD_POOL_SLOT;
using BATTLE.CARD_SYSTEM.MANAGER.CARD_POOL_SYSTEM.PLAYER_DECK;
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
        
        private void Start()
        {
            // For Development Only.
            RefillCardPool();
        }

        private void OnDisable()
        {
            if (CurrentCoroutine is not null)
            {
                StopCoroutine(CurrentCoroutine);
                CurrentCoroutine = null;
            }
        }

        #region Refill Card Pool
            private void RefillCardPool()
            {
                CurrentCoroutine = RefillCardPoolCoroutine();
                StartCoroutine(CurrentCoroutine);
            }
            
            private IEnumerator RefillCardPoolCoroutine()
            {
                Debug.Log("Refill Card Pool Start.");
                
                foreach (var slot in SlotList)
                {
                    if (!slot.IsEmpty()) continue;

                    var targetParent = slot.transform;
                    var randomCard = PlayerDeckSO.GetRandomCard();
                    var card = Instantiate(randomCard, SpawnPoint);
                    
                    slot.AddCard(card);
                    card.GetComponent<IBattleCard>().OnSpawnInCardPool(targetParent);
                    
                    yield return new WaitForSeconds(.2f);
                }
                
                Debug.Log("Refill Card Pool Done.");
            }
        #endregion
    }
}