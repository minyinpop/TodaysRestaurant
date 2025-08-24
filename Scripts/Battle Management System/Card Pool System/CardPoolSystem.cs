using System.Collections.Generic;
using Player.Data.Equip_Deck;
using UnityEngine;

namespace Battle_Management_System.Card_Pool_System
{
    internal class CardPoolSystem : MonoBehaviour
    {
        [field: Header("Equip Deck Data")]
        [field: SerializeField] private EquipDeckData EquipDeckData;
        
        [field: Header("Position")]
        [field: SerializeField] private Transform SpawnPos;
        
        [field: Header("Card Slots")]
        [field: SerializeField] private List<GameObject> CardSlots;
    }
}