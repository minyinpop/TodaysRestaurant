using System.Collections;
using System.Collections.Generic;
using Battle_System.Object.Card_Slot;
using Data.Player.Equip_Deck;
using UnityEngine;

namespace Battle_System.Child_System
{
    internal sealed class CardPoolSystem : MonoBehaviour
    {
        [field: Header("Card Slot")]
        [field: SerializeField] private GameObject CardSlotPrefab;
        [field: SerializeField] private Transform SpawnParent;
        
        [field: Header("Data")]
        [field: SerializeField] private PlayerEquipDeckSO PlayerEquipDeckData;

        private List<CardSlot> CardSlots = new();

        private const int MaxCardSlot = 8;
        
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
        
        public void Refill()
        {
            SortCoroutine = SortProcess();
            RefillCoroutine = RefillProcess();
            StartCoroutine(SortCoroutine);
        }

        private IEnumerator SortProcess()
        {
            for (var i = 0; i < CardSlots.Count; i++)
            {
                var slot = CardSlots[i];
                if (!slot.IsEmpty()) continue;
                CardSlots.Remove(slot);
                Destroy(slot.gameObject);
            }

            yield break;
        }

        private IEnumerator RefillProcess()
        {
            // TODO 2025.09.04
            yield break;
        }
    }
}