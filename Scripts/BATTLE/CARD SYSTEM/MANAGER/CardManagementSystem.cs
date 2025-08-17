using BATTLE.CARD_SYSTEM.MANAGER.CARD_DRAW_SYSTEM;
using BATTLE.CARD_SYSTEM.MANAGER.HAND_CARD_SYSTEM;
using UnityEngine;

namespace BATTLE.CARD_SYSTEM.MANAGER
{
    [RequireComponent(typeof(CardDrawSystem))]
    [RequireComponent(typeof(HandCardSystem))]
    internal class CardManagementSystem : MonoBehaviour
    {
        private CardDrawSystem CardDrawSystem;
        private HandCardSystem HandCardSystem;
        
        private void Awake()
        {
            CardDrawSystem = GetComponent<CardDrawSystem>();
            HandCardSystem = GetComponent<HandCardSystem>();
        }
    }
}