using System.Collections;
using System.Collections.Generic;
using BATTLE_MANAGEMENT_SYSTEM.CARD_SLOT_SYSTEM;
using UnityEngine;

namespace BATTLE_MANAGEMENT_SYSTEM.CARD_POOL_SYSTEM
{
    internal class CardPoolSystem : MonoBehaviour
    {
        [field: Header("Card Slot")]
        [field: SerializeField] private List<CardSlotSystem> CardSlotList;

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
            public void RefillCard()
            {
                RefillCardCoroutine = StartRefillCardCoroutine();
                StartCoroutine(RefillCardCoroutine);
            }

            private IEnumerator StartRefillCardCoroutine()
            {
                for (int i = 0; i < CardSlotList.Count; i++)
                {
                    yield return new WaitForSeconds(.25f);
                }
            }
        #endregion
    }
}