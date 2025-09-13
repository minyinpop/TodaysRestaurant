using System.Card_Battle_System.Object.Card_Slot;
using System.Collections.Generic;
using UnityEngine;

namespace System.Card_Battle_System.System.Child
{
    internal sealed class SelectedCardSystem : MonoBehaviour
    {
        [field: Header("Card Slot")]
        [field: SerializeField] private Transform SpawnParent;
        [field: SerializeField] private GameObject SlotPrefab;
        
        [field: Header("Card Order")]
        [field: SerializeField] private List<GameObject> CardOrderPrefabs;
        
        [field: Header("Object")]
        [field: SerializeField] private GameObject SelectedCardUI;

        private List<CardSlot> CardSlots = new();

        private void OnEnable()
        {
            HandCardSystem.CanSelectCard += AddCard;
        }
        
        private void OnDisable()
        {
            HandCardSystem.CanSelectCard -= AddCard;
        }

        private bool AddCard()
        {
            return false;
        }
    }
}