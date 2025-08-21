using BATTLE_MANAGEMENT_SYSTEM.CARD_POOL_SYSTEM;
using UnityEngine;

namespace BATTLE_MANAGEMENT_SYSTEM
{
    [RequireComponent(typeof(CardPoolSystem))]
    internal class BattleManagementSystem : MonoBehaviour
    {
        [field: Header("System")]
        [field: SerializeField] private CardPoolSystem CardPoolSystem;
    }
}