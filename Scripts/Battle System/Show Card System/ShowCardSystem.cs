using System.Collections;
using System.Collections.Generic;
using Battle_System.Card_Slot_System;
using UnityEngine;

namespace Battle_System.Show_Card_System
{
    internal sealed class ShowCardSystem : MonoBehaviour
    {
        [field: Header("Slot")]
        [field: SerializeField] private List<CardSlotSystem> CardSlots;
        
        private IEnumerator ShowCoroutine;
        
        private void OnDisable()
        {
            if (ShowCoroutine is not null)
            {
                StopCoroutine(ShowCoroutine);
                ShowCoroutine = null;
            }
        }

        public void ShowCard(List<GameObject> Cards)
        {
        }

        private IEnumerator ShowProcess()
        {
            yield return null;
        }
    }
}