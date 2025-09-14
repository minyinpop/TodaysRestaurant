using System.Battle_System.Object.Card_Slot;
using System.Collections.Generic;
using UnityEngine;

namespace System.Battle_System.System.Child
{
    internal sealed class UseCardSystem : MonoBehaviour
    {
        [field: Header("Card Slot")]
        [field: SerializeField] private Transform SpawnParent;
        [field: SerializeField] private GameObject SlotPrefab;
        
        private readonly List<CardSlot> CardSlots = new();
    }
}