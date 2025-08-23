using Battle_Management_System.Card_System;
using Battle_Management_System.Decisive_Coin_System;
using UnityEngine;

namespace Battle_Management_System
{
    internal class BattleManagementSystem : MonoBehaviour
    {
        [field: Header("System")]
        [field: SerializeField] private CardSystem cardSystem;
        [field: SerializeField] private DecisiveCoinSystem DecisiveCoinSystem;
    }
}