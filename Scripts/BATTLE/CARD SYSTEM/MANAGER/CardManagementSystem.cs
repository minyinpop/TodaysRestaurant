using BATTLE.CARD_SYSTEM.MANAGER.CARD_POOL_SYSTEM;
using BATTLE.CARD_SYSTEM.MANAGER.HAND_CARD_SYSTEM;
using UnityEngine;

namespace BATTLE.CARD_SYSTEM.MANAGER
{
    internal class CardManagementSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private CardPoolSystem CardPoolSystem;
        [field: SerializeField] private HandCardSystem HandCardSystem;
    }
}