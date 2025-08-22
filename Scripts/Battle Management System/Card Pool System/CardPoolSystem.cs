using System.Collections;
using System.Collections.Generic;
using Battle_Management_System.Card_Slot_System;
using Battle_Management_System.Player_Deck_Data;
using UnityEngine;

namespace Battle_Management_System.Card_Pool_System
{
    internal class CardPoolSystem : MonoBehaviour
    {
        [field: Header("Player Deck Data")]
        [field: SerializeField] private PlayerDeckSO PlayerDeckData;
        
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
                for (var i = 0; i < CardSlotList.Count; i++)
                {
                    Debug.Log(PlayerDeckData.GetRandomCard().name);
                    yield return new WaitForSeconds(.25f);
                }
            }
        #endregion
    }
}