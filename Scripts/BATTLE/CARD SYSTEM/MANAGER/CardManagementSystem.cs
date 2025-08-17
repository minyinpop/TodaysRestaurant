using BATTLE.CARD_SYSTEM.MANAGER.CARD_POOL_SYSTEM;
using BATTLE.CARD_SYSTEM.MANAGER.HAND_CARD_SYSTEM;
using UnityEngine;

namespace BATTLE.CARD_SYSTEM.MANAGER
{
    [RequireComponent(typeof(CardPoolSystem))]
    [RequireComponent(typeof(HandCardSystem))]
    internal class CardManagementSystem : MonoBehaviour
    {
        private CardPoolSystem CardPoolSystem;
        private HandCardSystem HandCardSystem;
        
        private void Awake()
        {
            CardPoolSystem = GetComponent<CardPoolSystem>();
            HandCardSystem = GetComponent<HandCardSystem>();
        }
    }
}