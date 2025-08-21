using UnityEngine;

namespace BATTLE_MANAGEMENT_SYSTEM.CARD_SYSTEM.BATTLE_CARD
{
    [RequireComponent(typeof(AnimationSystem))]
    internal abstract class BattleCardBase : MonoBehaviour, ICard, IBattleCard
    {
        [field: Header("Card Data")]
        [field: SerializeField] private CardSO CardData;
    }
}